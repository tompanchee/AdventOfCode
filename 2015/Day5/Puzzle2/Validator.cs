namespace Day5.Puzzle2
{
    class Validator : IValidator
    {
        public bool IsNice(string input) {
            return ContainsPair(input)
                   && HasSameTwoLetters(input);
        }

        private bool ContainsPair(string input) {
            for (var i = 0; i < input.Length - 2; i++) {
                var pair = input.Substring(i, 2);
                if (input.Substring(i + 2).Contains(pair)) return true;
            }

            return false;
        }

        private bool HasSameTwoLetters(string input) {
            for (var i = 0; i < input.Length - 2; i++) {
                if (input[i] == input[i + 2]) return true;
            }

            return false;
        }
    }
}