using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using ProxyUnsetter.Properties;

namespace ProxyUnsetter
{
    public class Program : Form
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new ProxyUnsetterApplicationContext());
            //Application.Run(new Program());
        }

        private readonly NotifyIcon _trayIcon;

        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        // ReSharper disable InconsistentNaming
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;
        // ReSharper restore InconsistentNaming

        //bool settingsReturn, refreshReturn;

        private Program()
        {
            // Create a simple tray menu with only one item.
            var trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Force unset proxy now", OnUnsetProxy);
            trayMenu.MenuItems.Add("Exit", OnExit);

            var proxySet = GetCurrentProxyState();
            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            _trayIcon = new NotifyIcon
            {
                Text = $@"Proxy is {(proxySet ? "" : "not ")}set",
                Icon = proxySet ? Resources.SetProxy : Resources.UnsetProxy,
                ContextMenu = trayMenu,
                Visible = true
            };

            // Add menu to tray icon and show it.
        }

        private bool GetCurrentProxyState()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            if (registry == null)
            {
                MessageBox.Show(@"Couldn't find the registry key to update proxy settings.", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return true;
            }
            var proxyEnableValue = registry.GetValue("ProxyEnable");
            if (proxyEnableValue != null && (int)proxyEnableValue == 0)
            {
                return false;
            }
            return true;
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.

            base.OnLoad(e);
        }

        private void OnUnsetProxy(object sender, EventArgs e)
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            if (registry == null)
            {
                MessageBox.Show(@"Couldn't find the registry key to update proxy settings.", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            registry.SetValue("ProxyEnable", 0);
            //registry.SetValue("ProxyServer", "127.0.0.1:8080");
            // These lines implement the Interface in the beginning of program 
            // They cause the OS to refresh the settings, causing IP to realy update
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                // Release the icon resource.
                _trayIcon.Dispose();
            }

            base.Dispose(isDisposing);
        }
    }
}
