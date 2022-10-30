using System.Collections.Generic;
using System.Linq;

namespace Day4
{
    class Room
    {
        public static Room FromInput(string input) {
            var idx = input.LastIndexOf('-');
            var id = input[..idx];
            var sector = int.Parse(input[(idx + 1)..]);
            return new Room(id, sector);
        }

        private Room(string id, int sector) {
            Id = id;
            Sector = sector;
        }

        public string Id { get; }
        public int Sector { get; }

        public string CalculateCheckSum() {
            var temp = Id.Replace("-", "");
            var counts = temp
                .GroupBy(c => c)
                .ToDictionary(g => g.Key, g => g.Count());
            var ordered = counts.OrderBy(p => p, new Comparer());

            return new string(ordered.Take(5).Select(p => p.Key).ToArray());
        }

        public string Decipher() {
            const string abc = "abcdefghijklmnopqrstuvwxyz";

            var result = new List<char>();
            foreach (var c in Id) {
                if (c == '-') {
                    result.Add(' ');
                    continue;
                }

                var idx = (abc.IndexOf(c) + Sector) % abc.Length;
                result.Add(abc[idx]);
            }

            return new string(result.ToArray());
        }

        class Comparer : IComparer<KeyValuePair<char, int>>
        {
            public int Compare(KeyValuePair<char, int> x, KeyValuePair<char, int> y) {
                if (x.Value == y.Value) {
                    return x.Key.CompareTo(y.Key);
                }

                return x.Value < y.Value ? 1 : -1;
            }
        }
    }
}