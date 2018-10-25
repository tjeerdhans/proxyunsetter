using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using ProxyUnsetter.Helpers;

namespace ProxyUnsetter
{
    public class Program
    {
        public static List<string> SimpleLogLines = new List<string>();

        [STAThread]
        public static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Application.ThreadException += ApplicationOnThreadException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SimpleLogLines.Add($"{DateTime.Now:t} Started.");
            Application.Run(new ProxyUnsetterApplicationContext());
        }

        private static void ApplicationOnThreadException(object sender,
            ThreadExceptionEventArgs threadExceptionEventArgs)
        {
            CrashHelper.ReportCrash(threadExceptionEventArgs.Exception);
        }

        private static void CurrentDomainOnUnhandledException(object sender,
            UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            CrashHelper.ReportCrash((Exception) unhandledExceptionEventArgs.ExceptionObject);
        }
    }
}