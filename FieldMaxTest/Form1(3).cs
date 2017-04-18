using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using ZedGraph;
using System.IO;
//using System.Timers;
using FieldMax2DLLServer;
using System.Threading;

namespace FieldMaxTest
{
    public partial class Form1 : Form
    {
        struct Info
        {
            public string deviceIndex;
            public string serialNumber;
            public Info(string Index, string serial)
            {
                deviceIndex = Index;
                serialNumber = serial;
            }
        }

        private List<Info> devicesList = new List<Info>();
        private Info selectedDeviceComboBox1;
        private FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

        const int DotsAmount = 1000;
        private EngineFM2 engine;
        private PointPairList list = new PointPairList();
        private PointPairList allData = new PointPairList();
        private Stopwatch stopwatch = new Stopwatch();
        private StreamWriter DataFile;
        private StreamWriter SaveFile;
        private PointPairList AverageList = new PointPairList();
        private string StartTime;

        private double getAverageOfLastPointsInList(int numbOfDots, PointPairList listWithPoints)
        {

            int lastIndexInList = listWithPoints.Count - 1;
            double averagePower = 0;

            if ((numbOfDots > 0) && (listWithPoints.Count > 0))
            {
                if (listWithPoints.Count > numbOfDots)
                {
                    for (int i = lastIndexInList; i > lastIndexInList - numbOfDots; i--)
                        averagePower += listWithPoints[i].Y / numbOfDots;                   
                }
                else
                {
                    for (int i = lastIndexInList; i >= 0; i--)
                        averagePower += listWithPoints[i].Y / (lastIndexInList + 1);
                }
            }
            else
            {
                textBoxAveragePower.Text = "Check the amount of dots or start button";
                return -1;
            }
            return averagePower;
        }

        public Form1()
        {
            InitializeComponent();
            zedGraphControl1.GraphPane.Title = "Graph";
            zedGraphControl1.GraphPane.XAxis.Title = "Time, ms";
            zedGraphControl1.GraphPane.YAxis.Title = "Power, W";
            zedGraphControl1.GraphPane.AddCurve("", list, Color.Blue, SymbolType.None);
            zedGraphControl1.GraphPane.AddCurve("", AverageList, Color.Red, SymbolType.None);
            
            timer1.Enabled = false;
            button_ExportData.Enabled = false;
            
            engine = new EngineFM2();
            engine.EventNotifyFault += ErrorHandler;
            engine.EventNotifyMeasurementData += MeasurementDataHandler;
            engine.EventNotifyMeasurementDataLost += MeasurementDataLostHandler;
            engine.EventNotifyOverTemperature += OverTemperatureHandler;
            engine.EventNotifyPacketIsOverrange += PacketIsOverrangeHandler;
            engine.EventNotifyPowerOff += PowerOffHandler;
            engine.EventNotifyPowerOn += PowerOnHandler;
            engine.EventNotifyProbeAdded += ProbeAddedHandler;
            engine.EventNotifyProbeRemoved += ProbeRemovedHandler;
            engine.EventNotifyMeterAdded += NotifyMeterAddedHandler;
            engine.EventNotifyMeterRemoved += NotifyMeterRemovedHandler;
            engine.EventNotifyDisplayZeroDeviceProgressToClient += NotifyZeroDisplayZeroDeviceProgressToClientHandler;

            folderBrowserDialog1.SelectedPath = "";
            
            DataFile = new StreamWriter("LastData.txt");
        }       

        public void ErrorHandler(string serialNumber, string lastFault) //обработчик ошибок
        {
            listBox1.Items.Add(serialNumber+" "+ lastFault);
            MessageBox.Show(serialNumber + " " + lastFault);
        }

        public void ProbeRemovedHandler(string serialNumber)
        {
            timer1.Stop();
            string temp = serialNumber + " Probe removed";
            listBox1.Items.Add(temp);
            if (DataFile != null)
                DataFile.Flush();
            //MessageBox.Show(temp);
        }

        public void ProbeAddedHandler(string serialNumber)
        {
            string temp = serialNumber + " Probe added";
            listBox1.Items.Add(temp);
            //MessageBox.Show(temp);
        }

        public void PowerOnHandler(string serialNumber)
        {
            string temp = serialNumber+" Power On";
            listBox1.Items.Add(temp);
            MessageBox.Show(temp);
        }

        public void PowerOffHandler(string serialNumber)
        {
            string temp = serialNumber + " Power Off";
            listBox1.Items.Add(temp);
            MessageBox.Show(temp);
        }

        public void MeasurementDataHandler(string serialNumber, double data, double period)
        {
            if (timer1.Enabled)
            {
                list.Add(Convert.ToDouble(stopwatch.ElapsedMilliseconds), data);
                allData.Add(Convert.ToDouble(stopwatch.ElapsedMilliseconds), data);

                int numbOfDots = (int)numericUpDown_amountOfAverDots.Value;

                if ((numbOfDots > 0) && (list.Count > 0))
                {
                    AverageList.Add(Convert.ToDouble(stopwatch.ElapsedMilliseconds), getAverageOfLastPointsInList(numbOfDots,list));
                }
                else
                {
                    textBoxAveragePower.Text = "Check the amount of dots or start button";
                }
                string temp = string.Format("{0,4}\t{1,4}", Convert.ToDouble(stopwatch.ElapsedMilliseconds), data);
                DataFile.WriteLine(temp);
                DataFile.Flush();
                //listBox1.Items.Add(temp);
            }
        }

        public void PacketIsOverrangeHandler(string serialNumber)
        {
            string temp = serialNumber + " Packet Is Ovverange";
            listBox1.Items.Add(temp);
            MessageBox.Show(temp);
        }

        public void OverTemperatureHandler(string serialNumber)
        {
            string temp = serialNumber + " Over Temperature";
            listBox1.Items.Add(temp);
            MessageBox.Show(temp);
        }

        public void MeasurementDataLostHandler(string serialNumber)
        {
            string temp = serialNumber + " Some data was lost";
            listBox1.Items.Add(temp);
            MessageBox.Show(temp);
        }

        public void NotifyMeterAddedHandler(string serialNumber, string deviceIndex)
        {
            comboBox1.Items.Add(serialNumber);
            comboBox1.SelectedItem = serialNumber;
            devicesList.Add(new Info(deviceIndex, serialNumber));
            selectedDeviceComboBox1 = new Info(deviceIndex, serialNumber);
        }

        public void NotifyMeterRemovedHandler(string serialNumber, string deviceIndex)
        {
            devicesList.Remove(new Info(deviceIndex, serialNumber));
            selectedDeviceComboBox1 = new Info();
            comboBox1.Items.Remove(serialNumber);
            if ((DataFile != null)&&(timer1.Enabled))
                DataFile.Flush();
                      
        }

        private void NotifyZeroDisplayZeroDeviceProgressToClientHandler(short zeroDeviceTimeoutCounter)
        {
            if (zeroDeviceTimeoutCounter == 0)
            {
                ZeroButton.Enabled = true;
                ZeroButton.Text = "Zero";
            }
            if (zeroDeviceTimeoutCounter == -1)
            {
                MessageBox.Show("Zero fail");
                listBox1.Items.Add("Zero fail " + selectedDeviceComboBox1.serialNumber);
                ZeroButton.Text = "Zero";
            }
        }

        private void TextBoxChange()
        { 
        
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // list.Add(DateTime.Now,Math.Sin(DateTime.Now.Minute
            
            if (list.Count > DotsAmount)
            {
                for (int i = 0; i < list.Count - DotsAmount; i++)
                {
                    list.RemoveAt(0);
                }
                for (int i = 0; i < AverageList.Count - DotsAmount; i++)
                {
                    AverageList.RemoveAt(0);
                }
            }
            if (list != null)
            {
                zedGraphControl1.GraphPane.CurveList[0].Points = list;
                zedGraphControl1.GraphPane.CurveList[1].Points = AverageList;
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
            }
        }
        
        private void button_Start_Clicked(object sender, EventArgs e)
        {
            if (button_Start.Text == "Start")
            {
                engine.ChangeScanForData(true);
                //engine.ScanForData.CheckTimer(ref engine.m_DevicesList);
                StartTime = DateTime.Now.ToString();
                DataFile.WriteLine(StartTime);
                DataFile.WriteLine();
                DataFile.WriteLine("Time, ms\tPower, W");
                DataFile.Flush();
                timer1.Start();
                stopwatch.Start();
                button_Start.Text = "Stop";
                button_ExportData.Enabled = false;

            }
            else
            {
                engine.ChangeScanForData(false);
                //engine.ScanForData.StopTimer();
                timer1.Stop();
                stopwatch.Stop();
                button_Start.Text = "Start";
                DataFile.Flush();
                if (allData.Count > 0)
                    button_ExportData.Enabled = true;              
            }

        }

        private void BackLight_Click(object sender, EventArgs e)
        {
            //if(butt
            if (selectedDeviceComboBox1.serialNumber != null)
                engine.m_DevicesList.Item(selectedDeviceComboBox1.deviceIndex).BacklightCommand(true);
            //engine.m_DevicesList.Item(devicesList.
        }

        private void button_average_Click(object sender, EventArgs e)
        {
            int lastIndexInList = list.Count-1;
            int numbOfDots=(int)numericUpDown_amountOfAverDots.Value;

            if  ((numbOfDots > 0)&&(list.Count>0))
            {
                if (list.Count > numbOfDots)
                {
                    textBoxAveragePower.Text = string.Format("{0:E3}", getAverageOfLastPointsInList(numbOfDots,list));
                }
                else
                {
                    textBoxAveragePower.Text = string.Format("{0:E3}\t{1} - ", getAverageOfLastPointsInList(list.Count, list), list.Count);  
                }
            }
            else
            {
                textBoxAveragePower.Text = "Check the amount of dots or start button";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Info i in devicesList)
            {
                if (i.serialNumber == comboBox1.SelectedItem.ToString())
                {
                    selectedDeviceComboBox1 = i;
                    break;
                }
            }
            selectedDeviceComboBox1 = new Info("0", "0");
        }

        private void ZeroButton_Click(object sender, EventArgs e)
        {
            if (selectedDeviceComboBox1.serialNumber != null)
            {
                ZeroButton.Enabled = false;
                Thread th = new Thread(engine.m_DevicesList.Item(selectedDeviceComboBox1.deviceIndex).ZeroDevice);
                th.Start();
                ZeroButton.Text = "Zeroing";
            }
        }

        private void button_browse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
        }

        private void button_ExportData_Click(object sender, EventArgs e)
        {
            SaveFile = new StreamWriter(folderBrowserDialog1.SelectedPath + "\\" + textBox_FileName.Text, true);
            SaveFile.WriteLine(StartTime);
            SaveFile.WriteLine();
            SaveFile.WriteLine("Time, ms\tPower, W");
            foreach (PointPair p in allData)
            {
                string temp = string.Format("{0,4}\t{1,4}", p.X, p.Y);
            }
            SaveFile.Close();
        }
    }
}
