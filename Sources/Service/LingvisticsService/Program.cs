using System;
using System.Linq;
using System.ServiceProcess;
using System.Text.RegularExpressions;

namespace Lingvistics
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Program
	{
		/// <summary>
		/// 
		/// </summary>
		private static void Main()
		{
            var rgx = new Regex( @"(?<P>[^\s\""]+)|(\""(?<P>.*?)\"")", RegexOptions.Singleline );
            var runAsConsole = rgx.Matches( Environment.CommandLine )
                                  .Cast< Match >()
                                  .Select( m => m.Groups[ "P" ].Value )
                                  .Where( arg => string.Compare( arg, "-console", true ) == 0 ||
                                                 string.Compare( arg, "console" , true ) == 0 ||
                                                 string.Compare( arg, "-c" , true ) == 0)
                                  .Any();

            if ( runAsConsole )
			{
				var lingvisticServer = new LingvisticsServer();
				lingvisticServer.Start();
                //Thread.Sleep( Timeout.Infinite );
                Console.WriteLine("[......push Enter for exit......]");
                Console.ReadLine();
                lingvisticServer.Stop();
                Console.WriteLine( "[......exit in progress......]" );
			}
			else
			{
                ServiceBase.Run( new LingvisticsServer() );
			}
		}
	}
}
