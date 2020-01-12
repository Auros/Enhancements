using IPALogger = IPA.Logging.Logger;

namespace Enhancements
{
    internal static class Logger
    {
        public static IPALogger log { get; set; }

        internal static void Log(this object obj, IPALogger.Level level = IPALogger.Level.Info) => log.Log(level, obj.ToString());
    }
}
