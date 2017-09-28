using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using ProxyUnsetter.Properties;
using Timer = System.Windows.Forms.Timer;

namespace ProxyUnsetter
{
    public class ProxyUnsetterApplicationContext : ApplicationContext
    {
        private readonly NotifyIcon _trayIcon;

        private bool _lastProxyState;

        private bool _unsetProxyAutomatically = true;

        private bool _notifyOfProxySet;

        public ProxyUnsetterApplicationContext()
        {
            var trayMenu = new ContextMenu();
            var automaticUnsetProxyMenuItem =
                new MenuItem("Unset proxy automatically", OnToggleAutomaticProxyUnset) { Checked = true };
            var notifyOfProxySetMenuItem =
                new MenuItem("Notify when proxy set", OnToggleNotifyOfProxySet) { Checked = false };
            trayMenu.MenuItems.Add(automaticUnsetProxyMenuItem);
            trayMenu.MenuItems.Add(notifyOfProxySetMenuItem);
            trayMenu.MenuItems.Add("Force unset proxy now (double click)", OnUnsetProxy);
            trayMenu.MenuItems.Add("Launch at Windows startup", OnToggleWindowsStartup);
            trayMenu.MenuItems.Add("Set to 127.0.0.1:8080", OnSetProxy);
            trayMenu.MenuItems.Add("About..", OnShowAboutBox);
            trayMenu.MenuItems.Add("Exit", OnExit);

            _trayIcon = new NotifyIcon
            {
                Text = @"ProxyUnsetter",
                Icon = SystemIcons.Asterisk,
                ContextMenu = trayMenu,
                Visible = true
            };

            SetTrayIcon();

            _trayIcon.DoubleClick += OnUnsetProxy;

            var checkTimer = new Timer { Interval = 5000 };
            checkTimer.Tick += _checkTimer_Tick;
            checkTimer.Start();
        }

        private void OnToggleWindowsStartup(object sender, EventArgs e)
        {
            var menuItem = (MenuItem) sender;
            menuItem.Checked = !menuItem.Checked;

            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (menuItem.Checked)
                rk.SetValue("ProxyUnsetter", Application.ExecutablePath);
            else
                rk.DeleteValue("ProxyUnsetter", false);
        }

        private void OnToggleNotifyOfProxySet(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            menuItem.Checked = !menuItem.Checked;
            _notifyOfProxySet = menuItem.Checked;
        }

        private void OnToggleAutomaticProxyUnset(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            menuItem.Checked = !menuItem.Checked;
            _unsetProxyAutomatically = menuItem.Checked;
        }

        private void _checkTimer_Tick(object sender, EventArgs e)
        {
            var proxySet = SetTrayIcon();
            if (proxySet && proxySet != _lastProxyState)
            {
                Program.SimpleLogLines.Add($"{DateTime.Now:t} Proxy was set.");
                if (_notifyOfProxySet)
                {
                    _trayIcon.ShowBalloonTip(3000, @"Proxy settings have changed", @"Proxy was set", ToolTipIcon.Info);
                }
                _lastProxyState = true;
                if (_unsetProxyAutomatically)
                {
                    ProxyHelper.UnsetProxy();
                    Program.SimpleLogLines.Add($"{DateTime.Now:t} Proxy was automatically unset.");
                    _trayIcon.Icon = SystemIcons.Asterisk;
                    Thread.Sleep(1000);
                    _lastProxyState = SetTrayIcon();
                }
            }
        }

        private void OnShowAboutBox(object sender, EventArgs e)
        {
            var aboutBox = new AboutBox();
            aboutBox.Show();
        }

        private bool SetTrayIcon()
        {
            var proxySet = ProxyHelper.GetCurrentProxyState();
            _trayIcon.Icon = proxySet ? Resources.SetProxy : Resources.UnsetProxy;
            _trayIcon.Text = $@"Proxy is {(proxySet ? "" : "not ")}set";
            return proxySet;
        }

        private void OnSetProxy(object sender, EventArgs e)
        {
            ProxyHelper.SetProxy();
            _lastProxyState = SetTrayIcon();

        }

        private void OnUnsetProxy(object sender, EventArgs e)
        {
            ProxyHelper.UnsetProxy();
            _lastProxyState = SetTrayIcon();
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
                // Hide tray icon, otherwise it will remain shown until user mouses over it
                _trayIcon.Visible = false;
                _trayIcon.Dispose();
            }

            base.Dispose(isDisposing);
        }
    }
}