using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4
{
    class Passport
    {
        private const string VALID_HAIR_COLOR_CHARS = "01234567890abcdef";
        static readonly string[] validEyeColors = {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

        readonly IDictionary<string, string> fields = new Dictionary<string, string>();

        Passport() { }

        public static Passport Init(string data) {
            var pp = new Passport();
            var split = data.Split(' ');
            foreach (var fieldData in split) {
                var splitField = fieldData.Split(':');
                pp.fields.Add(splitField[0], splitField[1]);
            }

            return pp;
        }

        public bool IsValid1 => fields.Count == 8 || fields.Count == 7 && !fields.ContainsKey("cid");

        public bool IsValid2 => IsByrValid && IsIyrValid && IsEyrValid && IsHgtValid && IsHclValid && IsEclValid && IsPidValid;

        bool IsByrValid => IsNumericFieldValid("byr", 1920, 2002);

        bool IsIyrValid => IsNumericFieldValid("iyr", 2010, 2020);

        bool IsEyrValid => IsNumericFieldValid("eyr", 2020, 2030);

        bool IsHgtValid
        {
            get
            {
                if (!fields.ContainsKey("hgt")) return false;

                var unit = fields["hgt"].Substring(Math.Max(0, fields["hgt"].Length - 2));
                if (unit != "cm" && unit != "in") return false;

                var value = fields["hgt"].Substring(0, fields["hgt"].Length - 2);
                if (!int.TryParse(value, out var height)) return false;

                return unit switch {
                    "cm" => height >= 150 && height <= 193,
                    "in" => height >= 59 && height <= 76,
                    _ => false
                };
            }
        }

        bool IsHclValid
        {
            get
            {
                if (!fields.ContainsKey("hcl")) return false;
                if (fields["hcl"].Length != 7 || fields["hcl"][0] != '#') return false;

                var value = fields["hcl"].Substring(1);
                return value.All(c => VALID_HAIR_COLOR_CHARS.Contains(c));
            }
        }

        bool IsEclValid => fields.ContainsKey("ecl") && validEyeColors.Contains(fields["ecl"]);

        bool IsPidValid => fields.ContainsKey("pid") && fields["pid"].Length == 9 && fields["pid"].All(char.IsDigit);

        bool IsNumericFieldValid(string field, int min, int max) {
            if (!fields.ContainsKey(field)) return false;
            if (!int.TryParse(fields[field], out var value)) return false;
            return value >= min && value <= max;
        }
    }
}