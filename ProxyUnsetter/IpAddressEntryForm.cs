using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProxyUnsetter
{
    public partial class IpAddressEntryForm : Form
    {
        public string IpAddressString { get; private set; }
        public string NetmaskString { get; private set; }

        public IpAddressEntryForm(IPAddress ipAddress, IPAddress netmask)
        {
            InitializeComponent();

            ipAddressControlIp.IPAddress = ipAddress;
            ipAddressControlNetmask.IPAddress = netmask;

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            IpAddressString = ipAddressControlIp.ToString();
            NetmaskString = ipAddressControlNetmask.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
