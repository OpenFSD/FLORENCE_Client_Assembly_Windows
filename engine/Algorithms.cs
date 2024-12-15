using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli
{
    public class Algorithms
    {
        static private FLORENCE.Frame.Cli.Algo.Concurrent[] concurrent;
        static private FLORENCE.Frame.Cli.Algo.IO_ListenRespond io_ListenRespond;
        static private FLORENCE.Frame.Cli.Algo.Concurrent new_Concurrent;
        static private FLORENCE.Frame.Cli.Algo.User_Alg user_I;

        public Algorithms(int numberOfCores) 
        {
            concurrent = new FLORENCE.Frame.Cli.Algo.Concurrent[numberOfCores];
            for (Int16 index = 0; index < numberOfCores; index++) 
            {
                concurrent[index] = new FLORENCE.Frame.Cli.Algo.Concurrent();
                while (concurrent[index] == null) { /* Wait while is created */ }
            }
            user_I = new FLORENCE.Frame.Cli.Algo.User_Alg();
            while (user_I == null) { /* Wait while is created */ }

            System.Console.WriteLine("FLORENCE: Algorithms");//TEST
        }

        public void Initialise(int numberOfCores)
        {
            new_Concurrent = new FLORENCE.Frame.Cli.Algo.Concurrent();
            while (new_Concurrent == null) { /* wait untill is created */ }
            new_Concurrent.InitialiseControl();

            concurrent = new FLORENCE.Frame.Cli.Algo.Concurrent[numberOfCores];
            for (int index = 0; index < numberOfCores - 1; index++)
            {
                concurrent[index] = GetNewEmptyConcurrent();
            }

            io_ListenRespond = new FLORENCE.Frame.Cli.Algo.IO_ListenRespond();
            while (io_ListenRespond == null) { /* wait untill class constructed */ }
            io_ListenRespond.InitialiseControl();
        }

       // public FLORENCE.Frame.Cli.Algo.User_Alg GetGameAlgorithms()
       // {
       //     return gameAlgorithms;
       // }

        public FLORENCE.Frame.Cli.Algo.IO_ListenRespond GetIO_ListenRespond()
        {
            return io_ListenRespond;
        }

        public FLORENCE.Frame.Cli.Algo.Concurrent GetConcurrent(int index)
        {
            return concurrent[index-2];
        }
        public FLORENCE.Frame.Cli.Algo.Concurrent GetNewEmptyConcurrent()
        {
            return new_Concurrent;
        }
    }
}
