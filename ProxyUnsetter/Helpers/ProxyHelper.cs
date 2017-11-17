using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using NetTools;
using ProxyUnsetter.Properties;

namespace ProxyUnsetter.Helpers
{
    internal static class ProxyHelper
    {
        [DllImport("wininet.dll")]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        // ReSharper disable InconsistentNaming
        private const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        private const int INTERNET_OPTION_REFRESH = 37;
        // ReSharper restore InconsistentNaming

        public static bool GetCurrentProxyState()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            if (registry == null)
            {
                MessageBox.Show(@"Couldn't find the registry key to update proxy settings.", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return true;
            }
            var proxyEnableValue = registry.GetValue("ProxyEnable");
            return proxyEnableValue == null || (int)proxyEnableValue != 0;
        }

        private static void RefreshProxySettings()
        {
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }

        public static void UnsetProxy()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            if (registry == null)
            {
                MessageBox.Show(@"Couldn't find the registry key to update proxy settings.", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            registry.SetValue("ProxyEnable", 0);
            RefreshProxySettings();
        }

        public static void SetProxy()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            if (registry == null)
            {
                MessageBox.Show(@"Couldn't find the registry key to update proxy settings.", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            registry.SetValue("ProxyEnable", 1);
            registry.SetValue("ProxyServer", "127.0.0.1:8080");
            RefreshProxySettings();
        }

        public static bool IpIsWhiteListed()
        {
            var localIpAddress = LocalIpAddress();
            if (Equals(localIpAddress, IPAddress.None))
            {
                return true;
            }
            foreach (var ipRange in Settings.Default.IpWhitelist)
            {
                var range = IPAddressRange.Parse(ipRange);
                if (range.Contains(localIpAddress))
                {
                    return true;
                }
            }
            return false;
        }

        public static IPAddress LocalIpAddress()
        {
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.OperationalStatus == OperationalStatus.Up)
                {
                    IPInterfaceProperties adapterProperties = item.GetIPProperties();

                    if (adapterProperties.GatewayAddresses.Any())
                    {
                        foreach (UnicastIPAddressInformation ip in adapterProperties.UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                return ip.Address;
                            }
                        }
                    }
                }
            }
            return IPAddress.None;
        }
    }
}
