using System.Configuration;
using System.Diagnostics;

namespace TonalityMarkingAndDigest.web.demo
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Config
    {
#if WITH_OM_TM
        public static readonly string LINGUISTICS_SERVER_URL                  = ConfigurationManager.AppSettings[ "LINGUISTICS_SERVER_URL" ];
        public static readonly int    LINGUISTICS_SERVER_TIMEOUT_IN_SECONDS   = ConfigurationManager.AppSettings[ "LINGUISTICS_SERVER_TIMEOUT_IN_SECONDS" ].ToInt32();
        public static readonly string LINGUISTICS_SERVER_EXE_FILE_LOCATION    = ConfigurationManager.AppSettings[ "LINGUISTICS_SERVER_EXE_FILE_LOCATION" ];
        public static readonly bool   LINGUISTICS_SERVER_EXE_CREATE_NO_WINDOW = ConfigurationManager.AppSettings[ "LINGUISTICS_SERVER_EXE_CREATE_NO_WINDOW" ].TryToBool( false );

        public static ProcessStartInfo CreateLinguisticsServerProcessStartInfo()
        {
            var startInfo = new ProcessStartInfo( LINGUISTICS_SERVER_EXE_FILE_LOCATION, "-console" );
            if ( LINGUISTICS_SERVER_EXE_CREATE_NO_WINDOW )
            {
                startInfo.WindowStyle     = ProcessWindowStyle.Hidden;
                startInfo.CreateNoWindow  = false;
                startInfo.UseShellExecute = false;
            }
            return (startInfo);
        }
#else
        public static readonly string TONALITY_MARKING_ENDPOINT_CONFIGURATION_NAME = ConfigurationManager.AppSettings[ "TONALITY_MARKING_ENDPOINT_CONFIGURATION_NAME" ];
        public static readonly string DIGEST_ENDPOINT_CONFIGURATION_NAME           = ConfigurationManager.AppSettings[ "DIGEST_ENDPOINT_CONFIGURATION_NAME" ];
#endif
        public static readonly int MAX_INPUTTEXT_LENGTH                = ConfigurationManager.AppSettings[ "MAX_INPUTTEXT_LENGTH"                ].ToInt32();
        public static readonly int SAME_IP_INTERVAL_REQUEST_IN_SECONDS = ConfigurationManager.AppSettings[ "SAME_IP_INTERVAL_REQUEST_IN_SECONDS" ].ToInt32();
        public static readonly int SAME_IP_MAX_REQUEST_IN_INTERVAL     = ConfigurationManager.AppSettings[ "SAME_IP_MAX_REQUEST_IN_INTERVAL"     ].ToInt32();        
        public static readonly int SAME_IP_BANNED_INTERVAL_IN_SECONDS  = ConfigurationManager.AppSettings[ "SAME_IP_BANNED_INTERVAL_IN_SECONDS"  ].ToInt32();
    }
}