using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli.Algo
{
    public class CommsSend
    {
        public CommsSend()
        {
        
        }

        static public void WaitAndCreateAndSendNewMessage()
        {
            Networking.CreateAndSendNewMessage();
        }
    }
}
