using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProxyUnsetter
{
    public class Program
    {
        public static List<string> SimpleLogLines = new List<string>();

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SimpleLogLines.Add($"{DateTime.Now:t} Started.");
            Application.Run(new ProxyUnsetterApplicationContext());
        }
    }
}
