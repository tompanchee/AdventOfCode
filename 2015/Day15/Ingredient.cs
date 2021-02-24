namespace Day15
{
    class Ingredient
    {
        public Ingredient(int capacity, int durability, int flavor, int texture, int calories) {
            Capacity = capacity;
            Durability = durability;
            Flavor = flavor;
            Texture = texture;
            Calories = calories;
        }

        public int Capacity { get; }

        public int Durability { get; }

        public int Flavor { get; }

        public int Texture { get; }

        public int Calories { get; }
    }
}