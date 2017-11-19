using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyUnsetter.Helpers
{
    internal class ReleaseCheckEventArgs : EventArgs
    {
        public bool DeliberateCheck { get; }

        public ReleaseCheckEventArgs(bool deliberateCheck)
        {
            DeliberateCheck = deliberateCheck;
        }
    }
}
