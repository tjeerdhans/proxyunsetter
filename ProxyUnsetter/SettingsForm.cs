using System;
using System.Net;
using System.Windows.Forms;
using ProxyUnsetter.Helpers;
using ProxyUnsetter.Properties;

namespace ProxyUnsetter
{
    internal partial class SettingsForm : Form
    {
        private readonly ReleaseChecker _releaseChecker;

        public SettingsForm(ReleaseChecker releaseChecker)
        {
            InitializeComponent();
            _releaseChecker = releaseChecker;

            checkBoxUnsetProxyAutomatically.Checked = Settings.Default.UnsetProxyAutomatically;
            checkBoxUnsetPac.Checked = Settings.Default.UnsetPac;
            checkBoxNotifyWhenProxySet.Checked = Settings.Default.NotifyOfProxySet;
            checkBoxCheckForNewReleaseWeekly.Checked = Settings.Default.CheckForNewReleaseWeekly;
            checkBoxLaunchAtWindowsStartup.Checked = SettingsHelper.GetLaunchAtWindowsStartupState();


            checkBoxUnsetProxyAutomatically.CheckedChanged += CheckBoxUnsetProxyAutomatically_CheckedChanged;
            checkBoxUnsetPac.CheckedChanged += CheckBoxUnsetPac_CheckedChanged;
            checkBoxNotifyWhenProxySet.CheckedChanged += CheckBoxNotifyWhenProxySet_CheckedChanged;
            checkBoxCheckForNewReleaseWeekly.CheckedChanged += CheckBoxCheckForNewReleaseWeekly_CheckedChanged;
            checkBoxLaunchAtWindowsStartup.CheckedChanged += CheckBoxLaunchAtWindowsStartup_CheckedChanged;

            buttonDelete.Enabled = false;

            InitializeIpWhitelist();
            InitializeManualProxy();
        }

        private void InitializeManualProxy()
        {
            textBoxManualProxy.Text = Settings.Default.ManuallySetProxy;
            textBoxManualProxy.LostFocus +=
                (sender, args) => SettingsHelper.SetManuallySetProxy(textBoxManualProxy.Text);
        }

        private void InitializeIpWhitelist()
        {
            labelDetectedIp.Text = ProxyHelper.LocalIpAddress().ToString();
            foreach (var ip in Settings.Default.IpWhitelist)
            {
                listBoxIpWhitelist.Items.Add(ip);
            }

            listBoxIpWhitelist.MouseDoubleClick += ListBoxWhitelist_MouseDoubleClick;
        }

        private void ListBoxWhitelist_MouseDoubleClick(object sender, MouseEventArgs mouseEventArgs)
        {
            var index = listBoxIpWhitelist.IndexFromPoint(mouseEventArgs.Location);
            if (index == ListBox.NoMatches)
                return;
            var item = (string) listBoxIpWhitelist.Items[index];
            var strings = SettingsHelper.GetIpAddressAndNetmaskFromSetting(item);
            var ipAddress = IPAddress.Parse(strings[0]);
            var netmask = IPAddress.Parse(strings[1]);

            using (var ipAddressEntryForm = new IpAddressEntryForm(ipAddress, netmask))
            {
                var dialogResult = ipAddressEntryForm.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    item = $"{ipAddressEntryForm.IpAddressString}/{ipAddressEntryForm.NetmaskString}";
                    listBoxIpWhitelist.Items[index] = item;
                    Settings.Default.IpWhitelist[index] = item;
                    Settings.Default.Save();
                }
            }
        }

        private static void CheckBoxUnsetProxyAutomatically_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox) sender;
            SettingsHelper.ToggleAutomaticProxyUnset(checkbox.Checked);
        }

        private static void CheckBoxUnsetPac_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox) sender;
            SettingsHelper.ToggleUnsetPac(checkbox.Checked);
        }
        
        private static void CheckBoxNotifyWhenProxySet_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox) sender;
            SettingsHelper.ToggleNotifyOfProxySet(checkbox.Checked);
        }

        private static void CheckBoxLaunchAtWindowsStartup_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox) sender;
            SettingsHelper.ToggleWindowsStartup(checkbox.Checked);
        }

        private void CheckBoxCheckForNewReleaseWeekly_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox) sender;
            SettingsHelper.ToggleCheckForNewReleaseWeekly(checkbox.Checked, _releaseChecker);
        }

        private void ButtonCheckForNewRelease_Click(object sender, EventArgs e)
        {
            _releaseChecker.CheckNow();
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            using (var ipAddressEntryForm =
                new IpAddressEntryForm(IPAddress.Parse("0.0.0.0"), IPAddress.Parse("0.0.0.0")))
            {
                var dialogResult = ipAddressEntryForm.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    var item = $"{ipAddressEntryForm.IpAddressString}/{ipAddressEntryForm.NetmaskString}";
                    listBoxIpWhitelist.Items.Add(item);
                    Settings.Default.IpWhitelist.Add(item);
                    Settings.Default.Save();
                }
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            var index = listBoxIpWhitelist.SelectedIndex;
            listBoxIpWhitelist.Items.RemoveAt(index);
            Settings.Default.IpWhitelist.RemoveAt(index);
            Settings.Default.Save();
        }

        private void ListBoxIpWhitelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxIpWhitelist.SelectedIndex >= 0)
            {
                buttonDelete.Enabled = true;
            }
            else
            {
                buttonDelete.Enabled = false;
            }
        }

        private void ButtonSetManualProxy_Click(object sender, EventArgs e)
        {
            ProxyHelper.SetProxy();
            ProxyHelper.LastProxyState = ProxyHelper.GetCurrentProxyState();
            Program.SimpleLogLines.Add(
                $"{DateTime.Now:g} Proxy was manually set to {ProxyHelper.ManuallySetProxyServer}.");
        }
    }
}