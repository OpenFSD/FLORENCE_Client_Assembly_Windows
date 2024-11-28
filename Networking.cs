using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Valve.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FLORENCE.Frame
{
    public class Networking
    {
        static private NetworkingSockets client = null;
        static private byte[] data = null;
        static private byte[][] stackOutboundSockets = null;

        public Networking() 
        {
            client = new NetworkingSockets();
            data = new byte[64];
            stackOutboundSockets = new byte[1][];
            stackOutboundSockets[0] = new byte[64];
        }

        static public void CreateNetworkingClient()
        {
            //NetworkingSockets client = new NetworkingSockets();

            uint connection = 0;

            StatusCallback status = (ref StatusInfo info) => {
                switch (info.connectionInfo.state)
                {
                    case ConnectionState.None:
                        break;

                    case ConnectionState.Connected:
                        Console.WriteLine("Client connected to server - ID: " + connection);
                        CopyPayloadFromMessage();
                        break;

                    case ConnectionState.ClosedByPeer:
                    case ConnectionState.ProblemDetectedLocally:
                        client.CloseConnection(connection);
                        Console.WriteLine("Client disconnected from server");
                        break;
                }
            };

            NetworkingUtils utils = new NetworkingUtils();
            utils.SetStatusCallback(status);

            Address address = new Address();

            address.SetAddress("::1", port);

            connection = client.Connect(ref address);

#if VALVESOCKETS_SPAN
	MessageCallback message = (in NetworkingMessage netMessage) => {
		Console.WriteLine("Message received from server - Channel ID: " + netMessage.channel + ", Data length: " + netMessage.length);
	};
#else
            const int maxMessages = 20;

            NetworkingMessage[] netMessages = new NetworkingMessage[maxMessages];
#endif

            while (!Console.KeyAvailable)
            {
                client.RunCallbacks();

#if VALVESOCKETS_SPAN
		client.ReceiveMessagesOnConnection(connection, message, 20);
#else
                int netMessagesCount = client.ReceiveMessagesOnConnection(connection, netMessages, maxMessages);

                if (netMessagesCount > 0)
                {
                    for (int i = 0; i < netMessagesCount; i++)
                    {
                        ref NetworkingMessage netMessage = ref netMessages[i];

                        Console.WriteLine("Message received from server - Channel ID: " + netMessage.channel + ", Data length: " + netMessage.length);
                        CopyPayloadFromMessage();//added

                        netMessage.Destroy();
                    }
                }
#endif

                Thread.Sleep(15);
            }
        }

        static public void CreateAndSendNewMessage()
        {
            
            //byte[] temp = new byte[64];

           // stackOutboundSockets[stackOutboundSockets.GetLength(0) + 1][];
           // stackOutboundSockets[stackOutboundSockets.GetLength(0) + 1] = new byte[64];
           // stackOutboundSockets[stackOutboundSockets.GetLength(0) + 1] = temp;

           // data = stackOutboundSockets[0];
            
            sockets.SendMessageToConnection(connection, data);
        }

        static public void CopyPayloadFromMessage()
        {
            byte[] buffer = new byte[1024];
            netMessage.CopyTo(buffer);
        }

        static public void SetA_HookForDebugInformation()
        {
            DebugCallback debug = (type, message) => {
                Console.WriteLine("Debug - Type: " + type + ", Message: " + message);
            };

            NetworkingUtils utils = new NetworkingUtils();

            utils.SetDebugCallback(DebugType.Everything, debug);
        }
    }
}
