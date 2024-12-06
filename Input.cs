using FLORENCE.Frame.Cli.Dat.In;
using FLORENCE.Frame.Cli.Dat.Out.Gfx;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli.Dat
{
    public class Input
    {
        static private FLORENCE.Frame.Cli.Dat.In.Input_Control input_Control;
        private FLORENCE.Frame.Cli.Dat.In.Player player;
        static private Int16 praiseEventId;
        static private Object praiseInputBuffer_Subset;
//======
//======
        static private FLORENCE.Frame.Cli.Dat.In.Praise0_Input praise0_Input;
        static private FLORENCE.Frame.Cli.Dat.In.Praise1_Input praise1_Input;
//======
//======
        public Input()
        {
            input_Control = null;

            praiseEventId = new int();
            praiseEventId = 0;

            praiseInputBuffer_Subset = null;

            praise0_Input = new FLORENCE.Frame.Cli.Dat.In.Praise0_Input();
            praise1_Input = new FLORENCE.Frame.Cli.Dat.In.Praise1_Input();

            player = new FLORENCE.Frame.Cli.Dat.In.Player();
            while (player == null) { /* Wait while is created */ }

            System.Console.WriteLine("FLORENCE: Input");
        }

        public void InitialiseControl() 
        {
            input_Control = new FLORENCE.Frame.Cli.Dat.In.Input_Control();
            while (input_Control == null) { /* Wait while is created */ }
        }

        public Object Get_InputBufferSubset()
        {
            return praiseInputBuffer_Subset;
        }

        public FLORENCE.Frame.Cli.Dat.In.Input_Control GetInputControl()
        {
            return input_Control;
        }

        public FLORENCE.Frame.Cli.Dat.In.Player GetPlayer() 
        { 
            return player; 
        }

        public int GetPraiseEventId() 
        {   
            return praiseEventId; 
        }
//======
//======
        public FLORENCE.Frame.Cli.Dat.In.Praise0_Input GetPraise0_Input()
        {
            return praise0_Input;
        }

        public FLORENCE.Frame.Cli.Dat.In.Praise1_Input GetPraise1_Input()
        {
            return praise1_Input;
        }
//======
//======
        public void Set_InputBuffer_SubSet(Object value)
        {
            praiseInputBuffer_Subset = value;
        }

        public void SetPraiseEventId(Int16 value)
        {
            praiseEventId = value;
        }
    }
}