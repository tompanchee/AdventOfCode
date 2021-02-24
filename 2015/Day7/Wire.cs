namespace Day7
{
    class Wire
    {
        public Wire(string id) {
            Id = id;
            Signal = null;
        }

        public string Id { get; }
        public ushort? Signal { get; set; }
    }
}