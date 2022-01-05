using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day7
{
    class IpV7
    {
        private IList<string> supernet;
        private IList<string> hypernet;

        public IpV7(string address) {
            Address = address;

            ResolveSubnets();
        }

        private void ResolveSubnets() {
            var outer = new List<string>();
            var inner = new List<string>();

            var current = new StringBuilder();
            var inOuter = Address[0] != '[';
            foreach (var c in Address) {
                if (inOuter) {
                    if (c != '[') {
                        current.Append(c);
                    } else {
                        outer.Add(current.ToString());
                        current = new StringBuilder();
                        inOuter = false;
                    }
                } else {
                    if (c != ']') {
                        current.Append(c);
                    } else {
                        inner.Add(current.ToString());
                        current = new StringBuilder();
                        inOuter = true;
                    }
                }
            }

            if (inOuter) outer.Add(current.ToString());
            else inner.Add(current.ToString());

            supernet = outer;
            hypernet = inner;
        }

        public string Address { get; }

        public bool SupportsTLS => DoesSupportTLS();

        public bool SupportsSSL => DoesSupportSSL();

        private bool DoesSupportTLS() {
            if (hypernet.Any(HasAbba)) return false;
            if (supernet.Any(HasAbba)) return true;

            return false;
        }

        private static bool HasAbba(string s) {
            for (var i = 0; i <= s.Length - 4; i++) {
                if (s[i] != s[i + 1] && s[i] == s[i + 3] && s[i + 1] == s[i + 2]) return true;
            }

            return false;
        }

        private bool DoesSupportSSL() {
            var abas = GetAbasFromSuperNet();
            if (abas.Count == 0) return false;

            return abas.Select(aba => new string(new[] {aba[1], aba[0], aba[1]})).Any(bab => hypernet.Any(h => h.Contains(bab)));
        }

        private IList<string> GetAbasFromSuperNet() {
            var abas = new List<string>();
            foreach (var s in supernet) {
                for (var i = 0; i <= s.Length - 3; i++) {
                    if (s[i] == s[i + 2] && s[i] != s[i + 1]) abas.Add(s[i..(i + 3)]);
                }
            }

            return abas;
        }
    }
}