using System.Collections.Generic;

namespace Day21
{
    static class Shop
    {
        public static IList<Item[]> GetEquipmentCombinations() {
            var result = new List<Item[]>();

            // Weapon + armor + 2 rings
            foreach (var w in Weapons) {
                foreach (var a in Armors) {
                    for (var r1 = 0; r1 < Rings.Length - 1; r1++) {
                        for (var r2 = r1 + 1; r2 < Rings.Length; r2++) {
                            result.Add(new[] {w, a, Rings[r1], Rings[r2]});
                        }
                    }
                }
            }

            return result;
        }

        private static Item[] Weapons => new[] {
            new Item {Name = "Dagger", Cost = 8, Damage = 4},
            new Item {Name = "Shortsword", Cost = 10, Damage = 5},
            new Item {Name = "Warhammer", Cost = 25, Damage = 6},
            new Item {Name = "Longsword", Cost = 40, Damage = 7},
            new Item {Name = "Greataxe", Cost = 74, Damage = 8}
        };

        private static Item[] Armors => new[] {
            new Item {Name = "No Armor", Cost = 0}, // Dummy armor
            new Item {Name = "Leather", Cost = 13, Armor = 1},
            new Item {Name = "Chainmail", Cost = 31, Armor = 2},
            new Item {Name = "Splintmail", Cost = 53, Armor = 3},
            new Item {Name = "Bandedmail", Cost = 75, Armor = 4},
            new Item {Name = "Platemail", Cost = 102, Armor = 5}
        };

        private static Item[] Rings => new[] {
            new Item {Name = "No Damage Ring", Cost = 0}, // Dummy ring
            new Item {Name = "Damage +1", Cost = 25, Damage = 1},
            new Item {Name = "Damage +2", Cost = 50, Damage = 2},
            new Item {Name = "Damage +3", Cost = 100, Damage = 3},
            new Item {Name = "No Defense Ring", Cost = 0}, // Dummy ring
            new Item {Name = "Defense +1", Cost = 20, Armor = 1},
            new Item {Name = "Defense +2", Cost = 40, Armor = 2},
            new Item {Name = "Defense +3", Cost = 80, Armor = 3},
        };
    }
}