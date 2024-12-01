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
        static public FLORENCE.Frame.Cli.Exe.Wrt.WriteEnable_Control control_Of_WriteEnable = null;

        public WriteEnable() 
        {
            control_Of_WriteEnable = null;
        }

        public void Initialise_Control(
            FLORENCE.Frame.Cli.Global global,
            int numberOfCores
        )
        {
            control_Of_WriteEnable = new FLORENCE.Frame.Cli.Exe.Wrt.WriteEnable_Control(global, numberOfCores);
            while (control_Of_WriteEnable == null) { /* wait untill created */ }
        }

        public void Write_End(
            FLORENCE.Frame.Cli.Exe.Wrt.WriteEnable_Control control_Of_WriteEnable,
            int coreId,
            int numberOfCores,
            FLORENCE.Frame.Cli.Global global
        )
        {
            for (int index = 0; index < 2; index++)
            {
                control_Of_WriteEnable.SetFlag_writeState(coreId, index, global.GetConst_Write_IDLE(index));
            }
            control_Of_WriteEnable.Set_new_coreIdForWritePraiseIndex(control_Of_WriteEnable.Get_coreIdForWritePraiseIndex() + 1);
            if (control_Of_WriteEnable.Get_new_coreIdForWritePraiseIndex() == 3)
            {
                control_Of_WriteEnable.Set_new_coreIdForWritePraiseIndex(0);
            }
            control_Of_WriteEnable.WriteQue_Update(
                numberOfCores
            );
            control_Of_WriteEnable.WriteEnable_SortQue(
                numberOfCores,
                global
            );
            control_Of_WriteEnable.SetFlag_readWrite_Open(false);
        }

        public void Write_Start(
            FLORENCE.Frame.Cli.Exe.Wrt.WriteEnable_Control control_Of_WriteEnable,
            int coreId,
            int numberOfCores,
            FLORENCE.Frame.Cli.Global global
        )
        {
            control_Of_WriteEnable.WriteEnable_Request(
                coreId,
                numberOfCores,
                global
            );
            control_Of_WriteEnable.WriteQue_Update(
                numberOfCores
            );
            control_Of_WriteEnable.WriteEnable_SortQue(
                numberOfCores,
                global
            );
            control_Of_WriteEnable.WriteEnable_Activate(
                coreId,
                global,
                numberOfCores
            );
        }

        public FLORENCE.Frame.Cli.Exe.Wrt.WriteEnable_Control Get_Control_Of_WriteEnable()
        {
            return control_Of_WriteEnable;
        }
    }
}
