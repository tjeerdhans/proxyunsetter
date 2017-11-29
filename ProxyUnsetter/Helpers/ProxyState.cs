namespace ProxyUnsetter.Helpers
{
    internal enum ProxyState
    {
        Unknown,
        AutomaticallyUnset,
        AutomaticallySet,
        ManuallySet,
        IgnoredBecauseOfWhitelistedIp              
    }
}