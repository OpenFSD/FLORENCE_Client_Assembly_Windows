using FLORENCE.Frame.Cli.Algo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli
{
    public class Algorithms
    {
        static private Algo.Concurrent[] concurrent;
        static private Algo.Game gameAlgorithms;
        static private Algo.IO_ListenRespond io_ListenRespond;
        static private Algo.Concurrent new_Concurrent;

        public Algorithms(int numberOfCores) 
        {
            gameAlgorithms = new Algo.Game();
            while (gameAlgorithms == null) { /* Wait while is created */ }

            System.Console.WriteLine("FLORENCE: Algorithms");//TEST
        }

        public void Initialise(int numberOfCores)
        {
            new_Concurrent = new Algo.Concurrent();
            while (new_Concurrent == null) { /* wait untill is created */ }
            new_Concurrent.InitialiseControl();

            concurrent = new Concurrent[numberOfCores];
            for (int index = 0; index < numberOfCores - 1; index++)
            {
                concurrent[index] = new_Concurrent;
            }

            io_ListenRespond = new Algo.IO_ListenRespond();
            while (io_ListenRespond == null) { /* wait untill class constructed */ }
            io_ListenRespond.InitialiseControl();
        }

        public FLORENCE.Frame.Cli.Algo.Game GetGameAlgorithms()
        {
            return gameAlgorithms;
        }

        public FLORENCE.Frame.Cli.Algo.IO_ListenRespond GetIO_ListenRespond()
        {
            return io_ListenRespond;
        }

        public FLORENCE.Frame.Cli.Algo.Concurrent GetConcurrent(int index)
        {
            return concurrent[index];
        }
    }
}
