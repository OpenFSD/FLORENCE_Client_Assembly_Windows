﻿using FLORENCE.Frame.Cli.Dat;
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
        static private FLORENCE.Frame.Cli.Exe.Execute_Control execute_Control;
        static private FLORENCE.Frame.Cli.Exe.WriteEnable writeEnable = null;
        private Thread[] concurrent = null;
        private Thread listenRespond = null;
        private Thread[] threads = null;

        public Execute(int numberOfCores) 
        {
            execute_Control = null;



            concurrent = new Thread[numberOfCores];//NUMBER OF CORES

        }

        public void Initialise_Control(
            int numberOfCores,
            Global global
        )
        {
            execute_Control = new FLORENCE.Frame.Cli.Exe.Execute_Control(numberOfCores);
            while (execute_Control == null) { /* Wait while is created */ }
        }

        public void Initialise()
        {
            Framework.GetClient().GetAlgorithms().Initialise(Framework.GetClient().GetGlobal().Get_NumCores());
            
            writeEnable = new FLORENCE.Frame.Cli.Exe.WriteEnable();
            while (writeEnable == null) { /* Wait while is created */ }
            writeEnable.Initialise_Control(
                 Framework.GetClient().GetGlobal(),
                 Framework.GetClient().GetGlobal().Get_NumCores()
             );
        }

        

        public void Initialise_Threads(
            int numberOfCores
        )
        {
            threads = new Thread[numberOfCores];
            threads[0] = System.Threading.Thread.CurrentThread;
            //Framework.GetClient().GetExecute().GetExecute_Control().SetConditionCodeOfThisThreadedCore(0);

            threads[1] = new Thread(Framework.GetClient().GetAlgorithms().GetIO_ListenRespond().Thread_io_ListenRespond);
            threads[1].Start();

            for(int index = 2; index < numberOfCores; index++)
            {
                threads[index] = new Thread(Framework.GetClient().GetAlgorithms().GetConcurrent(index).Thread_Concurrent);
                threads[index].Start();
            }
            /*while (Framework.GetClient().GetExecute().GetControlOfExecute().GetFlag_SystemInitialised(Framework.GetClient().GetGlobal().Get_NumCores()) != false)
            {
                /* wait while initialising */
            ///}
            
        }

        public void Create_And_Run_Graphics()
        {
            Framework.GetClient().GetData().GetOutputBuffer(Framework.GetClient().GetData().GetStateOfOutBufferWrite()).Initalise_Graphics();
        }

        public FLORENCE.Frame.Cli.Exe.Execute_Control GetExecute_Control()
        {
            return execute_Control;
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
