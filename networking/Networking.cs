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
        static private Valve.Sockets.NetworkingSockets sockets = null;

        public Networking()
        {
            sockets = new NetworkingSockets();
        }

        static public void CreateNetworkingClient()
        {
            NetworkingSockets client = new NetworkingSockets();

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

            //address.SetAddress("::1", port);

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

        static public void CreateAndSendNewMessage(int praiseEventId)
        {
            using (var stream = File.Open("..\\resources\\Binary_PacketData.bin", FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    if (File.Exists("..\\resources\\Binary_PacketData.bin"))
                    {
                        switch (praiseEventId)
                        {
                            case 0:
                                writer.Write((Int16)praiseEventId);
                                writer.Write((Int16)Framework.GetClient().GetData().GetTransmitInputBuffer().GetPlayer().GetMousePos().X);
                                writer.Write((Int16)Framework.GetClient().GetData().GetTransmitInputBuffer().GetPlayer().GetMousePos().Y);
                                writer.Write(true);
                                break;

                            case 1:
                                writer.Write((Int16)praiseEventId);
                                writer.Write((Int16)Framework.GetClient().GetData().GetInputBuffer(Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().GetPlayerPosition().X);
                                writer.Write((Int16)Framework.GetClient().GetData().GetInputBuffer(Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().GetPlayerPosition().Y);
                                writer.Write((Int16)Framework.GetClient().GetData().GetInputBuffer(Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().GetPlayerPosition().Z);
                                writer.Write(true);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            var data = File.Open("..\\resources\\Binary_PacketData.bin", FileMode.Open);
            sockets.SendMessageToConnection(connection, data);
        }

        static public void CopyPayloadFromMessage()
        {
            byte[] buffer = new byte[1024];
            //netMessage.CopyTo(buffer);

            Framework.GetClient().GetExecute().GetWriteEnable().Write_Start(
                Framework.GetClient().GetExecute().GetWriteEnable().GetWriteEnable_Contorl(),
                0,
                Framework.GetClient().GetGlobal().Get_NumCores(),
                Framework.GetClient().GetGlobal()
            );
            using (var stream = File.Open("..\\resources\\Binary_PacketData.bin", FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    if (File.Exists("..\\resources\\Binary_PacketData.bin"))
                    {
                        var switch_praiseEventId = reader.ReadUInt16();
                        //Console.WriteLine("Error Code : " + reader.ReadString());
                        // Console.WriteLine("Message : " + reader.ReadString());
                        // Console.WriteLine("Restart Explorer : " + reader.ReadBoolean());
                        switch (switch_praiseEventId)
                        {
                            case 0:
                                //data = new byte[64];
                                mouse_X = reader.ReadUInt16();
                                mouse_Y = reader.ReadUInt16();
                                break;

                            case 1:
                                //ToDo
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
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
