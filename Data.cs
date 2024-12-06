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
        static private FLORENCE.Frame.Cli.Dat.Input[] inputBuffer;
        static private FLORENCE.Frame.Cli.Dat.Map_Default mapDefault;
        static private FLORENCE.Frame.Cli.Dat.Output[] outputBuffer;
        static private FLORENCE.Frame.Cli.Dat.Settings settings;

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

            inputBuffer = new FLORENCE.Frame.Cli.Dat.Input[2];
            for(byte index = 0; index < 2; index++)
            {
                inputBuffer[index] = new FLORENCE.Frame.Cli.Dat.Input();
                while (inputBuffer[index] == null) { /* Wait while is created */ }
                inputBuffer[index].InitialiseControl();
            }
            
            inBufferToWrite = false;
            outBufferToWrite = false;

            outputBuffer = new FLORENCE.Frame.Cli.Dat.Output[2];
            for (byte index = 0; index < 2; index++)
            {
                outputBuffer[index] = new FLORENCE.Frame.Cli.Dat.Output();
                while (outputBuffer == null) { /* Wait while is created */ }
                //outputBuffer[index].InitialiseControl();
            }
            

            System.Console.WriteLine("FLORENCE: Data");
        }

        public FLORENCE.Frame.Cli.Dat.Arena GetArena()
        {
            return arena;
        }

        public bool GetInBufferToWrite()
        {
            return inBufferToWrite;
        }

        public bool GetOutBufferToWrite()
        {
            return outBufferToWrite;
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
    }
}
