using FLORENCE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Valve.Sockets;

namespace FLORENCE.Frame
{
    public class Client
    {
        static private FLORENCE.Frame.Cli.Algorithms algorithms;

        public Client() 
        {
            algorithms = new FLORENCE.Frame.Cli.Algorithms();
            while (algorithms == null) { /* Wait while class is created */ }
        }

        public FLORENCE.Frame.Cli.Algorithms GetAlgorithms()
        {
            return algorithms;
        }
    }
}