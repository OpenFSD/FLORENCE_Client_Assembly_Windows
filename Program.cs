using System.Runtime.InteropServices;

namespace FLORENCE
{
    public class Program
    {
        static public FLORENCE.Framework framework;
        static public FLORENCE.Frame.Client networkingClient;

        public static void Main(String[] args)
        {
            System.Console.WriteLine("FLORENCE START");

            framework = new FLORENCE.Framework();
            while (framework == null) { /* wait untill created */ }

            Valve.Sockets.Library.Initialize();
            networkingClient = new FLORENCE.Frame.Client();
            while (networkingClient == null) { /* wait untill created */ }
        }
    }
}