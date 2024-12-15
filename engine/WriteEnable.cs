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
        static private FLORENCE.Frame.Cli.Exe.Wrt.WriteEnable_Control write_Control = null;

        public WriteEnable() 
        {
            write_Control = null;
        }

        public void Initialise_Control(
            FLORENCE.Frame.Cli.Global global,
            int numberOfCores
        )
        {
            write_Control = new FLORENCE.Frame.Cli.Exe.Wrt.WriteEnable_Control(global, numberOfCores);
            while (write_Control == null) { /* wait untill created */ }
        }

        public void Write_End(
            FLORENCE.Frame.Cli.Exe.Wrt.WriteEnable_Control write_Control,
            int coreId,
            int numberOfCores,
            FLORENCE.Frame.Cli.Global global
        )
        {
            for (int index = 0; index < 2; index++)
            {
                write_Control.SetFlag_writeState(coreId, index, global.GetConst_Write_IDLE(index));
            }
            write_Control.Set_new_coreIdForWritePraiseIndex(write_Control.Get_coreIdForWritePraiseIndex() + 1);
            if (write_Control.Get_new_coreIdForWritePraiseIndex() == 3)
            {
                write_Control.Set_new_coreIdForWritePraiseIndex(0);
            }
            write_Control.WriteQue_Update(
                numberOfCores
            );
            write_Control.WriteEnable_SortQue(
                numberOfCores,
                global
            );
            write_Control.SetFlag_readWrite_Open(false);
        }

        public void Write_Start(
            FLORENCE.Frame.Cli.Exe.Wrt.WriteEnable_Control write_Control,
            int coreId,
            int numberOfCores,
            FLORENCE.Frame.Cli.Global global
        )
        {
            write_Control.WriteEnable_Request(
                coreId,
                numberOfCores,
                global
            );
            write_Control.WriteQue_Update(
                numberOfCores
            );
            write_Control.WriteEnable_SortQue(
                numberOfCores,
                global
            );
            write_Control.WriteEnable_Activate(
                coreId,
                global,
                numberOfCores
            );
        }

        public FLORENCE.Frame.Cli.Exe.Wrt.WriteEnable_Control GetWriteEnable_Contorl()
        {
            return write_Control;
        }
    }
}
