using FLORENCE.Frame.Cli.Dat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli.Algo
{
    public class Game
    {
//======
//======
        static private FLORENCE.Frame.Cli.Algo.UserAlg.Praise0_Algorithm praise0_Algorithm;
        static private FLORENCE.Frame.Cli.Algo.UserAlg.Praise1_Algorithm praise1_Algorithm;
//======
//======
        public Game() 
        {
            praise0_Algorithm = new FLORENCE.Frame.Cli.Algo.UserAlg.Praise0_Algorithm();
            while (praise0_Algorithm == null) { /* Wait while is created */ }

            praise1_Algorithm = new FLORENCE.Frame.Cli.Algo.UserAlg.Praise1_Algorithm();
            while (praise1_Algorithm == null) { /* Wait while is created */ }
        }
    }
}
