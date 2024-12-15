using FLORENCE.Frame.Cli.Dat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli
{
    public class Data
    {
        static private FLORENCE.Frame.Cli.Dat.Arena arena;
        static private FLORENCE.Frame.Cli.Dat.Data_Control data_Control;
        static private FLORENCE.Frame.Cli.Dat.Input empty_InputBuffer;
        static private FLORENCE.Frame.Cli.Dat.Output empty_OutputBuffer;
        static private FLORENCE.Frame.Cli.Dat.Input[] inputDoubleBuffer;
        static private FLORENCE.Frame.Cli.Dat.Input inputThirdBuffer;
        static private FLORENCE.Frame.Cli.Dat.Output[] outputDoubleBuffer;
        static private FLORENCE.Frame.Cli.Dat.Output outputThirdBuffer;
        static private FLORENCE.Frame.Cli.Dat.Settings settings;
        static private List<FLORENCE.Frame.Cli.Dat.Input> stack_InputActions;
        static private List<FLORENCE.Frame.Cli.Dat.Output> stack_OutputRecieves;
        static private FLORENCE.Frame.Cli.Dat.Input transmitInputBuffer;
        static private FLORENCE.Frame.Cli.Dat.User_I user_IO;
        static private FLORENCE.Frame.Cli.Dat.Map_Default mapDefault;

        static private bool state_InBufferToWrite;
        static private bool state_OutBufferToWrite;

        public Data()
        {
            arena = new FLORENCE.Frame.Cli.Dat.Arena();
            while (arena == null) { /* Wait while is created */ }

            data_Control = null;

            empty_InputBuffer = new FLORENCE.Frame.Cli.Dat.Input();
            while (arena == null) { /* Wait while is created */ }
            empty_InputBuffer.InitialiseControl();

            empty_OutputBuffer = new FLORENCE.Frame.Cli.Dat.Output();
            while (arena == null) { /* Wait while is created */ }
            empty_OutputBuffer.InitialiseControl();

            inputDoubleBuffer = new FLORENCE.Frame.Cli.Dat.Input[2];
            for (byte index = 0; index < 2; index++)
            {
                inputDoubleBuffer[index] = empty_InputBuffer;
                while (inputDoubleBuffer[index] == null) { /* Wait while is created */ }
            }

            inputThirdBuffer = new FLORENCE.Frame.Cli.Dat.Input();
            while (inputThirdBuffer == null) { /* Wait while is created */ }
            inputThirdBuffer.InitialiseControl();

            outputDoubleBuffer = new FLORENCE.Frame.Cli.Dat.Output[2];
            for (byte index = 0; index < 2; index++)
            {
                outputDoubleBuffer[index] = empty_OutputBuffer;
                while (outputDoubleBuffer == null) { /* Wait while is created */ }
            }

            outputThirdBuffer = new FLORENCE.Frame.Cli.Dat.Output();
            while (outputThirdBuffer == null) { /* Wait while is created */ }
            outputThirdBuffer.InitialiseControl();

            settings = new FLORENCE.Frame.Cli.Dat.Settings();
            while (settings == null) { /* Wait while is created */ }

            stack_InputActions = new List<FLORENCE.Frame.Cli.Dat.Input>();
            while (stack_InputActions == null) { /* Wait while is created */ }

            stack_OutputRecieves = new List<FLORENCE.Frame.Cli.Dat.Output>();
            while (stack_OutputRecieves == null) { /* Wait while is created */ }

            transmitInputBuffer = new FLORENCE.Frame.Cli.Dat.Input();
            while (transmitInputBuffer == null) { /* Wait while is created */ }

            user_IO = new FLORENCE.Frame.Cli.Dat.User_I();
            while (user_IO == null) { /* Wait while is created */ }

            mapDefault = new FLORENCE.Frame.Cli.Dat.Map_Default();
            while (mapDefault == null) { /* Wait while is created */ }
     
            state_InBufferToWrite = false;
            state_OutBufferToWrite = false;

            System.Console.WriteLine("FLORENCE: Data");
        }

        public Int16 BoolToInt16(bool value)
        {
            Int16 temp = new Int16();
            if (value)
            {
                temp = (Int16)1;
            }
            else if (!value)
            {
                temp = (Int16)0;
            }
            return temp;
        }

        public void InitialiseControl()
        {
            data_Control = new FLORENCE.Frame.Cli.Dat.Data_Control();
            while (data_Control == null) { /* Wait while is created */ }
        }

        public void Flip_InBufferToWrite()
        {
            state_InBufferToWrite = !state_InBufferToWrite;
        }
        public void Flip_OutBufferToWrite()
        {
             state_OutBufferToWrite = !state_OutBufferToWrite;
        }

        public FLORENCE.Frame.Cli.Dat.Arena GetArena()
        {
            return arena;
        }

        public FLORENCE.Frame.Cli.Dat.Data_Control GetData_Control()
        {
            return data_Control;
        }

        public FLORENCE.Frame.Cli.Dat.Input GetEmptyInput()
        {
            return empty_InputBuffer;   
        }
        public FLORENCE.Frame.Cli.Dat.Output GetEmptyOutput()
        {
            return empty_OutputBuffer;
        }
        public bool GetStateOfOutBufferWrite()
        {
            return state_OutBufferToWrite;
        }

        public bool GetStateOfInBufferWrite()
        {
            return state_InBufferToWrite;
        }



        public FLORENCE.Frame.Cli.Dat.Input GetInputBuffer(bool bufferToRead)
        {
            if (bufferToRead)
            {
                return inputDoubleBuffer[0];
            }
            else
            {
                return inputDoubleBuffer[1];
            }
        }

        public FLORENCE.Frame.Cli.Dat.Map_Default GetMapDefault()
        {
            return mapDefault;
        }

        public FLORENCE.Frame.Cli.Dat.Input GetThirdInputBuffer()
        {
            return inputThirdBuffer;
        }

        public FLORENCE.Frame.Cli.Dat.Output GetThirdOutputBuffer()
        {
            return outputThirdBuffer;
        }

        public FLORENCE.Frame.Cli.Dat.Output GetOutputBuffer(bool bufferToRead)
        {
            if (bufferToRead)
            {
                return outputDoubleBuffer[0];
            }
            else
            {
                return outputDoubleBuffer[1];
            }
        }

        public FLORENCE.Frame.Cli.Dat.Settings GetSettings()
        {
            return settings;
        }
        public List<FLORENCE.Frame.Cli.Dat.Input> GetStackOfInputActions()
        {
            return stack_InputActions;
        }

        public List<FLORENCE.Frame.Cli.Dat.Output> GetStackOfOutputsRecieved()
        {
            return stack_OutputRecieves;
        }

        public FLORENCE.Frame.Cli.Dat.Input GetTransmitInputBuffer()
        {
            return transmitInputBuffer;
        }

        public FLORENCE.Frame.Cli.Dat.User_I GetUserIO()
        {
            return user_IO;
        }

        public void SetInputBuffer(FLORENCE.Frame.Cli.Dat.Input value)
        {
            inputDoubleBuffer[BoolToInt16(Framework.GetClient().GetData().GetStateOfInBufferWrite())] = value;
        }

        public void SetOutputBuffer(FLORENCE.Frame.Cli.Dat.Output value)
        {
            outputDoubleBuffer[BoolToInt16(Framework.GetClient().GetData().GetStateOfInBufferWrite())] = value;
        }

        public void SetThirdInputBuffer(FLORENCE.Frame.Cli.Dat.Input value)
        {
            inputThirdBuffer = value;
        }

        public void SetThirdOutputBuffer(FLORENCE.Frame.Cli.Dat.Output value)
        {
            outputThirdBuffer = value;
        }

        public void SetTransmitInputBuffer(FLORENCE.Frame.Cli.Dat.Input value)
        {
            transmitInputBuffer = value;
        }
    }
}
