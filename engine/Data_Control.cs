﻿using FLORENCE.Frame.Cli.Dat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli.Dat
{
    public class Data_Control
    {
        static private bool flag_InputStackLoaded;
        static private bool flag_OutputStackLoaded;

        public Data_Control() 
        { 
        
        }

        public void PopFromStackOfInputActions(
            FLORENCE.Frame.Cli.Dat.Input reference,
            List<FLORENCE.Frame.Cli.Dat.Input> inputStack
        )
        {
            reference = inputStack[0];
            inputStack.RemoveAt(0);
        }

        public void PopFromStackOfOutputReturns()
        {

        }

        public void PushToStackOfInputActions(
            List<FLORENCE.Frame.Cli.Dat.Input> inputStack,
            FLORENCE.Frame.Cli.Dat.Input praiseBuffer
        )
        {
            inputStack.Add(praiseBuffer);
        }

        public void PushToStackOfOutputReturns(
            List<FLORENCE.Frame.Cli.Dat.Output> outputStack,
            FLORENCE.Frame.Cli.Dat.Input praiseBuffer
        )
        {
            
        }

        public bool GetFlag_InputStackLoaded()
        {
            return flag_InputStackLoaded;
        }

        public bool GetFlag_OutputStackLoaded()
        {
            return flag_OutputStackLoaded;
        }

        public void SetFlag_InputStackLoaded(bool value)
        {
            flag_InputStackLoaded = value;
        }
        public void SetFlag_OutputStackLoaded(bool value)
        {
            flag_OutputStackLoaded = value;
        }
    }
}
