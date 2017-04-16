using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using FieldMax2DLLServer;

namespace FieldMaxTest
{
    public class EngineFM2
    {
        public cFM2Listener FieldMax2Listener;
        public _IFM2Listener ThisListener;
        public cFM2ScanUSBForChange ScanUSBForChange;
        public cFM2ScanForData ScanForData;
        public cFM2Notify NotifyMe;
        public cFM2Devices m_DevicesList;
        
        public struct deviceStringInfo
        {
            string index, serialNumber;
            string range, wavelength;
            bool holdMode, backLight, powerState;
            public deviceStringInfo(string index, string serialNumber)
            {
                this.index = index;
                this.serialNumber = serialNumber;
                this.range = "";
                this.wavelength = "";
                this.holdMode = false;
                this.backLight = false;
                this.powerState = false;
            }

            public deviceStringInfo(string index, string serialNumber, string range, string wavelength, bool holdMode, bool backLight, bool powerState)
            {
                this.index = index;
                this.serialNumber = serialNumber;
                this.range = range;
                this.wavelength = wavelength;
                this.holdMode = holdMode;
                this.backLight = backLight;
                this.powerState = powerState;
            }
        }

        //поля для уведомления о изменении статуса
        private string m_lastDeviceStatusConnection = "No connection";
        public string lastDeviceStatusConnection { get { return m_lastDeviceStatusConnection; } }
        public List<deviceStringInfo> m_stringDeviceList = new List<deviceStringInfo>();
        public List<deviceStringInfo> stringDeviceList { get { return m_stringDeviceList; } }

        //события для взаимодействия вне класса

        public delegate void DelegateNotifyFault(string serialNumber, string lastFault);
        public event DelegateNotifyFault EventNotifyFault;
        public delegate void DelegateNotifyProbeRemoved(string serialNumber);
        public event DelegateNotifyProbeRemoved EventNotifyProbeRemoved;
        public delegate void DelegateNotifyProbeAdded(string serialNumber);
        public event DelegateNotifyProbeAdded EventNotifyProbeAdded;
        public delegate void DelegateNotifyPowerOn(string serialNumber);
        public event DelegateNotifyPowerOn EventNotifyPowerOn;
        public delegate void DelegateNotifyPowerOff(string serialNumber);
        public event DelegateNotifyPowerOff EventNotifyPowerOff;
        public delegate void DelegateNotifyMeasurementData(string serialNumber, double data, double period);
        public event DelegateNotifyMeasurementData EventNotifyMeasurementData;
        public delegate void DelegateNotifyPacketIsOverrange(string serialNumber);
        public event DelegateNotifyPacketIsOverrange EventNotifyPacketIsOverrange;
        public delegate void DelegateNotifyOverTemperature(string serialNumber);
        public event DelegateNotifyOverTemperature EventNotifyOverTemperature;
        public delegate void DelegateNotifyMeasurementDataLost(string serialNumber);
        public event DelegateNotifyMeasurementDataLost EventNotifyMeasurementDataLost;
        public delegate void DelegateNotifyMeterAdded(string serialNumber, string deviceIndex);
        public event DelegateNotifyMeterAdded EventNotifyMeterAdded;
        public delegate void DelegateNotifyMeterRemoved(string serialNumber, string deviceIndex);
        public event DelegateNotifyMeterRemoved EventNotifyMeterRemoved;
        public delegate void DelegateNotifyDisplayZeroDeviceProgressToClient(short zeroDeviceTimeoutCounter);
        public event DelegateNotifyDisplayZeroDeviceProgressToClient EventNotifyDisplayZeroDeviceProgressToClient;

        //пулл ошибок
        List<string> lastError = new List<string>();

        //timer
        //System.Timers.Timer timer = new System.Timers.Timer(1000);

        ~EngineFM2()
        {
            foreach (IFM2Device d in m_DevicesList)
            {
                d.CloseAllUSBDeviceDrivers(d.DeviceHandle);
            }
        }

        public EngineFM2(cFM2Notify.FM2Notifier FM2EventHandler, cFM2Notify.FM2ErrorNotifier FM2ErrorEventHandler,
            cFM2Notify.FM2DeviceStatusNotifier FM2DeviceStatusNotifier, cFM2Notify.FM2DisplayZeroDeviceProgressToClient FM2DisplayZeroDeviceProgressToClientHandler)
        {
            FieldMax2Listener = new cFM2Listener();
            ThisListener = (IFM2Listener)FieldMax2Listener;
            ScanUSBForChange = new cFM2ScanUSBForChange();
            ScanForData = new cFM2ScanForDataClass();
            NotifyMe = new cFM2Notify(FM2EventHandler, FM2ErrorEventHandler, FM2DeviceStatusNotifier, FM2DisplayZeroDeviceProgressToClientHandler);
            NotifyMe.notifyEvent += this.EngineFm2EventHandler;
            NotifyMe.notifyErrorEvent += this.EngineFM2ErrorEventHandler;
            NotifyMe.notifyDeviceStatusEvent += this.EngineFM2DeviceStatusNotifier;
            NotifyMe.notifyDisplayZeroDeviceProgressToClient += this.EngineFM2DisplayZeroDeviceProgressToClientHandler;
            FieldMax2Listener.DeviceEvents = NotifyMe;
            ScanUSBForChange.CheckTimer(ref ThisListener);
            
            //timer.Elapsed += this.CheckDevices;
            //lastError.Capacity = 20;
        }

        public EngineFM2()
        {
            FieldMax2Listener = new cFM2Listener();
            ThisListener = (IFM2Listener)FieldMax2Listener;
            ScanUSBForChange = new cFM2ScanUSBForChange();
            ScanForData = new cFM2ScanForDataClass();
            NotifyMe = new cFM2Notify(this.EngineFm2EventHandler, this.EngineFM2ErrorEventHandler,
                this.EngineFM2DeviceStatusNotifier, this.EngineFM2DisplayZeroDeviceProgressToClientHandler);
            FieldMax2Listener.DeviceEvents = NotifyMe;
            ScanUSBForChange.CheckTimer(ref ThisListener);
            //timer.Elapsed += this.CheckDevices;
            Console.WriteLine("Класс создан");
        }

        public void ChangeScanForData(bool enabled)
        {
            if (enabled)
            {
                ScanForData = new cFM2ScanForDataClass();
            }
            else
                ScanForData.StopTimer();
        }

        public void EngineFm2EventHandler(IFM2DeviceEvents CallbackData)
        {
            IFM2Device d;
            string s = CallbackData.DeviceIndex.ToString();
            d = (IFM2Device)m_DevicesList.Item(ref s);
            if (d == null)
            {
                Console.WriteLine("Notify data is null");
            }

            switch (CallbackData.CallbackEvent)
            {
                case "Fault":
                    lastError.Add(d.LastFault.ToString());
                    Console.WriteLine(d.LastFault);
                    if (EventNotifyFault != null)
                        EventNotifyFault(d.SerialNumber, d.LastFault.ToString());
                    break;
                case "ProbeRemoved":
                    Console.WriteLine("ProbeRemoved");
                    if (EventNotifyProbeRemoved != null)
                        EventNotifyProbeRemoved(d.SerialNumber);
                    break;
                case "ProbeAdded":
                    Console.WriteLine("ProveAdded");
                    if (EventNotifyProbeAdded != null)
                        EventNotifyProbeAdded(d.SerialNumber);
                    break;
                case "PowerOn":
                    Console.WriteLine("PowerOn " + d.SerialNumber);
                    if (EventNotifyPowerOn != null)
                        EventNotifyPowerOn(d.SerialNumber);
                    break;
                case "PowerOff":
                    Console.WriteLine("PowerOff" + d.SerialNumber);
                    if (EventNotifyPowerOff != null)
                        EventNotifyPowerOff(d.SerialNumber);
                    break;
                case "MeasurementData":
                    Console.WriteLine("Device " + d.SerialNumber + " " + d.LastData);
                    if (EventNotifyMeasurementData != null)
                        EventNotifyMeasurementData(d.SerialNumber, d.LastData, d.LastPeriod);
                    break;
                case "PacketIsOverrange":
                    Console.WriteLine("Device " + d.SerialNumber + "PacketIsOverrange");
                    if (EventNotifyPacketIsOverrange != null)
                        EventNotifyPacketIsOverrange(d.SerialNumber);
                    break;
                case "OverTemperature":
                    Console.WriteLine("Device " + d.SerialNumber + "OverTemperature");
                    if (EventNotifyOverTemperature != null)
                        EventNotifyOverTemperature(d.SerialNumber);
                    break;
                case "MeasurementDataLost":
                    Console.WriteLine("Device " + d.SerialNumber + "MeasurementDataLost");
                    if (EventNotifyMeasurementDataLost != null)
                        EventNotifyMeasurementDataLost(d.SerialNumber);
                    break;
                default:
                    break;
            }
        }

        public void EngineFM2ErrorEventHandler(string callBackMessage)
        {
            Console.WriteLine(callBackMessage);
            lastError.Add(callBackMessage);
        }

        public void EngineFM2DeviceStatusNotifier(IFM2DeviceEvents CallbackData, cFM2Devices DevicesList)
        {
            m_DevicesList = DevicesList;
            switch (CallbackData.CallbackEvent)
            {
                case "MeterAdded":
                    m_lastDeviceStatusConnection = "MeterAdded";
                    string s = CallbackData.DeviceIndex.ToString();
                    m_DevicesList.Item(ref s).DeviceEvents = NotifyMe;
                    ScanForData.CheckTimer(ref m_DevicesList);
                    m_stringDeviceList.Add(new deviceStringInfo(s, m_DevicesList.Item(ref s).SerialNumber));
                    Console.WriteLine("MeterAdded " + m_DevicesList.Item(ref s).SerialNumber);
                    if (EventNotifyMeterAdded != null)
                        EventNotifyMeterAdded(m_DevicesList.Item(ref s).SerialNumber, s);
                    break;
                case "MeterRemoved":
                    m_lastDeviceStatusConnection = "MeterRemoved";
                    ScanForData.CheckTimer(ref m_DevicesList);
                    m_stringDeviceList.Clear();
                    Console.WriteLine("MeterRemoved");
                    foreach (IFM2Device d in m_DevicesList)
                    {
                        m_stringDeviceList.Add(new deviceStringInfo(d.DeviceIndex.ToString(), d.SerialNumber));
                    }
                    if (EventNotifyMeterRemoved != null)
                        EventNotifyMeterRemoved(m_DevicesList.Item(CallbackData.DeviceIndex.ToString()).SerialNumber, CallbackData.DeviceIndex.ToString());
                    break;
                default:
                    m_lastDeviceStatusConnection = "NoMeterAdded";
                    break;
            }
        }

        public void EngineFM2DisplayZeroDeviceProgressToClientHandler(short zeroDeviceTimeoutCounter)
        {
            if (EventNotifyDisplayZeroDeviceProgressToClient != null)
                EventNotifyDisplayZeroDeviceProgressToClient(zeroDeviceTimeoutCounter);
            Console.WriteLine("Zero progress " + zeroDeviceTimeoutCounter);
        }

        public void CheckDevices(object sender, ElapsedEventArgs e)
        {

            if (m_stringDeviceList.Count == 0)
                return;
            m_stringDeviceList.Clear();
            foreach (IFM2Device d in m_DevicesList)
            {
                m_stringDeviceList.Add(new deviceStringInfo(d.DeviceIndex.ToString(), d.SerialNumber, d.Range.ToString(),
                    d.Wavelength.ToString(), d.HoldMode, d.Backlight, d.PowerState));
            }
        }
    }

    public class cFM2Notify : IFM2DeviceEvents
    {
        private string m_CallbackEvent; //содержит тип события
        private string m_CallbackMessage; //содержит текст ошибки
        private short m_DeviceIndex;
        private string m_SerialNumber;
        private short m_ZeroDeviceTimeoutCounter;

        public delegate void FM2Notifier(IFM2DeviceEvents CallbackData);
        public event FM2Notifier notifyEvent;
        public delegate void FM2ErrorNotifier(string callBackMessage);
        public event FM2ErrorNotifier notifyErrorEvent;
        public delegate void FM2DeviceStatusNotifier(IFM2DeviceEvents CallbackData, cFM2Devices DevicesList);
        public event FM2DeviceStatusNotifier notifyDeviceStatusEvent;
        public delegate void FM2DisplayZeroDeviceProgressToClient(short zeroDeviceTimeoutCounter);
        public event FM2DisplayZeroDeviceProgressToClient notifyDisplayZeroDeviceProgressToClient;

        public cFM2Notify(FM2Notifier FM2EventHandler, FM2ErrorNotifier FM2ErrorEventHandler, FM2DeviceStatusNotifier FM2DeviceStatusHandler,
            FM2DisplayZeroDeviceProgressToClient FM2DilsplayZeroDeviceProgressToClientHandler)
        {
            notifyEvent += FM2EventHandler;
            notifyErrorEvent += FM2ErrorEventHandler;
            notifyDeviceStatusEvent += FM2DeviceStatusHandler;
            notifyDisplayZeroDeviceProgressToClient += FM2DilsplayZeroDeviceProgressToClientHandler;
        }


        public void DisplayErrorToClient() //посылает ошибки пользователю, которые описаны в CallbackMessage
        {
            //MessageBox.Show(m_CallbackMessage);
            notifyErrorEvent(m_CallbackMessage);
        }


        public void NotifyData(IFM2DeviceEvents CallbackData) //метод, обрабатывающий событие
        {
            //form.NotifyData(CallbackData);
            notifyEvent(CallbackData);
        }


        public void NotifyDeviceStatus(IFM2DeviceEvents CallbackData, cFM2Devices DevicesList) //метод, обрабатывающий событие изменения статуса устройства
        {

            //form.NotifyDeviceStatus(CallbackData, DevicesList);
            notifyDeviceStatusEvent(CallbackData, DevicesList);

        }


        public void DisplayZeroDeviceProgressToClient() //метод вызывается раз в секунду для индикации прогресса калибровки устройства; m_ZeroDeviceTimeoutCounter показывает оставшиеся секнды, если значение -1 - ошибка
        {
            //form.DisplayZeroDeviceProgress(m_CallbackMessage, m_ZeroDeviceTimeoutCounter);
            notifyDisplayZeroDeviceProgressToClient(m_ZeroDeviceTimeoutCounter);
        }


        public string CallbackEvent
        {
            set { m_CallbackEvent = value; }
            get { return m_CallbackEvent; }
        }


        public string CallbackMessage //содержит текст ошибки
        {
            set { m_CallbackMessage = value; }
            get { return m_CallbackMessage; }
        }


        public short DeviceIndex
        {
            set { m_DeviceIndex = value; }
            get { return m_DeviceIndex; }
        }


        public string SerialNumber
        {
            set { m_SerialNumber = value; }
            get { return m_SerialNumber; }
        }


        public short ZeroDeviceTimeoutCounter
        {
            set { m_ZeroDeviceTimeoutCounter = value; }
        }
    }

}
