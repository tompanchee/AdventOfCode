namespace Day2
{
    abstract class ValidatorBase : IValidator
    {
        protected int first;
        protected int second;
        protected char character;
        protected string password;

        public bool IsValid(string input) {
            Parse(input);
            return Validate();
        }

        protected abstract bool Validate();

        void Parse(string input) {
            var split = input.Split(' ');

            var range = split[0].Split('-');
            first = int.Parse(range[0]);
            second = int.Parse(range[1]);

            character = split[1][0];

            password = split[2].Trim();
        }
    }
}