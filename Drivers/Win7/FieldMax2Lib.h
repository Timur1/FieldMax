/******************************************************************************
FILE:			$Archive: /Delta/ActiveX/FieldMax2/FieldMax2Lib/APA/FieldMax2Lib.h $
AUTHOR:			$Author: Spethj $
REVISION:		$Revision: 19 $

DESCRIPTION:	The header file that exposes the DLL public functions.

HISTORY:		$Log: /Delta/ActiveX/FieldMax2/FieldMax2Lib/APA/FieldMax2Lib.h $
 * 
 * 19    10/04/06 8:05a Spethj
 * Added the serial number write API function
 *
 * 16    8/26/05 12:35p Spethj
 *
 * 15    7/26/05 11:29a Spethj
 * Added the double precision version of get-instrument-state
 *
 * 11    7/13/05 2:40p Spethj
 * Further improvement of device status
 *
 * 8     6/15/05 12:45p Spethj
 * Removed USB descriptor function and structure
 *
 * 2     5/04/05 8:51a Spethj
 * Added the probe type enum
 *
 * 1     4/27/05 9:34a Spethj
 *
 * 1     12/21/04 8:18a Spethj
******************************************************************************/

#ifndef _FIELDMAX2LIB_H
#define	_FIELDMAX2LIB_H

// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the FIELDMAX2LIB_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// FIELDMAX2LIB_API functions as being imported from a DLL, wheras this DLL sees symbols
// defined with this macro as being exported.
#ifdef FIELDMAX2LIB_EXPORTS
	#define FIELDMAX2LIB_API extern "C" __declspec(dllexport)
#else
	#define FIELDMAX2LIB_API extern "C" __declspec(dllimport)
#endif

#define	MAX_USB_CONNECTIONS					127

typedef unsigned char	VB_BYTE;
typedef short			VB_INTEGER;
typedef int				VB_LONG;
typedef unsigned short	VB_BOOLEAN;
typedef char *			VB_STRING;
typedef float			VB_SINGLE;
typedef double			VB_DOUBLE;

#define	VB_TRUE			-1
#define	VB_FALSE		0

//
// Fieldmax status change structure definition
//
typedef struct
{
	VB_INTEGER index;
	VB_INTEGER position;
	VB_BOOLEAN present;
} StatusChangeType;

/****************************************************************************
	VB declaration:

	Private Type StatusChangeType
	    index As Integer
	    position As Integer
	    present As Boolean
	End Type
****************************************************************************/

//
// Fieldmax data packet structure definition
//

typedef struct
{
	union
	{
		VB_SINGLE value;
		VB_LONG dw;
	} measure;
	VB_LONG period;
} DataPacketType;

/****************************************************************************
	VB declaration:

	Private Type DataPacketType
	    value As Single
	    period As Long
	End Type
****************************************************************************/

//
// Fieldmax instrument state structure definition using single precision floats
//
typedef struct
{
	VB_INTEGER probeType;							// 0
	VB_INTEGER meterType;							// 2
	VB_INTEGER lastFault;							// 4
	VB_INTEGER statisticsMode;						// 6
	VB_INTEGER batchRestartMode;					// 8
	VB_INTEGER fill1;								// 10
	VB_LONG batchSizeSeconds;						// 12
	VB_LONG batchSizePulses;						// 16
	VB_LONG wavelength;								// 20
	VB_LONG minWavelength;							// 24
	VB_LONG maxWavelength;							// 28
	VB_BOOLEAN areaCorrectionEnabled;				// 32
	VB_INTEGER fill2;								// 34
	VB_SINGLE areaCorrectionDiameter;				// 36
	VB_BOOLEAN averageModeEnabled;					// 40
	VB_INTEGER averageWindowSizeSeconds;			// 42
	VB_INTEGER averageWindowSizePulses;				// 44
	VB_BOOLEAN attenuationCorrectionModeEnabled;	// 46
	VB_SINGLE attenuationCorrectionFactor;			// 48
	VB_INTEGER triggerLevel;						// 52
	VB_INTEGER measurementMode;						// 54
	VB_BOOLEAN autoRangingEnabled;					// 56
	VB_INTEGER fill3;								// 58
	VB_SINGLE range;								// 60
	VB_SINGLE minRange;								// 64
	VB_SINGLE maxRange;								// 68
	VB_BOOLEAN speedupDigitalDisplay;				// 72
	VB_BOOLEAN speedupHostData;						// 74
	VB_BOOLEAN speedupAnalogOutput;					// 76
	VB_BOOLEAN speedupMeter;						// 78
	VB_INTEGER analogOutFullscaleVoltage;			// 80
	VB_BOOLEAN powerState;							// 82
	VB_BOOLEAN backlight;							// 84
	VB_BOOLEAN hertzMode;							// 86
	VB_BOOLEAN holdMode;							// 88
} InstrumentStateType;

//
// Fieldmax instrument state structure definition using double precision floats
//
typedef struct
{
	VB_INTEGER probeType;							// 0
	VB_INTEGER meterType;							// 2
	VB_INTEGER lastFault;							// 4
	VB_INTEGER statisticsMode;						// 6
	VB_INTEGER batchRestartMode;					// 8
	VB_INTEGER fill1;								// 10
	VB_LONG batchSizeSeconds;						// 12
	VB_LONG batchSizePulses;						// 16
	VB_LONG wavelength;								// 20
	VB_LONG minWavelength;							// 24
	VB_LONG maxWavelength;							// 28
	VB_BOOLEAN areaCorrectionEnabled;				// 32
	VB_INTEGER fill2;								// 34
	VB_DOUBLE areaCorrectionDiameter;				// 36
	VB_BOOLEAN averageModeEnabled;					// 44
	VB_INTEGER averageWindowSizeSeconds;			// 46
	VB_INTEGER averageWindowSizePulses;				// 48
	VB_BOOLEAN attenuationCorrectionModeEnabled;	// 50
	VB_DOUBLE attenuationCorrectionFactor;			// 52
	VB_INTEGER triggerLevel;						// 60
	VB_INTEGER measurementMode;						// 62
	VB_BOOLEAN autoRangingEnabled;					// 64
	VB_INTEGER fill3;								// 66
	VB_DOUBLE range;								// 68
	VB_DOUBLE minRange;								// 76
	VB_DOUBLE maxRange;								// 84
	VB_BOOLEAN speedupDigitalDisplay;				// 92
	VB_BOOLEAN speedupHostData;						// 94
	VB_BOOLEAN speedupAnalogOutput;					// 96
	VB_BOOLEAN speedupMeter;						// 98
	VB_INTEGER analogOutFullscaleVoltage;			// 100
	VB_BOOLEAN powerState;							// 102
	VB_BOOLEAN backlight;							// 104
	VB_BOOLEAN hertzMode;							// 106
	VB_BOOLEAN holdMode;							// 108
} InstrumentStateType2;

// VB_INTEGER probeType enumerated values
enum
{
	ptNone = 0,
	ptThermopile = 1,
	ptPyroelectric = 2,
	ptOptical = 3
};

// VB_INTEGER meterType enumerated values
enum
{
	mtNone = 0,
	mtTOP = 1,
	mtTO = 2,
	mtP = 3
};

// VB_INTEGER statisticsMode enumerated values
enum
{
	smNone = 0,
	smOff = 1,
	smMax = 2,
	smMin = 3,
	smMean = 4,
	smStdv = 5
};

// VB_INTEGER batchRestartMode enumerated values
enum
{
	brmNone = 0,
	brmManual = 1,
	brmAuto = 2
};

// VB_INTEGER measurementMode enumerated values
enum
{
	mmNone = 0,
	mmJoules = 1,
	mmWatts = 2
};

// VB_INTEGER analogOutFullscaleVoltage enumerated values
enum
{
	aofvNone = 0,
	aofv1V = 1,
	aofv2V = 2,
	aofv5V = 3
};

/****************************************************************************
	VB declaration:

	Private Type InstrumentStateType
		probeType As Integer
		meterType As Integer
		lastFault As Integer
		statisticsMode As Integer
		batchRestartMode As Integer
		fill1 As Integer
		batchSizeSeconds As Long
		batchSizePulses As Long
		wavelength As Long
		minWavelength As Long
		maxWavelength As Long
		areaCorrectionEnabled As Boolean
		fill2 As Integer
		areaCorrectionDiameter As Single
		averageModeEnabled As Boolean
		averageWindowSizeSeconds As Integer
		averageWindowSizePulses As Integer
		attenuationCorrectionModeEnabled As Boolean
		attenuationCorrectionFactor As Single
		triggerLevel As Integer
		measurementMode As Integer
		autoRangingEnabled As Boolean
		fill3 As Integer
		range As Single
		minRange As Single
		maxRange As Single
		speedupDigitalDisplay As Boolean
		speedupHostData As Boolean
		speedupAnalogOutput As Boolean
		speedupMeter As Boolean
		analogOutFullscaleVoltage As Integer
		powerState As Boolean
		backlight As Boolean
		hertzMode As Boolean
		holdMode As Boolean
	End Type

	Private Type InstrumentStateType2
		probeType As Integer
		meterType As Integer
		lastFault As Integer
		statisticsMode As Integer
		batchRestartMode As Integer
		fill1 As Integer
		batchSizeSeconds As Long
		batchSizePulses As Long
		wavelength As Long
		minWavelength As Long
		maxWavelength As Long
		areaCorrectionEnabled As Boolean
		fill2 As Integer
		areaCorrectionDiameter As Double
		averageModeEnabled As Boolean
		averageWindowSizeSeconds As Integer
		averageWindowSizePulses As Integer
		attenuationCorrectionModeEnabled As Boolean
		attenuationCorrectionFactor As Double
		triggerLevel As Integer
		measurementMode As Integer
		autoRangingEnabled As Boolean
		fill3 As Integer
		range As Double
		minRange As Double
		maxRange As Double
		speedupDigitalDisplay As Boolean
		speedupHostData As Boolean
		speedupAnalogOutput As Boolean
		speedupMeter As Boolean
		analogOutFullscaleVoltage As Integer
		powerState As Boolean
		backlight As Boolean
		hertzMode As Boolean
		holdMode As Boolean
	End Type

	Private Enum ProbeTypeEnum
		ptNone = 0
		ptThermopile = 1
		ptPyroelectric = 2
		ptOptical = 3
	End Enum

	Private Enum MeterTypeEnum
		mtNone = 0
		mtTOP = 1
		mtTO = 2
		mtP = 3
	End Enum

	Private Enum StatisticsModeEnum
		smNone = 0
		smOff = 1
		smMax = 2
		smMin = 3
		smMean = 4
		smStdv = 5
	End Enum

	Private Enum BatchRestartModeEnum
		brmNone = 0
		brmManual = 1
		brmAuto = 2
	End Enum

	Private Enum MeasurementModeEnum
		mmNone = 0
		mmJoules = 1
		mmWatts = 2
	End Enum

	Private Enum AnalogOutFullscaleVoltageEnum
		aofvNone = 0
		aofv1V = 1
		aofv2V = 2
		aofv5V = 3
	End Enum
****************************************************************************/

/////////////////////////////////////////////////////////////////////////////


/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibInit
-------------------------------------------------------------------------------
DESCRIPTION:
	Initializes basic operations of the library driver.
	Must be called before anything other function.

PARAMETERS:
	None

RETURNS:
	VB_TRUE if successful and VB_FALSE otherwise.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibDeInit Lib "FieldMax2Lib"
****************************************************************************/
FIELDMAX2LIB_API VB_LONG __stdcall fm2LibInit(void);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibOpenDriver
-------------------------------------------------------------------------------
DESCRIPTION:
	Obtains a handle to the driver at the indicated index.

PARAMETERS:
	The index of the driver to access.

RETURNS:
	A handle to the driver or -1 if there was an error.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibOpenDriver Lib "FieldMax2Lib" Alias "_fm2LibOpenDriver@4" (ByVal index As Integer) As Long
****************************************************************************/
FIELDMAX2LIB_API VB_LONG __stdcall fm2LibOpenDriver(VB_INTEGER index);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibCloseDriver
-------------------------------------------------------------------------------
DESCRIPTION:
	Closes the driver indicated by the handle.

PARAMETERS:
	The handle previously returned by fm2LibOpenDriver().

RETURNS:
	VB_TRUE if successful and VB_FALSE otherwise.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibCloseDriver Lib "FieldMax2Lib" Alias "_fm2LibCloseDriver@4" (ByVal handle As Long) As Boolean
****************************************************************************/
FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibCloseDriver(VB_LONG h);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibDeInitDriver
-------------------------------------------------------------------------------
DESCRIPTION:
	DeInitializes basic operations of the library driver.
	Must be called before unloading dll from a process.

PARAMETERS:
	None

RETURNS:
	VB_TRUE if successful and VB_FALSE otherwise.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibDeInit Lib "FieldMax2Lib"
****************************************************************************/

FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibDeInit(void);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibGetStatusChangeList
-------------------------------------------------------------------------------
DESCRIPTION:
	Queries the status change list for changes since the last time it was
	called.

PARAMETERS:
	A pointer to the status change list array into which the result will be
	stored.

RETURNS:
	VB_TRUE if successful and VB_FALSE otherwise.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibGetStatusChangeList Lib "FieldMax2Lib" Alias "_fm2LibGetStatusChangeList@4" (ByRef list As StatusChangeType) As Boolean
****************************************************************************/
FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibGetStatusChangeList(StatusChangeType *pStatusChangeTypeList);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibGetString
-------------------------------------------------------------------------------
DESCRIPTION:
	Obtains the string (or multiple strings) from the USB input queue if one
	is waiting.

PARAMETERS:
	The driver handle, a pointer to the string buffer where the string will
	be stored, and a pointer indicating the size of the string.  pSize is
	an input and an output parameter.  As an input parameter, it indicates
	the size allocated to returnBuffer.  As an output parameter, it indicates
	the number of bytes that can be found in returnBuffer.

RETURNS:
	VB_TRUE if successful and VB_FALSE otherwise.  If no bytes are waiting,
	VB_FALSE is returned.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibGetString Lib "FieldMax2Lib" Alias "_fm2LibGetString@12" (ByVal handle as Long, ByRef returnBuffer As Byte, ByRef size as Integer) As Boolean
****************************************************************************/
FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibGetString(VB_LONG h,VB_STRING returnBuffer,VB_INTEGER *pSize);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibGetDataCount
-------------------------------------------------------------------------------
DESCRIPTION:
	Obtains the count of the number of data packets waiting to be read.

PARAMETERS:
	The driver handle and a pointer to the integer where the count will be
	stored.

RETURNS:
	VB_TRUE if successful and VB_FALSE otherwise.  If no data packets are
	waiting, VB_FALSE is returned.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibGetDataCount Lib "FieldMax2Lib" Alias "_fm2LibGetDataCount@8" (ByVal handle as Long, ByRef returnBuffer As Integer) As Boolean
****************************************************************************/
FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibGetDataCount(VB_LONG h,VB_INTEGER  *returnBuffer);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibGetData
-------------------------------------------------------------------------------
DESCRIPTION:
	Obtains the indicated number of data packets from the driver.

PARAMETERS:
	The driver handle, a pointer to the data packet array that will hold the
	data, and a pointer to an integer indicating the count of data packets
	to obtain (input parameter) and the number of data packets returned
	(output parameter).

RETURNS:
	VB_TRUE if successful and VB_FALSE otherwise.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibGetData Lib "FieldMax2Lib" Alias "_fm2LibGetData@12" (ByVal handle as Long, ByRef returnBuffer As DataPacketType, ByRef count as Integer) As Boolean
****************************************************************************/
FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibGetData(VB_LONG h,DataPacketType *returnBuffer,VB_INTEGER *count);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibFlush
-------------------------------------------------------------------------------
DESCRIPTION:
	Flushes the USB queue.

PARAMETERS:
	The driver handle.

RETURNS:
	Nothing.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Sub fm2LibFlush Lib "FieldMax2Lib" Alias "_fm2LibFlush@4" (ByVal handle as Long)
****************************************************************************/
FIELDMAX2LIB_API void __stdcall fm2LibFlush(VB_LONG h);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibSendCommand
-------------------------------------------------------------------------------
DESCRIPTION:
	Sends a string command to the USB driver.

PARAMETERS:
	The driver handle and the string to send.  Note that the CR/LF terminator
	is automatically appended to the string by this function when it is sent.

RETURNS:
	VB_TRUE if successful and VB_FALSE otherwise.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibSendCommand Lib "FieldMax2Lib" Alias "_fm2LibSendCommand@8" (ByVal handle as Long, ByVal command As String) As Boolean
****************************************************************************/
FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibSendCommand(VB_LONG h,VB_STRING command);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibCopyBufferToDataPacket
-------------------------------------------------------------------------------
DESCRIPTION:
	Copies the generic source buffer into the indicated data packet structure.

PARAMETERS:
	A pointer to the destination structure and a pointer to the source
	buffer.

RETURNS:
	Nothing.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Sub fm2LibCopyBufferToDataPacket Lib "FieldMax2Lib" Alias "_fm2LibCopyBufferToDataPacket@8" (ByRef packet As DataPacketType, ByRef buffer As Byte)
****************************************************************************/
FIELDMAX2LIB_API void __stdcall fm2LibCopyBufferToDataPacket(DataPacketType *pPacket,void *buffer);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibDataPacketIsOverrange
-------------------------------------------------------------------------------
DESCRIPTION:
	Determines if the indicated data packet represents an overrange signal.

PARAMETERS:
	A pointer to the data packet structure to test.

RETURNS:
	VB_TRUE if the data packet represents an overrange signal otherwise
	VB_FALSE.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibDataPacketIsOverrange Lib "FieldMax2Lib" Alias "_fm2LibDataPacketIsOverrange@4" (ByRef packet As DataPacketType) As Boolean
****************************************************************************/
FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibDataPacketIsOverrange(DataPacketType *pPacket);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibDataPacketIsAttention
-------------------------------------------------------------------------------
DESCRIPTION:
	Determines if the indicated data packet represents an attention signal.

PARAMETERS:
	A pointer to the data packet structure to test.

RETURNS:
	VB_TRUE if the data packet represents an attention signal otherwise
	VB_FALSE.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibDataPacketIsAttention Lib "FieldMax2Lib" Alias "_fm2LibDataPacketIsAttention@4" (ByRef packet As DataPacketType) As Boolean
****************************************************************************/
FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibDataPacketIsAttention(DataPacketType *pPacket);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibDataWasDropped
-------------------------------------------------------------------------------
DESCRIPTION:
	Determines if the indicated data packet represents an data dropped signal.

PARAMETERS:
	A pointer to the data packet structure to test.

RETURNS:
	VB_TRUE if the data packet represents an data dropped signal otherwise
	VB_FALSE.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibDataWasDropped Lib "FieldMax2Lib" Alias "_fm2LibDataWasDropped@4" (ByRef packet As DataPacketType) As Boolean
****************************************************************************/
FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibDataWasDropped(DataPacketType *pPacket);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibSync
-------------------------------------------------------------------------------
DESCRIPTION:
	Resynchronizes the USB queue.

PARAMETERS:
	The driver handle.

RETURNS:
	VB_TRUE if successful and VB_FALSE otherwise.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibSync Lib "FieldMax2Lib" Alias "_fm2LibSync@4" (ByVal handle as Long) As Boolean
****************************************************************************/
FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibSync(VB_LONG h);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibPackagedSendReply
-------------------------------------------------------------------------------
DESCRIPTION:
	Places the instrument in calibration mode, sends the indicated command,
	waits for a reply, and takes the instrument out of calibration mode.

PARAMETERS:
	The driver handle, a pointer to the buffer holding the command string, a
	pointer to the buffer where the serial number string will be stored, and
	a pointer to the buffer size.  pSize is an input and an output parameter.
	As an input parameter, it indicates the size allocated to returnBuffer.
	As an output parameter, it indicates the number of bytes that can be
	found in returnBuffer.

RETURNS:
	VB_TRUE if successful and VB_FALSE otherwise.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibPackagedSendReply Lib "FieldMax2Lib" Alias "_fm2LibPackagedSendReply@16" (ByVal handle as Long, ByVal commandBuffer As String, ByRef returnBuffer As Byte, ByRef size as Integer) As Boolean
****************************************************************************/
FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibPackagedSendReply(VB_LONG h,VB_STRING commandBuffer,VB_STRING returnBuffer,VB_INTEGER *pSize);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibGetSerialNumber
-------------------------------------------------------------------------------
DESCRIPTION:
	Obtains the device serial number.

PARAMETERS:
	The driver handle, a pointer to the buffer where the serial number string
	will be stored, and a pointer to the buffer size.  pSize is an input and
	an output parameter.  As an input parameter, it indicates the size
	allocated to returnBuffer.  As an output parameter, it indicates the
	number of bytes that can be found in returnBuffer.

RETURNS:
	VB_TRUE if successful and VB_FALSE otherwise.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibGetSerialNumber Lib "FieldMax2Lib" Alias "_fm2LibGetSerialNumber@12" (ByVal handle as Long, ByRef returnBuffer As Byte, ByRef size as Integer) As Boolean
****************************************************************************/
FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibGetSerialNumber(VB_LONG h,VB_STRING returnBuffer,VB_INTEGER *pSize);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibPutSerialNumber
-------------------------------------------------------------------------------
DESCRIPTION:
	Writes the device serial number.

PARAMETERS:
	The driver handle, a pointer to the buffer where the serial number string
	is stored, and the string length.

RETURNS:
	VB_TRUE if successful and VB_FALSE otherwise.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibPutSerialNumber Lib "FieldMax2Lib" Alias "_fm2LibPutSerialNumber@12" (ByVal handle as Long, ByVal serialNumber As Byte, ByVal size as Integer) As Boolean
****************************************************************************/
FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibPutSerialNumber(VB_LONG h,VB_STRING serialNumber,VB_INTEGER size);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibGetInstrumentState
-------------------------------------------------------------------------------
DESCRIPTION:
	Obtains the instrument state by querying the state and loading the
	indicated state buffer.  It uses single precision floats for all floating
	point values.

PARAMETERS:
	The driver handle and a pointer to the structure where the results will
	be stored.

RETURNS:
	VB_TRUE if successful and VB_FALSE otherwise.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibGetInstrumentState Lib "FieldMax2Lib" Alias "_fm2LibGetInstrumentState@8" (ByVal handle as Long, ByRef returnBuffer As InstrumentStateType) As Boolean
****************************************************************************/
FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibGetInstrumentState(VB_LONG h,InstrumentStateType *returnBuffer);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibGetInstrumentState2
-------------------------------------------------------------------------------
DESCRIPTION:
	Obtains the instrument state by querying the state and loading the
	indicated state buffer.  It uses double precision floats for all floating
	point values.

PARAMETERS:
	The driver handle and a pointer to the structure where the results will
	be stored.

RETURNS:
	VB_TRUE if successful and VB_FALSE otherwise.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibGetInstrumentState2 Lib "FieldMax2Lib" Alias "_fm2LibGetInstrumentState2@8" (ByVal handle as Long, ByRef returnBuffer As InstrumentStateType2) As Boolean
****************************************************************************/
FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibGetInstrumentState2(VB_LONG h,InstrumentStateType2 *returnBuffer);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibZeroStart
-------------------------------------------------------------------------------
DESCRIPTION:
	Starts a zero on the device.

PARAMETERS:
	The driver handle.

RETURNS:
	VB_TRUE if successful and VB_FALSE otherwise.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibZeroStart Lib "FieldMax2Lib" Alias "_fm2LibZeroStart@4" (ByVal handle as Long) As Boolean
****************************************************************************/
FIELDMAX2LIB_API VB_BOOLEAN __stdcall fm2LibZeroStart(VB_LONG h);

/*-----------------------------------------------------------------------------
--------------------------------------------------------------- fm2LibGetZeroReply
-------------------------------------------------------------------------------
DESCRIPTION:
	Gets the zero reply.

PARAMETERS:
	The driver handle.

RETURNS:
	0 if the reply has not happened yet, 1 if the reply indicates zeroing
	completed successfully, or 2 if the reply indicates zeroing completed
	with an error.
-----------------------------------------------------------------------------*/
/****************************************************************************
	VB declaration:

	Private Declare Function fm2LibGetZeroReply Lib "FieldMax2Lib" Alias "_fm2LibGetZeroReply@4" (ByVal handle as Long) As Integer
****************************************************************************/
FIELDMAX2LIB_API VB_INTEGER __stdcall fm2LibGetZeroReply(VB_LONG h);

#endif

