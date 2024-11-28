using System.Runtime.InteropServices;

namespace FLORENCE
{
    public class Program
    {
        //private static FLORENCE.framework framework;
        private static FLORENCE.Client networkingClient;

        public static void Main(String[] args)
        {
            System.Console.WriteLine("FLORENCE START");

            //framework = new FLORENCE.framework();
            //while (framework == null) { /* wait untill created */ }

            Valve.Sockets.Library.Initialize();
            networkingClient = new FLORENCE.Client();
        }

        //public static FLORENCE.framework Get_Framework()
        //{
        //    return framework;
        //}
    }
}