using System;
using System.Linq;
using System.Text;

namespace Day11
{
    class Program
    {
        const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";

        static void Main(string[] args) {
            const string INPUT = "vzbxkghb";

            Console.WriteLine("Solving puzzle 1...");
            var password = INPUT;
            while (!IsValid(password)) {
                password = GetNextPassword(password);
            }

            Console.WriteLine($"Santa's next password is {password}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            password = GetNextPassword(password);
            while (!IsValid(password)) {
                password = GetNextPassword(password);
            }

            Console.WriteLine($"Santa's next password is {password}");
        }

        static string GetNextPassword(string password) {
            var current = password.Length;
            var next = password;
            var found = false;

            while (!found) {
                current--;
                var idx = ALPHABET.IndexOf(next[current]);
                char nextChar;
                if (idx < ALPHABET.Length - 1) {
                    nextChar = ALPHABET[idx + 1];
                    found = true;
                } else {
                    nextChar = ALPHABET[0];
                }

                next = new StringBuilder(next) {[current] = nextChar}.ToString();
            }

            return next;
        }

        static bool IsValid(string password) {
            return DoesNotContainForbiddenCharacters()
                   && HasIncreasingStraight()
                   && HasAtLeastTwoNonOverLappingPairs();

            bool DoesNotContainForbiddenCharacters() {
                var forbiddenChars = new[] {'i', 'o', 'l'};
                return forbiddenChars.All(forbiddenChar => !password.Contains(forbiddenChar));
            }

            bool HasIncreasingStraight() {
                for (var i = 0; i < ALPHABET.Length - 2; i++) {
                    if (password.Contains(ALPHABET[i..(i + 3)])) return true;
                }

                return false;
            }

            bool HasAtLeastTwoNonOverLappingPairs() {
                var pairFound = "";
                var current = 0;
                while (current < password.Length - 1) {
                    if (password[current] == password[current + 1]) {
                        var pair = password[current..(current + 1)];
                        if (pairFound != "" && pair != pairFound) return true;
                        pairFound = pair;
                        current += 2;
                    }

                    current++;
                }

                return false;
            }
        }
    }
}