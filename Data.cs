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
        static private FLORENCE.Frame.Cli.Dat.Data_Control data_Control;
        static private FLORENCE.Frame.Cli.Dat.Arena arena;
        static private FLORENCE.Frame.Cli.Dat.Input[] inputBuffer;
        static private FLORENCE.Frame.Cli.Dat.Map_Default mapDefault;
        static private FLORENCE.Frame.Cli.Dat.Output[] outputBuffer;
        static private FLORENCE.Frame.Cli.Dat.Settings settings;
        static private FLORENCE.Frame.Cli.Dat.Input new_inputBuffer;
        static private FLORENCE.Frame.Cli.Dat.Output new_outputBuffer;
        static private List<FLORENCE.Frame.Cli.Dat.Input> stack_InputActions;
        static private List<FLORENCE.Frame.Cli.Dat.Output> stack_OutputRecieves;

        static private bool inBufferToWrite;
        static private bool outBufferToWrite;

        public Data()
        {
            settings = new FLORENCE.Frame.Cli.Dat.Settings();
            while (settings == null) { /* Wait while is created */ }

            mapDefault = new FLORENCE.Frame.Cli.Dat.Map_Default();
            while (mapDefault == null) { /* Wait while is created */ }

            arena = new FLORENCE.Frame.Cli.Dat.Arena();
            while (arena == null) { /* Wait while is created */ }

            new_inputBuffer = new FLORENCE.Frame.Cli.Dat.Input();
            while (new_inputBuffer == null) { /* Wait while is created */ }
            new_inputBuffer.InitialiseControl();

            inputBuffer = new FLORENCE.Frame.Cli.Dat.Input[2];
            for(byte index = 0; index < 2; index++)
            {
                inputBuffer[index] = new_inputBuffer;
                while (inputBuffer[index] == null) { /* Wait while is created */ }
            }
            
            inBufferToWrite = false;
            outBufferToWrite = false;

            new_outputBuffer = new FLORENCE.Frame.Cli.Dat.Output();
            while (new_outputBuffer == null) { /* Wait while is created */ }
            new_outputBuffer.InitialiseControl();

            outputBuffer = new FLORENCE.Frame.Cli.Dat.Output[2];
            for (byte index = 0; index < 2; index++)
            {
                outputBuffer[index] = new_outputBuffer;
                while (outputBuffer == null) { /* Wait while is created */ }
            }

            stack_InputActions = new List<FLORENCE.Frame.Cli.Dat.Input>();
            stack_OutputRecieves = new List<FLORENCE.Frame.Cli.Dat.Output>();

            System.Console.WriteLine("FLORENCE: Data");
        }

        public void InitialiseControl()
        {
            data_Control = new FLORENCE.Frame.Cli.Dat.Data_Control();
            while (data_Control == null) { /* Wait while is created */ }
        }

        public void Flip_InBufferToWrite()
        {
            inBufferToWrite = !inBufferToWrite;
        }
        public void Flip_OutBufferToWrite()
        {
             outBufferToWrite = !outBufferToWrite;
        }

        public FLORENCE.Frame.Cli.Dat.Arena GetArena()
        {
            return arena;
        }

        public Data_Control GetData_Control()
        {
            return data_Control;
        }

        public bool GetInBufferToWrite()
        {
            return inBufferToWrite;
        }

        public bool GetOutBufferToWrite()
        {
            return outBufferToWrite;
        }

        public List<FLORENCE.Frame.Cli.Dat.Input> Get_StackOfInputActions()
        {
            return stack_InputActions;
        }

        public List<FLORENCE.Frame.Cli.Dat.Output> Get_StackOfOutputsRecieved()
        {
            return stack_OutputRecieves;
        }

        public FLORENCE.Frame.Cli.Dat.Input GetInputBuffer(bool bufferToRead)
        {
            if (bufferToRead)
            {
                return inputBuffer[0];
            }
            else
            {
                return inputBuffer[1];
            }
        }

        public FLORENCE.Frame.Cli.Dat.Map_Default GetMapDefault()
        {
            return mapDefault;
        }

        public FLORENCE.Frame.Cli.Dat.Input GetNewInputBuffer()
        {
            return new_inputBuffer;
        }

        public FLORENCE.Frame.Cli.Dat.Output GetNewOutputBuffer()
        {
            return new_outputBuffer;
        }

        public FLORENCE.Frame.Cli.Dat.Output GetOutputBuffer(bool bufferToRead)
        {
            if (bufferToRead)
            {
                return outputBuffer[0];
            }
            else
            {
                return outputBuffer[1];
            }
        }

        public FLORENCE.Frame.Cli.Dat.Settings GetSettings()
        {
            return settings;
        }

        public void Set_New_InputBuffer(FLORENCE.Frame.Cli.Dat.Input value)
        {
            new_inputBuffer = value;
        }

        public void Set_New_OutputBuffer(FLORENCE.Frame.Cli.Dat.Output value)
        {
            new_outputBuffer = value;
        }
    }
}
