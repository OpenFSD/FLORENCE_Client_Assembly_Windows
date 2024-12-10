﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace FLORENCE.Frame.Cli.Exe
{
    public class Execute_Control
    {
        static private bool flag_SystemInitialised;
        static private bool[] flag_ThreadInitialised;

        public Execute_Control(int numberOfCores)
        {
            flag_SystemInitialised = new bool();
            flag_SystemInitialised = true;

            flag_ThreadInitialised = new bool[numberOfCores];
            for(Int16 index =0; index < numberOfCores; index++)
            {
                flag_ThreadInitialised[index] = new bool();
                flag_ThreadInitialised[index] = true;
            }
        }

        public bool GetFlag_SystemInitialised(Int16 numberOfCores)
        {
            for (int index = 0; index < numberOfCores; index++)
            {
                flag_SystemInitialised = false;
                if (flag_ThreadInitialised[index] == true)
                {
                    flag_SystemInitialised = true;
                }
            }
            return flag_SystemInitialised;
        }

        public bool GetFlag_ThreadInitialised(Int16 coreId)
        {
            return flag_ThreadInitialised[coreId];
        }

        public void SetConditionCodeOfThisThreadedCore(Int16 coreId)
        {
            //Todo
            SetFlag_ThreadInitialised(coreId);
        }

        public void SetFlag_ThreadInitialised(Int16 coreId)
        {
            flag_ThreadInitialised[coreId] = false;
        }
    }
}
