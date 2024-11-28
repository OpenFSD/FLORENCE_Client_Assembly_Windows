using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli.Algo
{
    public class CommsRecieve
    {
        public CommsRecieve() 
        { 
        
        }

        static public void WaitAndCopyPayloadFromMessage()
        {
            FLORENCE.Frame.Networking.CopyPayloadFromMessage();
        }
    }
}
