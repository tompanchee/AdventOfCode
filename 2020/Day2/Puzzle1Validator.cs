using System.Linq;

namespace Day2
{
    class Puzzle1Validator : ValidatorBase
    {
        protected override bool Validate() {
            var count = password.Count(c => c == character);
            return count >= first && count <= second;
        }
    }
}