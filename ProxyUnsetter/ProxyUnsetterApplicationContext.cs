using System;
using System.Drawing;
using System.Windows.Forms;
using ProxyUnsetter.Properties;

namespace ProxyUnsetter
{
    public class ProxyUnsetterApplicationContext : ApplicationContext
    {
        private readonly NotifyIcon _trayIcon;

        public ProxyUnsetterApplicationContext()
        {
            var trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Force unset proxy now (double click)", OnUnsetProxy);
            trayMenu.MenuItems.Add("Set to 127.0.0.1:8080", OnSetProxy);
            trayMenu.MenuItems.Add("Exit", OnExit);

            _trayIcon = new NotifyIcon
            {
                Text = @"ProxyUnsetter",
                Icon = SystemIcons.Asterisk,// proxySet ? Resources.SetProxy : Resources.UnsetProxy,
                ContextMenu = trayMenu,
                Visible = true
            };

            SetTrayIcon();

            _trayIcon.DoubleClick += OnUnsetProxy;
        }

        private void SetTrayIcon()
        {
            var proxySet = ProxyHelper.GetCurrentProxyState();
            _trayIcon.Icon = proxySet ? Resources.SetProxy : Resources.UnsetProxy;
            _trayIcon.Text = $@"Proxy is {(proxySet ? "" : "not ")}set";
        }

        private void OnSetProxy(object sender, EventArgs e)
        {
            ProxyHelper.SetProxy();
            SetTrayIcon();
        }

        private void OnUnsetProxy(object sender, EventArgs e)
        {
            ProxyHelper.UnsetProxy();
            SetTrayIcon();
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