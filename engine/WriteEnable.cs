using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli.Exe
{
    public class WriteEnable
    {
        static private FLORENCE.Frame.Cli.Exe.Wrt.Execute_Control execute_Control = null;

        public WriteEnable() 
        {
            execute_Control = null;
        }

        public void Initialise_Control(
            FLORENCE.Frame.Cli.Global global,
            int numberOfCores
        )
        {
            execute_Control = new FLORENCE.Frame.Cli.Exe.Wrt.Execute_Control(global, numberOfCores);
            while (execute_Control == null) { /* wait untill created */ }
        }

        public void Write_End(
            FLORENCE.Frame.Cli.Exe.Wrt.Execute_Control execute_Control,
            int coreId,
            int numberOfCores,
            FLORENCE.Frame.Cli.Global global
        )
        {
            for (int index = 0; index < 2; index++)
            {
                execute_Control.SetFlag_writeState(coreId, index, global.GetConst_Write_IDLE(index));
            }
            execute_Control.Set_new_coreIdForWritePraiseIndex(execute_Control.Get_coreIdForWritePraiseIndex() + 1);
            if (execute_Control.Get_new_coreIdForWritePraiseIndex() == 3)
            {
                execute_Control.Set_new_coreIdForWritePraiseIndex(0);
            }
            execute_Control.WriteQue_Update(
                numberOfCores
            );
            execute_Control.WriteEnable_SortQue(
                numberOfCores,
                global
            );
            execute_Control.SetFlag_readWrite_Open(false);
        }

        public void Write_Start(
            FLORENCE.Frame.Cli.Exe.Wrt.Execute_Control execute_Control,
            int coreId,
            int numberOfCores,
            FLORENCE.Frame.Cli.Global global
        )
        {
            execute_Control.WriteEnable_Request(
                coreId,
                numberOfCores,
                global
            );
            execute_Control.WriteQue_Update(
                numberOfCores
            );
            execute_Control.WriteEnable_SortQue(
                numberOfCores,
                global
            );
            execute_Control.WriteEnable_Activate(
                coreId,
                global,
                numberOfCores
            );
        }

        public FLORENCE.Frame.Cli.Exe.Wrt.Execute_Control GetWriteEnable_Contorl()
        {
            return execute_Control;
        }
    }
}
