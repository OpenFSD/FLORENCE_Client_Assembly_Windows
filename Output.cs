using FLORENCE.Frame.Cli.Dat.In;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli.Dat
{
    public class Output
    {
        private FLORENCE.Frame.Cli.Dat.In.Player player;

        static private int praiseEventId;
        static private Object praiseOutputBuffer_Subset;
//===
//===
        static private FLORENCE.Frame.Cli.Dat.In.Praise0_Output praise0_Output;
//===
//===
        private static float[] vertices = {
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
            0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
            0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
            0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
            0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

            0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
            0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
            0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
            0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f
        };
        private static uint[] indices = {  // note that we start from 0!
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };
        private static float[] texCoords = {
            0.0f, 0.0f,  // lower-left corner  
            1.0f, 0.0f,  // lower-right corner
            0.5f, 1.0f   // top-center corner
        };

        public Output()
        {
            player = new FLORENCE.Frame.Cli.Dat.In.Player();

            praiseEventId = new int();
            praiseEventId = 0;

            praiseOutputBuffer_Subset = null;

            praise0_Output = new FLORENCE.Frame.Cli.Dat.In.Praise0_Output();

            System.Console.WriteLine("FLORENCE: Output");
        }
        public void Initalise_Graphics()
        {
            using (var graphics = new FLORENCE.Frame.Cli.Dat.Out.Graphics(
                Framework.GetClient().GetData().GetSettings().GetGameWindowSettings(),
                Framework.GetClient().GetData().GetSettings().GetNativeWindowSettings()
            ))
            {
                FLORENCE.Frame.Cli.Dat.Settings.Set_systemInitialised(true);
                graphics.Run();
            }
        }

        public uint[] Get_Indices()
        {
            return indices;
        }
        public Object Get_OutputBufferSubset()
        {
            return praiseOutputBuffer_Subset;
        }

        public float[] Get_Vertices()
        {
            return vertices;
        }
        public FLORENCE.Frame.Cli.Dat.In.Player GetPlayer()
        {
            return player;
        }

        public int GetPraiseEventId()
        {
            return praiseEventId;
        }

        public FLORENCE.Frame.Cli.Dat.In.Praise0_Output GetPraise0_Output()
        {
            return praise0_Output;
        }

        public void Set_InputBuffer_SubSet(Object value)
        {
            praiseOutputBuffer_Subset = value;
        }

        public void SetPraiseEventId(int value)
        {
            praiseEventId = value;
        }
    }
}