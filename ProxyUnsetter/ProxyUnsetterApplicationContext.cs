using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ProxyUnsetter.Helpers;
using ProxyUnsetter.Properties;
using Timer = System.Windows.Forms.Timer;

namespace ProxyUnsetter
{
    public class ProxyUnsetterApplicationContext : ApplicationContext
    {
        private NotifyIcon _trayIcon;
        private ProxyState _lastProxyState;

        private readonly ReleaseChecker _releaseChecker;

        private MenuItem _manuallySetProxyMenuItem;

        public ProxyUnsetterApplicationContext()
        {
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

            ProxyHelper.ManuallySetProxyServer = Settings.Default.ManuallySetProxy;

            System.Net.NetworkInformation.NetworkChange.NetworkAddressChanged +=
                (sender, args) => CheckProxy(true);
        }

        private void InitTrayMenu()
        {
            var trayMenu = new ContextMenu();

            var openSettingsFormMenuItem = new MenuItem("Settings..", OnOpenSettingsForm);
            _manuallySetProxyMenuItem = new MenuItem($"Set to {ProxyHelper.ManuallySetProxyServer}", OnSetProxy);
            trayMenu.MenuItems.Add("Force unset proxy now (double click)", OnUnsetProxy);
            trayMenu.MenuItems.Add(_manuallySetProxyMenuItem);
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

            _lastProxyState = SetTrayIconAndReturnProxyState();

            _trayIcon.DoubleClick += OnUnsetProxy;

            SettingsHelper.SettingsChanged += SettingsHelperOnSettingsChanged;
        }

        private void SettingsHelperOnSettingsChanged(object o, EventArgs eventArgs)
        {
            _manuallySetProxyMenuItem.Text = $@"Set to {ProxyHelper.ManuallySetProxyServer}";
        }

        private void OnOpenSettingsForm(object sender, EventArgs e)
        {
            using (var settingsForm = new SettingsForm(_releaseChecker))
            {
                settingsForm.ShowDialog();
            }
        }

        private void CheckProxy(bool networkChanged = false)
        {
            var proxyState = SetTrayIconAndReturnProxyState();

            if (proxyState != _lastProxyState || networkChanged)
            {
                var humanizedProxyState = proxyState.Humanize();
                if (networkChanged)
                {
                    Program.SimpleLogLines.Add($"{DateTime.Now:g} Network changed.");
                }
                if (Settings.Default.NotifyOfProxySet)
                {
                    _trayIcon.ShowBalloonTip(3000, @"Proxy settings have changed", $@"Proxy is {humanizedProxyState}.", ToolTipIcon.Info);
                }
                if (proxyState == ProxyState.IgnoredBecauseOfWhitelistedIp)
                {
                    var localIpAddress = ProxyHelper.LocalIpAddress();
                    Program.SimpleLogLines.Add($"{DateTime.Now:g} Proxy was left alone, ip is whitelisted ({localIpAddress}). Proxy is {ProxyHelper.CurrentProxyServer}");
                    _lastProxyState = proxyState;
                    return;
                }
                if (Settings.Default.UnsetProxyAutomatically)
                {
                    ProxyHelper.UnsetProxy();
                    Program.SimpleLogLines.Add($"{DateTime.Now:g} Proxy was automatically unset.");
                    _trayIcon.Icon = SystemIcons.Asterisk;
                    Thread.Sleep(1000);
                    _lastProxyState = SetTrayIconAndReturnProxyState();
                }
            }
        }

        private void OnShowAboutBox(object sender, EventArgs e)
        {
            using (var aboutBox = new AboutBox())
            {
                aboutBox.ShowDialog();
            }
        }

        private ProxyState SetTrayIconAndReturnProxyState()
        {
            var proxyState = ProxyHelper.GetCurrentProxyState();
            var proxyIsSet = proxyState > ProxyState.AutomaticallyUnset;
            _trayIcon.Icon = proxyIsSet ? Resources.SetProxy : Resources.UnsetProxy;

            _trayIcon.Text =
                $@"Proxy{
                        (proxyIsSet ? $" ({ProxyHelper.CurrentProxyServer.TrimHttpAndPort().TrimAndDot(20)}) " : " ")
                    }is {proxyState.Humanize()}".TrimAndDot(64);
            return proxyState;
        }

        private void OnSetProxy(object sender, EventArgs e)
        {
            ProxyHelper.SetProxy();
            _lastProxyState = SetTrayIconAndReturnProxyState();
            Program.SimpleLogLines.Add($"{DateTime.Now:g} Proxy was manually set to {ProxyHelper.ManuallySetProxyServer}.");
        }

        private void OnUnsetProxy(object sender, EventArgs e)
        {
            ProxyHelper.UnsetProxy();
            _lastProxyState = SetTrayIconAndReturnProxyState();
            Program.SimpleLogLines.Add($"{DateTime.Now:g} Proxy was manually unset.");
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