namespace Day2
{
    class Puzzle2Validator : ValidatorBase
    {
        protected override bool Validate() {
            return password[first - 1] == character && password[second - 1] != character
                   || password[first - 1] != character && password[second - 1] == character;
        }
    }
}