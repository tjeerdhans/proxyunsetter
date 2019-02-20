namespace ProxyUnsetter.Helpers
{
    internal class HostsFileEntry
    {
        public string Hostname;
        public string Address;

        public HostsFileEntry(string hostname, string address)
        {
            Hostname = hostname;
            Address = address;
        }

        public override string ToString()
        {
            return $"{GetType().Name}({Hostname}={Address})";
        }
    }
}
