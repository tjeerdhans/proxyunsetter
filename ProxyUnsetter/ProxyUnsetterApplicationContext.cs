using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ProxyUnsetter.Helpers;
using ProxyUnsetter.Properties;
using Application = System.Windows.Forms.Application;
using Timer = System.Windows.Forms.Timer;

namespace ProxyUnsetter
{
    public class ProxyUnsetterApplicationContext : ApplicationContext
    {
        private NotifyIcon _trayIcon;
        private bool _lastProxyState;

        private readonly ReleaseChecker _releaseChecker;

        public ProxyUnsetterApplicationContext()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            InitTrayMenu();

            _releaseChecker = new ReleaseChecker();
            if (Settings.Default.CheckForNewReleaseWeekly)
            {
                _releaseChecker.Start();
                _releaseChecker.CheckNowSilent();
            }
            var checkTimer = new Timer { Interval = 5000 };
            checkTimer.Tick += (sender, args) => CheckProxy();
            checkTimer.Start();

            System.Net.NetworkInformation.NetworkChange.NetworkAddressChanged +=
                (sender, args) => CheckProxy(true);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = (Exception)e.ExceptionObject;
            Program.SimpleLogLines.Add($"{DateTime.Now:g} An error occurred in the application ({exception.Message}).");
            MessageBox.Show(@"An error occurred in the application. Check the log in the about screen for details.",
                @"Proxy Unsetter", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void InitTrayMenu()
        {
            var trayMenu = new ContextMenu();

            var openSettingsFormMenuItem = new MenuItem("Settings..", OnOpenSettingsForm);
            trayMenu.MenuItems.Add("Force unset proxy now (double click)", OnUnsetProxy);
            trayMenu.MenuItems.Add("Set to 127.0.0.1:8080", OnSetProxy);
            trayMenu.MenuItems.Add(openSettingsFormMenuItem);
            trayMenu.MenuItems.Add("About..", OnShowAboutBox);
            trayMenu.MenuItems.Add("Exit", OnExit);

            _trayIcon = new NotifyIcon
            {
                Text = SettingsHelper.AppName,
                Icon = SystemIcons.Asterisk,
                ContextMenu = trayMenu,
                Visible = true
            };

            SetTrayIcon();

            _trayIcon.DoubleClick += OnUnsetProxy;
        }

        private void OnOpenSettingsForm(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm(_releaseChecker);
            settingsForm.Show();
        }

        private void CheckProxy(bool networkChanged = false)
        {
            var proxySet = SetTrayIcon();
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (proxySet && (proxySet != _lastProxyState || networkChanged))
            {
                Program.SimpleLogLines.Add($"{DateTime.Now:g} Proxy is set.{(networkChanged ? " Network changed." : "")}");
                if (Settings.Default.NotifyOfProxySet)
                {
                    _trayIcon.ShowBalloonTip(3000, @"Proxy settings have changed", @"Proxy is set", ToolTipIcon.Info);
                }
                _lastProxyState = true;
                var ipIsWhiteListed = ProxyHelper.IpIsWhiteListed();
                if (ipIsWhiteListed)
                {
                    var localIpAddress = ProxyHelper.LocalIpAddress();
                    Program.SimpleLogLines.Add($"{DateTime.Now:g} Proxy was left alone, ip is whitelisted ({localIpAddress}).");
                    return;
                }
                if (Settings.Default.UnsetProxyAutomatically)
                {
                    ProxyHelper.UnsetProxy();
                    Program.SimpleLogLines.Add($"{DateTime.Now:g} Proxy was automatically unset.");
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