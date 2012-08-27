using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SiteMonitor.ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server(
                new StaticSiteRepository(), 
                new ConsoleAppConfiguration()
                );

            var thread = new Thread(new ThreadStart(() => server.Run()));

            thread.Start();

            Console.WriteLine("Hit enter to exit");
            Console.ReadLine();
            Console.WriteLine("Exiting");

            thread.Abort();
        }
    }
}
