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
        //static private FLORENCE.Frame.Networking networkingClient;

        public Framework() 
        {
            client = new FLORENCE.Frame.Client();
            while(client == null){ /* Wait whileis created */ }
            client.GetExecute().Initialise(client.GetGlobal().Get_NumCores());
            client.GetExecute().Initialise_Threads(client.GetGlobal().Get_NumCores());

            //Valve.Sockets.Library.Initialize();
            //networkingClient = new FLORENCE.Frame.Networking();
            //while (networkingClient == null) { /* wait untill created */ }

            System.Console.WriteLine("FLORENCE: Framework");//TEST
        }

        static public FLORENCE.Frame.Client GetClient()
        {
            return client;
        }

        //static public FLORENCE.Frame.Networking GetNetworking()
        //{
       //     return networkingClient;
       // }
    }
}
