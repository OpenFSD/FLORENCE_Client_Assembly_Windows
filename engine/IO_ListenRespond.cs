﻿using FLORENCE.Frame.Cli.Algo.In;
using FLORENCE.Frame.Cli.Dat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli.Algo
{ 
    public class IO_ListenRespond
    {
        static private Int16 threadId = 1;
        static private FLORENCE.Frame.Cli.Algo.In.IO_ListenRespond_Control io_Control;

        public IO_ListenRespond() 
        {
            io_Control = null;
        }
        public void InitialiseControl()
        {
            io_Control = new FLORENCE.Frame.Cli.Algo.In.IO_ListenRespond_Control();
            while (io_Control == null) { /* Wait while is created */ }
        }

        public void Thread_io_ListenRespond()
        {
            //Framework.GetClient().GetExecute().GetExecute_Control().SetConditionCodeOfThisThreadedCore(threadId);
//while (Framework.GetClient().GetExecute().GetExecute_Control().GetFlag_SystemInitialised(Framework.GetClient().GetGlobal().Get_NumCores()) != false)
            //{
                // wait untill ALL threads initalised in preperation of system init.
            //}
            while (true)
            {
                switch (Framework.GetClient().GetAlgorithms().GetIO_ListenRespond().GetIO_Control().GetFlag_IO_ThreadState())
                {
                    case true:
                        {

                            break;
                        }
                    case false:
                        {
                        
                            break;
                        }
                }
            }
        }
        public FLORENCE.Frame.Cli.Algo.In.IO_ListenRespond_Control GetIO_Control()
        {
            return io_Control;
        }
    }
}
