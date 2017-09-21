using System;
using System.Windows.Forms;

namespace ProxyUnsetter
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new ProxyUnsetterApplicationContext());
            //Application.Run(new Program());
        }
    }
}
