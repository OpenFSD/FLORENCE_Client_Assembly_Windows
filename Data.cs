using FLORENCE.Frame.Cli.Dat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli
{
    public class Data
    {
        static private FLORENCE.Frame.Cli.Dat.Arena arena;
        static private FLORENCE.Frame.Cli.Dat.Input input;
        static private FLORENCE.Frame.Cli.Dat.Map_Default mapDefault;
        static private FLORENCE.Frame.Cli.Dat.Output output;
        static private FLORENCE.Frame.Cli.Dat.Settings settings;

        public Data()
        {
            settings = new FLORENCE.Frame.Cli.Dat.Settings();
            while (settings == null) { /* Wait while is created */ }

            mapDefault = new FLORENCE.Frame.Cli.Dat.Map_Default();
            while (mapDefault == null) { /* Wait while is created */ }

            arena = new FLORENCE.Frame.Cli.Dat.Arena();
            while (arena == null) { /* Wait while is created */ }

            input = new FLORENCE.Frame.Cli.Dat.Input();
            while (input == null) { /* Wait while is created */ }

            output = new FLORENCE.Frame.Cli.Dat.Output();
            while (output == null) { /* Wait while is created */ }

            System.Console.WriteLine("FLORENCE: Data");
        }

        public FLORENCE.Frame.Cli.Dat.Arena GetArena()
        {
            return arena;
        }

        public FLORENCE.Frame.Cli.Dat.Input GetInput()
        {
            return input;
        }

        public FLORENCE.Frame.Cli.Dat.Map_Default GetMapDefault()
        {
            return mapDefault;
        }

        public FLORENCE.Frame.Cli.Dat.Output GetOutput()
        {
            return output;
        }

        public FLORENCE.Frame.Cli.Dat.Settings GetSettings()
        {
            return settings;
        }
    }
}
