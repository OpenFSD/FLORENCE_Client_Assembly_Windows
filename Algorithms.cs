using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli
{
    public class Algorithms
    {
        static private FLORENCE.Frame.Cli.Algo.Game gameAlgorithms;
        static private FLORENCE.Frame.Cli.Algo.CommsRecieve thread_CommsRecieve;
        static private FLORENCE.Frame.Cli.Algo.CommsSend thread_CommsSend;
        
        public Algorithms() 
        {
            gameAlgorithms = new FLORENCE.Frame.Cli.Algo.Game();
            while (gameAlgorithms == null) { /* Wait while class is created */ }

            thread_CommsRecieve = new FLORENCE.Frame.Cli.Algo.CommsRecieve();
            while (thread_CommsRecieve == null) { /* Wait while class is created */ }

            thread_CommsSend = new FLORENCE.Frame.Cli.Algo.CommsSend();
            while (thread_CommsSend == null) { /* Wait while class is created */ }
        }
    }
}
