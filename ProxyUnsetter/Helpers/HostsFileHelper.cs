using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ProxyUnsetter.Helpers
{
    internal class HostsFileHelper
    {
        public static void TransformFile(string hostsFile,
            params Func<IEnumerable<string>, IEnumerable<string>>[] transforms)
        {
            var encoding = GetEncoding(hostsFile);
            IEnumerable<string> contents = File.ReadAllLines(hostsFile);

            foreach (var transform in transforms)
            {
                contents = transform(contents);
            }

            File.WriteAllLines(hostsFile, contents.ToArray(), encoding);
        }

        public static void RemoveFromFile(string target, string hostsPath)
        {
            TransformFile(hostsPath, GetRemoveTransformForHost(target));
        }

        public static void RemoveFromFile(Regex pattern, string hostsPath)
        {
            TransformFile(hostsPath, GetRemoveTransform(host => pattern.Match(host).Success));
        }

        public static Func<IEnumerable<string>, IEnumerable<string>> GetRemoveTransformForHost(string hostname)
        {
            return GetRemoveTransform(host => host.Equals(hostname, StringComparison.InvariantCultureIgnoreCase));
        }

        private static Func<IEnumerable<string>, IEnumerable<string>> GetRemoveTransform(
            Func<string, bool> doHostsMatch)
        {
            return lines => lines.Where(l =>
            {
                var match = TryGetHostsFileEntry(l);

                if (match == null)
                    return true;

                var matchedHost = match.Hostname;
                if (!doHostsMatch(matchedHost))
                    return true;
                else
                    return false;
            });
        }

        public static bool IsLineAHostFilesEntry(string line)
        {
            if (line.Trim().Length == 0)
                return false;
            else if (line.TrimStart().StartsWith("#"))
                return false;
            else
                return true;
        }

        public static HostsFileEntry GetHostsFileEntry(string line)
        {
            var result = TryGetHostsFileEntry(line);

            if (result == null)
                throw new InvalidDataException();

            return result;
        }

        public static HostsFileEntry TryGetHostsFileEntry(string line)
        {
            var match = RegexHostsEntry.Match(line);

            if (!match.Success)
                return null;

            return new HostsFileEntry(match.Groups["name"].Value, match.Groups["address"].Value);
        }

        public static Encoding GetEncoding(string file)
        {
            using (var reader = new StreamReader(file))
            {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                reader.Peek();
                return reader.CurrentEncoding;
            }
        }

        static readonly Regex RegexHostsEntry =
            new Regex(@"^\s*(?<address>\S+)\s+(?<name>\S+)\s*($|#)", RegexOptions.Compiled);
    }
}
