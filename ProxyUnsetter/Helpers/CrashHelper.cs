using System;
using System.Windows.Forms;

//using CrashReporterDotNET;

namespace ProxyUnsetter.Helpers
{
    public static class CrashHelper
    {
        public static void ReportCrash(Exception exception)
        {
            //var reportCrash = new ReportCrash("johndoe@johndoe.com")
            //{
            //    DoctorDumpSettings =
            //        new DoctorDumpSettings { ApplicationID = Guid.Parse("fc3922c0-c4f0-493b-85e8-8be42f370619") },
            //    AnalyzeWithDoctorDump = true
            //};
            //reportCrash.Send(exception);

            //Program.SimpleLogLines.Add($"{DateTime.Now:g} An error occurred in the application ({exception.Message}).");
            MessageBox.Show($@"An error occurred in the application:
{exception.Message}

{exception.StackTrace}",
                @"Proxy Unsetter", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(0);
        }
    }
}
