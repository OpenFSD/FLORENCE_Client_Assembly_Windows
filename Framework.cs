using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE
{
    public class Framework
    {
        static private FLORENCE.Frame.Client client;
        static private FLORENCE.Frame.Networking networking;

        public Framework() 
        {
            client = new FLORENCE.Frame.Client();
            while(client == null){ /* Wait while class is created */ }

            networking = new FLORENCE.Frame.Networking();
            while (networking == null) { /* Wait while class is created */ }
        }

        static public FLORENCE.Frame.Client GetClient()
        {
            return client;
        }

        static public FLORENCE.Frame.Networking Getnetworking()
        {
            return networking;
        }
    }
}
