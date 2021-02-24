using System;
using System.Collections.Generic;

namespace Day16
{
    class Sue
    {
        private Sue(int id) {
            Id = id;
        }

        public static Sue FromInputLine(string input) {
            var id = int.Parse(input[..input.IndexOf(':')][3..]);
            var sue = new Sue(id);

            var props = input[(input.IndexOf(':') + 1)..];
            var split = props.Split(',', StringSplitOptions.TrimEntries);
            foreach (var prop in split) {
                var splitProp = prop.Split(':', StringSplitOptions.TrimEntries);
                sue.Properties.Add(splitProp[0], int.Parse(splitProp[1]));
            }

            return sue;
        }

        public int Id { get; }
        public IDictionary<string, int> Properties { get; } = new Dictionary<string, int>();
    }
}