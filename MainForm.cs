// Copyright (c) 2008  Future Technology Devices International Ltd.
//
// Module Name
//
//     MainForm.cs
//
// Abstract:
//
//    C# program, to test the FTCJTAG.DLL. The FTCJTAG.DLL is used to control the FT2232H dual hi-speed device
//    and the FT4232H quad hi-speed device to simulate the Joint Test Action Group(JTAG) synchronous protocol to
//    communicate with JTAG devices connected to FT2232H dual hi-speed devices and FT4232H quad hi-speed devices.
//
// Revision History:
//
//   22/07/08    Kirke Ashton    Created.
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace FT2232HJTAGCSharpNETTestApp
{
    public partial class MainForm : Form
    {
        private const string App_Title = "FT2232/FT4232 JTAG Device C# .NET Test Application";

        private const string Dll_Version_Label = "FT2232/FT4232 JTAG DLL Version = ";

        private const string Device_Name_Label = "Device Name = ";

        private enum HI_SPEED_DEVICE_TYPES// : uint
        {
            FT2232H_DEVICE_TYPE = 1,
            FT4232H_DEVICE_TYPE = 2
        };

        private const uint FTC_SUCCESS = 0;
        private const uint FTC_DEVICE_IN_USE = 27;

        private const uint TEST_LOGIC_STATE = 1;
        private const uint RUN_TEST_IDLE_STATE = 2;

        private const uint MAX_NUM_DEVICE_NAME_CHARS = 100;
        private const uint MAX_NUM_CHANNEL_CHARS = 5;

        private const uint MAX_NUM_DLL_VERSION_CHARS = 10;
        private const uint MAX_NUM_ERROR_MESSAGE_CHARS = 100;

        private const uint WRITE_DATA_BUFFER_SIZE = 65536;
        private const uint READ_DATA_BUFFER_SIZE = 65536;
        private const uint READ_CMDS_DATA_BUFFER_SIZE = 131071;


        //**************************************************************************
        //
        // TYPE DEFINITIONS
        //
        //**************************************************************************

        public struct FTC_INPUT_OUTPUT_PINS
        {
            public bool bPin1InputOutputState;
            public bool bPin1LowHighState;
            public bool bPin2InputOutputState;
            public bool bPin2LowHighState;
            public bool bPin3InputOutputState;
            public bool bPin3LowHighState;
            public bool bPin4InputOutputState;
            public bool bPin4LowHighState;
        }

        public struct FTH_INPUT_OUTPUT_PINS
        {
            public bool bPin1InputOutputState;
            public bool bPin1LowHighState;
            public bool bPin2InputOutputState;
            public bool bPin2LowHighState;
            public bool bPin3InputOutputState;
            public bool bPin3LowHighState;
            public bool bPin4InputOutputState;
            public bool bPin4LowHighState;
            public bool bPin5InputOutputState;
            public bool bPin5LowHighState;
            public bool bPin6InputOutputState;
            public bool bPin6LowHighState;
            public bool bPin7InputOutputState;
            public bool bPin7LowHighState;
            public bool bPin8InputOutputState;
            public bool bPin8LowHighState;
        }

        public struct FTC_LOW_HIGH_PINS
        {
            public bool bPin1LowHighState;
            public bool bPin2LowHighState;
            public bool bPin3LowHighState;
            public bool bPin4LowHighState;
        }

        public struct FTH_LOW_HIGH_PINS
        {
            public bool bPin1LowHighState;
            public bool bPin2LowHighState;
            public bool bPin3LowHighState;
            public bool bPin4LowHighState;
            public bool bPin5LowHighState;
            public bool bPin6LowHighState;
            public bool bPin7LowHighState;
            public bool bPin8LowHighState;
        }

        public struct FTC_CLOSE_FINAL_STATE_PINS
        {
            public bool bTCKPinState;
            public bool bTCKPinActiveState;
            public bool bTDIPinState;
            public bool bTDIPinActiveState;
            public bool bTMSPinState;
            public bool bTMSPinActiveState;
        }


        //**************************************************************************
        //
        // FUNCTION IMPORTS FROM FTCJTAG DLL
        //
        //**************************************************************************

        [DllImport("ftcjtag.dll", EntryPoint = "JTAG_GetDllVersion", CallingConvention = CallingConvention.Cdecl)]
        static extern uint GetDllVersion(byte[] pDllVersion, uint buufferSize);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_GetErrorCodeString(string language, uint statusCode, byte[] pErrorMessage, uint bufferSize);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_GetNumHiSpeedDevices(ref uint NumHiSpeedDevices);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_GetHiSpeedDeviceNameLocIDChannel(uint deviceNameIndex, byte[] pDeviceName, uint deviceNameBufferSize, ref uint locationID, byte[] pChannel, uint channelBufferSize, ref uint hiSpeedDeviceType);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_OpenHiSpeedDevice(string DeviceName, uint locationID, string channel, ref IntPtr pftHandle);

        [DllImport("ftcjtag.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern uint JTAG_GetHiSpeedDeviceType(IntPtr ftHandle, ref uint hiSpeedDeviceType);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_Close(IntPtr ftHandle);

        [DllImport("ftcjtag.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern uint JTAG_CloseDevice(IntPtr ftHandle, ref FTC_CLOSE_FINAL_STATE_PINS pCloseFinalStatePinsData);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_InitDevice(IntPtr ftHandle, uint clockDivisor);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_TurnOnDivideByFiveClockingHiSpeedDevice(IntPtr ftHandle);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_TurnOffDivideByFiveClockingHiSpeedDevice(IntPtr ftHandle);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_TurnOnAdaptiveClockingHiSpeedDevice(IntPtr ftHandle);

        [DllImport("ftcjtag.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern uint JTAG_TurnOffAdaptiveClockingHiSpeedDevice(IntPtr ftHandle);

        [DllImport("ftcjtag.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern uint JTAG_SetDeviceLatencyTimer(IntPtr ftHandle, byte timerValue);

        [DllImport("ftcjtag.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern uint JTAG_GetDeviceLatencyTimer(IntPtr ftHandle, ref byte timerValue);

        [DllImport("ftcjtag.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern uint JTAG_GetHiSpeedDeviceClock(uint ClockDivisor, ref uint clockFrequencyHz);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_GetClock(uint clockDivisor, ref uint clockFrequencyHz);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_SetClock(IntPtr ftHandle, uint clockDivisor, ref uint clockFrequencyHz);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_SetLoopback(IntPtr ftHandle, bool loopBackState);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_SetHiSpeedDeviceGPIOs(IntPtr ftHandle, bool bControlLowInputOutputPins, ref FTC_INPUT_OUTPUT_PINS pLowInputOutputPinsData, bool bControlHighInputOutputPins, ref FTH_INPUT_OUTPUT_PINS pHighInputOutputPinsData);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_GetHiSpeedDeviceGPIOs(IntPtr ftHandle, bool bControlLowInputOutputPins, out FTC_LOW_HIGH_PINS pLowPinsInputData, bool bControlHighInputOutputPins, out FTH_LOW_HIGH_PINS pHighPinsInputData);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_Write(IntPtr ftHandle, bool bInstructionTestData, uint numBitsToWrite, byte[] WriteDataBuffer, uint numBytesToWrite, uint tapControllerState);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_Read(IntPtr ftHandle, bool bInstructionTestData, uint numBitsToRead, byte[] ReadDataBuffer, ref uint numBytesReturned, uint tapControllerState);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_WriteRead(IntPtr ftHandle, bool bInstructionTestData, uint numBitsToWriteRead, byte[] WriteDataBuffer, uint numBytesToWrite, byte[] ReadDataBuffer, ref uint numBytesReturned, uint tapControllerState);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_GenerateClockPulses(IntPtr ftHandle, uint numClockPulses);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_ClearCmdSequence();

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_AddWriteCmd(bool bInstructionTestData, uint numBitsToWrite, byte[] WriteDataBuffer, uint numBytesToWrite, uint tapControllerState);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_AddReadCmd(bool bInstructionTestData, uint numBitsToRead, uint tapControllerState);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_AddWriteReadCmd(bool bInstructionTestData, uint numBitsToWriteRead, byte[] WriteDataBuffer, uint numBytesToWrite, uint tapControllerState);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_ExecuteCmdSequence(IntPtr ftHandle, byte[] ReadCmdSequenceDataBuffer, ref uint numBytesReturned);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_ClearDeviceCmdSequence(IntPtr ftHandle);

        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_AddDeviceWriteCmd(IntPtr ftHandle, bool bInstructionTestData, uint numBitsToWrite, byte[] WriteDataBuffer, uint numBytesToWrite, uint tapControllerState);
        
        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_AddDeviceReadCmd(IntPtr ftHandle, bool bInstructionTestData, uint numBitsToRead, uint tapControllerState);
        
        [DllImport("ftcjtag.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern uint JTAG_AddDeviceWriteReadCmd(IntPtr ftHandle, bool bInstructionTestData, uint numBitsToWriteRead, byte[] WriteDataBuffer, uint numBytesToWrite, uint tapControllerState);
        

        public MainForm()
        {
            InitializeComponent();

            this.Text = App_Title;

            this.CommandSequenceMenuItem.CheckState = CheckState.Unchecked;

            this.ChannelComboBox.Items.Clear();
            this.ChannelComboBox.Items.Add("A");
            this.ChannelComboBox.Items.Add("B");
            this.ChannelComboBox.Text = "A";
            //this.ChannelComboBox.SelectedIndex = -1;
        }

        ~MainForm()
        {
        }

        private uint TestHiSpeedDeviceClock(IntPtr ftHandle) {
            uint ftStatus = FTC_SUCCESS;
            uint clockFrequencyHz = 0;

            if ((ftStatus = JTAG_GetHiSpeedDeviceClock(0, ref clockFrequencyHz)) == FTC_SUCCESS)
            {
                if ((ftStatus = JTAG_TurnOnDivideByFiveClockingHiSpeedDevice(ftHandle)) == FTC_SUCCESS)
                {
                    ftStatus = JTAG_GetHiSpeedDeviceClock(0, ref clockFrequencyHz);

                    if ((ftStatus = JTAG_SetClock(ftHandle, 0, ref clockFrequencyHz)) == FTC_SUCCESS)
                    {
                        ftStatus = JTAG_TurnOffDivideByFiveClockingHiSpeedDevice(ftHandle);

                        ftStatus = JTAG_SetClock(ftHandle, 1, ref clockFrequencyHz);
                    }
                }
            }

            return ftStatus;
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            uint ftStatus = FTC_SUCCESS;
            uint numHiSpeedDevices = 0; // 32-bit unsigned integer
            uint hiSpeedDeviceIndex = 0;
            uint locationID = 0;
            byte[] byteHiSpeedDeviceName = new byte[MAX_NUM_DEVICE_NAME_CHARS];
            byte[] byteHiSpeedDeviceChannel = new byte[MAX_NUM_CHANNEL_CHARS];
            uint hiSpeedDeviceType = 0;
            string hiSpeedDeviceName = null;
            string hiSpeedChannel = null;
            IntPtr ftHandle = IntPtr.Zero;
            byte timerValue = 0;
            uint clockFrequencyHz = 0;
            bool bLoopBackState = false;
            FTC_INPUT_OUTPUT_PINS LowInputOutputPinsData;
            FTH_INPUT_OUTPUT_PINS HighInputOutputPinsData;
            FTC_LOW_HIGH_PINS LowPinsInputData;
            FTH_LOW_HIGH_PINS HighPinsInputData;
            FTC_CLOSE_FINAL_STATE_PINS CloseFinalStatePinsData;
            Nullable<bool> bPerformCommandSequence = false;
            int iLoopCntr = 0;
            uint numBytesReturned = 0;
            byte[] WriteDataBuffer = new byte[WRITE_DATA_BUFFER_SIZE];
            byte[] ReadDataBuffer = new byte[READ_DATA_BUFFER_SIZE];
            byte[] ReadCmdSequenceDataBuffer = new byte[READ_CMDS_DATA_BUFFER_SIZE];
            byte[] byteDllVersion = new byte[MAX_NUM_DLL_VERSION_CHARS];
            string DllVersion; //"1.9";
            byte[] byteErrorMessage = new byte[MAX_NUM_ERROR_MESSAGE_CHARS];
            String ErrorMessage = null;

            string mismatchMsg = null;
            MessageBoxButtons MsgBoxButtons = MessageBoxButtons.OKCancel;
            DialogResult response = DialogResult.OK;

            // This function returns a ULONG value ie 32-bit unsigned integer
            //ftStatus = JTAG_GetDllVersion(byteDllVersion, MAX_NUM_DLL_VERSION_CHARS);
            ftStatus = GetDllVersion(byteDllVersion, MAX_NUM_DLL_VERSION_CHARS);

            DllVersion = Encoding.ASCII.GetString(byteDllVersion);
            // Trim strings to first occurrence of a null terminator character
            DllVersion = DllVersion.Substring(0, DllVersion.IndexOf("\0"));

            this.DLLVersionLabel.Text = Dll_Version_Label + DllVersion;
            this.DLLVersionLabel.Refresh();

            this.DeviceNameLabel.Text = Device_Name_Label;
            this.DeviceNameLabel.Refresh();

            this.FT2232HRadioButton.Checked = false;
            this.FT4232HRadioButton.Checked = false;

            this.PassFailResultsStatusLabel.Text = "";
            this.PassFailureStatusStrip.Refresh();
            Thread.Sleep(1000);

            // The parameter of this function is a pointer to a variable of type DWORD ie 32-bit unsigned integer
            ftStatus = JTAG_GetNumHiSpeedDevices(ref numHiSpeedDevices);

            if ((ftStatus == FTC_SUCCESS) && (numHiSpeedDevices > 0))
            {
                do {
                    ftStatus = JTAG_GetHiSpeedDeviceNameLocIDChannel(hiSpeedDeviceIndex, byteHiSpeedDeviceName, MAX_NUM_DEVICE_NAME_CHARS, ref locationID, byteHiSpeedDeviceChannel, MAX_NUM_CHANNEL_CHARS, ref hiSpeedDeviceType);

                    if (ftStatus == FTC_SUCCESS) {
                        hiSpeedChannel = Encoding.ASCII.GetString(byteHiSpeedDeviceChannel);
                        // Trim strings to first occurrence of a null terminator character
                        hiSpeedChannel = hiSpeedChannel.Substring(0, hiSpeedChannel.IndexOf("\0"));
                    }

                    hiSpeedDeviceIndex = hiSpeedDeviceIndex + 1;
                }
                while ((ftStatus == FTC_SUCCESS) && (hiSpeedDeviceIndex < numHiSpeedDevices) && ((hiSpeedChannel != null) && (hiSpeedChannel != this.ChannelComboBox.Text)));

                if (ftStatus == FTC_SUCCESS)
                {
                    if ((hiSpeedChannel != null) && (hiSpeedChannel != this.ChannelComboBox.Text))
                        ftStatus = FTC_DEVICE_IN_USE;
                }

                if (ftStatus == FTC_SUCCESS)
                {
                    hiSpeedDeviceName = Encoding.ASCII.GetString(byteHiSpeedDeviceName);
                    // Trim strings to first occurrence of a null terminator character
                    hiSpeedDeviceName = hiSpeedDeviceName.Substring(0, hiSpeedDeviceName.IndexOf("\0"));

                    // The ftHandle parameter is a pointer to a variable of type DWORD ie 32-bit unsigned integer
                    ftStatus = JTAG_OpenHiSpeedDevice(hiSpeedDeviceName, locationID, hiSpeedChannel, ref ftHandle);

                    if (ftStatus == FTC_SUCCESS)
                    {
                        this.DeviceNameLabel.Text = Device_Name_Label + hiSpeedDeviceName;
                        this.DeviceNameLabel.Refresh();

                        ftStatus = JTAG_GetHiSpeedDeviceType(ftHandle, ref hiSpeedDeviceType);

                        if (ftStatus == FTC_SUCCESS)
                        {
                            if (hiSpeedDeviceType == (uint)HI_SPEED_DEVICE_TYPES.FT4232H_DEVICE_TYPE)
                                this.FT4232HRadioButton.Checked = true;
                            else if (hiSpeedDeviceType == (uint)HI_SPEED_DEVICE_TYPES.FT2232H_DEVICE_TYPE)
                                this.FT2232HRadioButton.Checked = true;
                        }
                    }
                }

                if ((ftHandle != IntPtr.Zero) && (ftStatus == FTC_SUCCESS))
                {
                    ftStatus = JTAG_InitDevice(ftHandle, 0);

                    if (ftStatus == FTC_SUCCESS) {
                        //ftStatus = JTAG_TurnOnAdaptiveClockingHiSpeedDevice(ftHandle);

                        //ftStatus = JTAG_TurnOffAdaptiveClockingHiSpeedDevice(ftHandle);
                    }

                    if (ftStatus == FTC_SUCCESS) {
                        if ((ftStatus = JTAG_GetDeviceLatencyTimer(ftHandle, ref timerValue)) == FTC_SUCCESS)
                        {
                            if ((ftStatus = JTAG_SetDeviceLatencyTimer(ftHandle, 50)) == FTC_SUCCESS)
                            {
                                ftStatus = JTAG_GetDeviceLatencyTimer(ftHandle, ref timerValue);

                                ftStatus = JTAG_SetDeviceLatencyTimer(ftHandle, 16);

                                ftStatus = JTAG_GetDeviceLatencyTimer(ftHandle, ref timerValue);
                            }
                        }
                    }

                    if (ftStatus == FTC_SUCCESS) {
                        ftStatus = TestHiSpeedDeviceClock(ftHandle);
                    }

                    if (ftStatus == FTC_SUCCESS) {
                        ftStatus = JTAG_SetLoopback(ftHandle, bLoopBackState);
                    }

                    if (ftStatus == FTC_SUCCESS) {
                        LowInputOutputPinsData.bPin1InputOutputState = true;
                        LowInputOutputPinsData.bPin2InputOutputState = true;
                        LowInputOutputPinsData.bPin3InputOutputState = true;
                        LowInputOutputPinsData.bPin4InputOutputState = true;
                        LowInputOutputPinsData.bPin1LowHighState = true;
                        LowInputOutputPinsData.bPin2LowHighState = true;
                        LowInputOutputPinsData.bPin3LowHighState = true;
                        LowInputOutputPinsData.bPin4LowHighState = true;

                        HighInputOutputPinsData.bPin1InputOutputState = true;
                        HighInputOutputPinsData.bPin2InputOutputState = true;
                        HighInputOutputPinsData.bPin3InputOutputState = true;
                        HighInputOutputPinsData.bPin4InputOutputState = true;
                        HighInputOutputPinsData.bPin5InputOutputState = true;
                        HighInputOutputPinsData.bPin6InputOutputState = true;
                        HighInputOutputPinsData.bPin7InputOutputState = true;
                        HighInputOutputPinsData.bPin8InputOutputState = true;
                        HighInputOutputPinsData.bPin1LowHighState = false;
                        HighInputOutputPinsData.bPin2LowHighState = true;
                        HighInputOutputPinsData.bPin3LowHighState = true;
                        HighInputOutputPinsData.bPin4LowHighState = false;
                        HighInputOutputPinsData.bPin5LowHighState = false;
                        HighInputOutputPinsData.bPin6LowHighState = true;
                        HighInputOutputPinsData.bPin7LowHighState = true;
                        HighInputOutputPinsData.bPin8LowHighState = false;

                        if (hiSpeedDeviceType == (uint)HI_SPEED_DEVICE_TYPES.FT2232H_DEVICE_TYPE)
                            ftStatus = JTAG_SetHiSpeedDeviceGPIOs(ftHandle, true, ref LowInputOutputPinsData, true, ref HighInputOutputPinsData);
                        else
                            ftStatus = JTAG_SetHiSpeedDeviceGPIOs(ftHandle, true, ref LowInputOutputPinsData, false, ref HighInputOutputPinsData);

                        // Sleep for 1 second
                        Thread.Sleep(1000);

                        if (ftStatus == FTC_SUCCESS) {
                            if (hiSpeedDeviceType == (uint)HI_SPEED_DEVICE_TYPES.FT2232H_DEVICE_TYPE)
                                ftStatus = JTAG_GetHiSpeedDeviceGPIOs(ftHandle, true, out LowPinsInputData, true, out HighPinsInputData);
                            else
                                ftStatus = JTAG_GetHiSpeedDeviceGPIOs(ftHandle, true, out LowPinsInputData, false, out HighPinsInputData);
                        }

                        if (ftStatus == FTC_SUCCESS)
                        {
                            WriteDataBuffer[0] = 0x01;
                            WriteDataBuffer[1] = 0x01;
                            WriteDataBuffer[2] = 0x01;
                            WriteDataBuffer[3] = 0x01;

                            if (this.CommandSequenceMenuItem.CheckState == CheckState.Unchecked)
                            {
                                do
                                {
                                    if ((ftStatus = JTAG_Write(ftHandle, true, 32, WriteDataBuffer, 4, RUN_TEST_IDLE_STATE)) == FTC_SUCCESS)
                                        ftStatus = JTAG_GenerateClockPulses(ftHandle, 100);

                                    if (ftStatus == FTC_SUCCESS)
                                    {
                                        // Sleep for 10 milliseconds
                                        Thread.Sleep(10);

                                        if ((ftStatus = JTAG_Read(ftHandle, true, 32, ReadDataBuffer, ref numBytesReturned, RUN_TEST_IDLE_STATE)) == FTC_SUCCESS)
                                        {
                                            if ((ftStatus = JTAG_WriteRead(ftHandle, true, 32, WriteDataBuffer, 4, ReadDataBuffer, ref numBytesReturned, RUN_TEST_IDLE_STATE)) == FTC_SUCCESS)
                                            {
                                                if ((WriteDataBuffer[0] != ReadDataBuffer[0]) || (WriteDataBuffer[1] != ReadDataBuffer[1]) ||
                                                    (WriteDataBuffer[2] != ReadDataBuffer[2]) || (WriteDataBuffer[3] != ReadDataBuffer[3]))
                                                {
                                                    byte mismatchValue = 0;

                                                    if (WriteDataBuffer[0] != ReadDataBuffer[0])
                                                        mismatchValue = ReadDataBuffer[0];
                                                    else if (WriteDataBuffer[1] != ReadDataBuffer[1])
                                                        mismatchValue = ReadDataBuffer[1];
                                                    else if (WriteDataBuffer[2] != ReadDataBuffer[2])
                                                        mismatchValue = ReadDataBuffer[2];
                                                    else if (WriteDataBuffer[3] != ReadDataBuffer[3])
                                                        mismatchValue = ReadDataBuffer[3];

                                                    mismatchMsg = String.Format("JTAG Write/Read mismatch, Loop = {0}, Write Value = {1}, Read Value = {2}", iLoopCntr, 1, mismatchValue);

                                                    response = MessageBox.Show(mismatchMsg, "JTAG Read Error Message", MsgBoxButtons);
                                                }
                                            }
                                        }
                                    }

                                    iLoopCntr++;

                                    // Sleep for 50 milliseconds
                                    Thread.Sleep(50);
                                }
                                while ((iLoopCntr < 20) && (ftStatus == FTC_SUCCESS) && (response == DialogResult.OK));
                            }
                            else
                            {
                                if (numHiSpeedDevices == 1)
                                {
                                    ftStatus = JTAG_ClearCmdSequence();

                                    while ((iLoopCntr < 10) && (ftStatus == FTC_SUCCESS))
                                    {
                                        if ((ftStatus = JTAG_AddWriteCmd(true, 32, WriteDataBuffer, 4, RUN_TEST_IDLE_STATE)) == FTC_SUCCESS)
                                            ftStatus = JTAG_AddReadCmd(true, 32, RUN_TEST_IDLE_STATE);

                                        if (ftStatus == FTC_SUCCESS)
                                            ftStatus = JTAG_AddWriteReadCmd(true, 32, WriteDataBuffer, 4, RUN_TEST_IDLE_STATE);

                                        iLoopCntr++;
                                    }
                                }
                                else
                                {
                                    ftStatus = JTAG_ClearDeviceCmdSequence(ftHandle);

                                    while ((iLoopCntr < 10) && (ftStatus == FTC_SUCCESS))
                                    {
                                        if ((ftStatus = JTAG_AddDeviceWriteCmd(ftHandle, true, 32, WriteDataBuffer, 4, RUN_TEST_IDLE_STATE)) == FTC_SUCCESS)
                                            ftStatus = JTAG_AddDeviceReadCmd(ftHandle, true, 32, RUN_TEST_IDLE_STATE);

                                        if (ftStatus == FTC_SUCCESS)
                                            ftStatus = JTAG_AddDeviceWriteReadCmd(ftHandle, true, 32, WriteDataBuffer, 4, RUN_TEST_IDLE_STATE);

                                        iLoopCntr = iLoopCntr + 1;
                                    }
                                }

                                if (ftStatus == FTC_SUCCESS)
                                    ftStatus = JTAG_ExecuteCmdSequence(ftHandle, ReadCmdSequenceDataBuffer, ref numBytesReturned);
                            }
                        }
                    }
                }
            }

            if (ftHandle != IntPtr.Zero)
            {
                //CloseFinalStatePinsData.bTCKPinState = true;
                //CloseFinalStatePinsData.bTCKPinActiveState = true;
                //CloseFinalStatePinsData.bTDIPinState = true;
                //CloseFinalStatePinsData.bTDIPinActiveState = false;
                //CloseFinalStatePinsData.bTMSPinState = true;
                //CloseFinalStatePinsData.bTMSPinActiveState = true;

                //ftStatus = JTAG_CloseDevice(ftHandle, ref CloseFinalStatePinsData);

                JTAG_Close(ftHandle);
                ftHandle = IntPtr.Zero;
            }

            if ((ftStatus != FTC_SUCCESS) || (numHiSpeedDevices == 0) || (mismatchMsg != null))
            {
                if (ftStatus != FTC_SUCCESS) {
                    ErrorMessage = "Status Code(" + ftStatus.ToString() + ") - ";

                    ftStatus = JTAG_GetErrorCodeString("EN", ftStatus, byteErrorMessage, MAX_NUM_ERROR_MESSAGE_CHARS);

                    ErrorMessage = ErrorMessage + Encoding.ASCII.GetString(byteErrorMessage);
                    // Trim strings to first occurrence of a null terminator character
                    ErrorMessage = ErrorMessage.Substring(0, ErrorMessage.IndexOf("\0"));

                    MessageBox.Show(ErrorMessage);
                } else {
                    if (numHiSpeedDevices == 0)
                    {
                        ErrorMessage = "There are no devices connected.";

                        MessageBox.Show(ErrorMessage);
                    }
                    else
                    {
                        ErrorMessage = mismatchMsg;
                    }
                }

                this.PassFailResultsStatusLabel.Text = "Fail - " + ErrorMessage;
                this.PassFailureStatusStrip.Refresh();

                //Console.WriteLine(ErrorMessage);
                Console.WriteLine("Fail - {0}", ErrorMessage);
            } else {
                this.PassFailResultsStatusLabel.Text = "Pass";
                this.PassFailureStatusStrip.Refresh();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void CommandSequenceMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CommandSequenceMenuItem.CheckState == CheckState.Unchecked)
                this.CommandSequenceMenuItem.CheckState = CheckState.Checked;
            else
                this.CommandSequenceMenuItem.CheckState = CheckState.Unchecked;
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}