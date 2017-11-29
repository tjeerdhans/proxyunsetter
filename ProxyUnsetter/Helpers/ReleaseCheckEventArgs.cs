using System;

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
