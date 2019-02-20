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

        private readonly ReleaseChecker _releaseChecker;

        private MenuItem _manuallySetProxyMenuItem;

        private readonly ProxyManager _proxyManager;
        private readonly SettingsManager _settingsManager;

        public ProxyUnsetterApplicationContext()
        {
            _proxyManager = new ProxyManager {ManuallySetProxyServer = Settings.Default.ManuallySetProxy};
            _settingsManager = new SettingsManager(_proxyManager);

            InitTrayMenu();

            _releaseChecker = new ReleaseChecker();
            if (Settings.Default.CheckForNewReleaseWeekly)
            {
                _releaseChecker.Start();
                _releaseChecker.CheckNowSilent();
            }

            var checkTimer = new Timer {Interval = 5000};
            checkTimer.Tick += (sender, args) => CheckProxy();
            checkTimer.Start();

            _proxyManager.ManuallySetProxyServer = Settings.Default.ManuallySetProxy;

            System.Net.NetworkInformation.NetworkChange.NetworkAddressChanged +=
                (sender, args) => CheckProxy(true);
        }

        private void InitTrayMenu()
        {
            var trayMenu = new ContextMenu();

            var openSettingsFormMenuItem = new MenuItem("Settings..", OnOpenSettingsForm);
            _manuallySetProxyMenuItem = new MenuItem($"Set to {_proxyManager.ManuallySetProxyServer}", OnSetProxy);
            trayMenu.MenuItems.Add("Force unset proxy now", OnUnsetProxy);
            trayMenu.MenuItems.Add(_manuallySetProxyMenuItem);
            trayMenu.MenuItems.Add(openSettingsFormMenuItem);
            trayMenu.MenuItems.Add("About..", OnShowAboutBox);
            trayMenu.MenuItems.Add("Exit", OnExit);

            _trayIcon = new NotifyIcon
            {
                Text = SettingsManager.AppName,
                Icon = SystemIcons.Asterisk,
                ContextMenu = trayMenu,
                Visible = true
            };

            _proxyManager.LastProxyState = SetTrayIconAndReturnProxyState();
            CheckProxy(true);

            _trayIcon.DoubleClick += OnUnsetProxy;

            SettingsManager.SettingsChanged += SettingsHelperOnSettingsChanged;
        }

        private void SettingsHelperOnSettingsChanged(object o, EventArgs eventArgs)
        {
            _manuallySetProxyMenuItem.Text = $@"Set to {_proxyManager.ManuallySetProxyServer}";
        }

        private void OnOpenSettingsForm(object sender, EventArgs e)
        {
            using (var settingsForm = new SettingsForm(_releaseChecker, _proxyManager, _settingsManager))
            {
                settingsForm.ShowDialog();
            }

            _proxyManager.LastProxyState = SetTrayIconAndReturnProxyState();
        }

        private void CheckProxy(bool networkChanged = false)
        {
            var proxyState = SetTrayIconAndReturnProxyState();

            if (proxyState == ProxyState.AutomaticallySetWithAutoconfigScript &&
                Settings.Default.UnsetOrFakePac == false)
            {
                _proxyManager.FakePac();
            }

            if (proxyState == ProxyState.Unknown || proxyState != _proxyManager.LastProxyState || networkChanged)
            {
                var humanizedProxyState = proxyState.Humanize();
                if (networkChanged)
                {
                    Program.SimpleLogLines.Add($"{DateTime.Now:g} Network changed.");
                }

                if (Settings.Default.NotifyOfProxySet)
                {
                    _trayIcon.ShowBalloonTip(3000, @"Proxy settings have changed", $@"Proxy is {humanizedProxyState}.",
                        ToolTipIcon.Info);
                }

                if (proxyState == ProxyState.IgnoredBecauseOfWhitelistedIp)
                {
                    var localIpAddress = _proxyManager.LocalIpAddress();
                    Program.SimpleLogLines.Add(
                        $"{DateTime.Now:g} Proxy was left alone, ip is whitelisted ({localIpAddress}). Proxy is {_proxyManager.CurrentProxyServer}");
                    _proxyManager.LastProxyState = proxyState;
                    return;
                }

                if (Settings.Default.UnsetProxyAutomatically)
                {
                    _proxyManager.UnsetProxy();
                    _proxyManager.LastProxyState = ProxyState.AutomaticallyUnset;
                    Program.SimpleLogLines.Add($"{DateTime.Now:g} Proxy was automatically unset.");
                    _trayIcon.Icon = SystemIcons.Asterisk;
                    Thread.Sleep(1000); // wait a second, so the transition is visible in the systray.
                    SetTrayIconAndReturnProxyState();
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
            var proxyState = _proxyManager.GetCurrentProxyState();
            var proxyIsSet = proxyState > ProxyState.AutomaticallyUnset;
            _trayIcon.Icon = proxyIsSet ? Resources.SetProxy : Resources.UnsetProxy;

            _trayIcon.Text =
                $@"Proxy{
                        (proxyIsSet ? $" ({_proxyManager.CurrentProxyServer.TrimHttpAndPort().TrimAndDot(20)}) " : " ")
                    }is {proxyState.Humanize()}".TrimAndDot(64);
            return proxyState;
        }

        private void OnSetProxy(object sender, EventArgs e)
        {
            _proxyManager.SetManuallySetProxy();
            _proxyManager.LastProxyState = SetTrayIconAndReturnProxyState();
            Program.SimpleLogLines.Add(
                $"{DateTime.Now:g} Proxy was manually set to {_proxyManager.ManuallySetProxyServer}.");
        }

        private void OnUnsetProxy(object sender, EventArgs e)
        {
            _proxyManager.UnsetProxy();
            _proxyManager.LastProxyState = SetTrayIconAndReturnProxyState();
            Program.SimpleLogLines.Add($"{DateTime.Now:g} Proxy was manually unset.");
        }

        private void OnExit(object sender, EventArgs e)
        {
            if (_proxyManager.LastProxyState == ProxyState.AutomaticallySetWithAutoconfigScript)
            {
                HostsFileManager.Remove(_proxyManager.GetAutoConfigUri().Host);
            }

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