using System.Collections.Generic;
using System.Linq;

namespace Day22
{
    static class Spells
    {
        public static Spell[] AvailableSpells(int mana, IEnumerable<Spell> activeSpells = null) {
            if (activeSpells == null) return All;
            var activeNames = activeSpells.Select(a => a.Name).ToList();
            return All.Where(s => s.Cost <= mana && !activeNames.Contains(s.Name)).ToArray();
        }

        static Spell[] All => new[] {
            new Spell("Magic Missile", 53) {Damage = 4},
            new Spell("Drain", 73) {Damage = 2, Heal = 2},
            new Spell("Shield", 113) {Armor = 7, Duration = 6},
            new Spell("Poison", 173) {Damage = 3, Duration = 6},
            new Spell("Recharge", 229) {Recharge = 101, Duration = 5}
        };
    }
}