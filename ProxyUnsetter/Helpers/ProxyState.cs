namespace ProxyUnsetter.Helpers
{
    internal enum ProxyState
    {
        Unknown,
        AutomaticallyUnset,
        AutomaticallySet,
        AutomaticallySetWithAutoconfigScript,
        ManuallySet,
        IgnoredBecauseOfWhitelistedIp
    }
}