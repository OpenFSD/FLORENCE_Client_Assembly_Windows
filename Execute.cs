using FLORENCE.Frame.Cli.Dat;
using FLORENCE.Frame.Cli.Exe;
using FLORENCE.Frame.Cli.Exe.Wrt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace FLORENCE.Frame.Cli
{
    public class Execute
    {
        static private FLORENCE.Frame.Cli.Exe.WriteEnable writeEnable;
        private Thread[] threads;
        private Thread new_thread;

        public Execute(int numberOfCores) 
        {
            writeEnable = new FLORENCE.Frame.Cli.Exe.WriteEnable();
            while (writeEnable == null) { /* Wait while is created */ }
            writeEnable.Initialise_Control(
                Framework.GetClient().GetGlobal(),
                numberOfCores
            );
            Thread[] threads = new Thread[numberOfCores];//NUMBER OF CORES

        }

        public void Initialise_Control(
            int numberOfCores,
            Global global
        )
        {

        }

        public void Initialise(
            int numberOfCores        
        )
        {
            Framework.GetClient().GetAlgorithms().Initialise(
                Framework.GetClient().GetGlobal().Get_NumCores()
            );
        }

        

        public void Initialise_Threads(
            int numberOfCores
        )
        {
            this.threads[0] = System.Threading.Thread.CurrentThread;

            this.threads = new Thread[numberOfCores];
            this.new_thread = new Thread(Algo.IO_ListenRespond.Thread_io_ListenRespond);
            this.new_thread.Start();
            this.threads[1] = this.new_thread;

            for(int index = 2; index < numberOfCores; index++)
            {
                this.new_thread = new Thread(Algo.Concurrent.Thread_concurrent);
                this.new_thread.Start();
                this.threads[index] = this.new_thread;
            }
            //while (Framework.GetClient().GetExecute().GetControlOfExecute().GetFlag_SystemInitialised(Framework.GetClient().GetGlobal().Get_NumCores()) != false)
            //{

            //}
        }

        public void Create_And_Run_Graphics()
        {
            Framework.GetClient().GetData().GetOutput().Initalise_Graphics();
        }

        public Thread GetThread(int index)
        {
            return threads[index];
        }

        public FLORENCE.Frame.Cli.Exe.WriteEnable GetWriteEnable()
        {
            return writeEnable;
        }
    }   
}
