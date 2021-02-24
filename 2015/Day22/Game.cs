using System;
using System.Collections.Generic;
using System.Linq;

namespace Day22
{
    class Game
    {
        private (int hitpoints, int mana) hero;
        private (int hitpoints, int damage) boss;
        readonly List<ActiveSpell> activeSpells = new List<ActiveSpell>();
        readonly List<Spell> castSpells = new List<Spell>();
        private readonly bool hardMode;

        public Game((int hitpoints, int mana) hero, (int hitpoints, int damage) boss, bool hardMode = false) {
            this.hero = hero;
            this.boss = boss;
            this.hardMode = hardMode;
        }

        public static Game Clone(Game other) {
            var clone = new Game(other.hero, other.boss, other.hardMode);
            foreach (var spell in other.activeSpells.Where(spell => spell.Duration > 0)) {
                clone.ActiveSpells.Add(new ActiveSpell {Spell = spell.Spell, Duration = spell.Duration});
            }

            clone.castSpells.AddRange(other.castSpells);
            clone.TotalMana = other.TotalMana;
            return clone;
        }

        public string PlayRound(Spell spell) {
            int armorEffect;

            if (hardMode) {
                hero.hitpoints -= 1;
                if (hero.hitpoints <= 0) return "Boss";
            }

            Effects();
            if (boss.hitpoints <= 0) return "Hero";

            TotalMana += spell?.Cost ?? 0;
            HeroTurn();
            if (boss.hitpoints <= 0) return "Hero";

            Effects();
            if (boss.hitpoints <= 0) return "Hero";

            BossTurn();
            if (hero.hitpoints <= 0) return "Boss";

            return null;

            void HeroTurn() {
                castSpells.Add(spell ?? new Spell("None", 0));
                if (spell == null) return;
                hero.mana -= spell.Cost;
                if (spell.Duration > 0) {
                    activeSpells.Add(new ActiveSpell {Spell = spell, Duration = spell.Duration});
                    return;
                }

                hero.hitpoints += spell.Heal;
                boss.hitpoints -= spell.Damage;
            }

            void BossTurn() {
                var damage = Math.Max(boss.damage - armorEffect, 1);
                hero.hitpoints -= damage;
            }

            void Effects() {
                armorEffect = 0;
                foreach (var activeSpell in activeSpells) {
                    boss.hitpoints -= activeSpell.Spell.Damage;
                    hero.hitpoints += activeSpell.Spell.Heal;
                    hero.mana += activeSpell.Spell.Recharge;
                    armorEffect += activeSpell.Spell.Armor;
                    activeSpell.Duration -= 1;
                }

                activeSpells.RemoveAll(a => a.Duration == 0);
            }
        }

        public int TotalMana { get; private set; }

        public List<ActiveSpell> ActiveSpells => activeSpells;
        public int HeroMana => hero.mana;
        public List<Spell> CastSpells => castSpells;

        public void Log() {
            Console.WriteLine($"Spells cast");
            foreach (var castSpell in castSpells) {
                Console.WriteLine(castSpell.Name);
            }

            Console.WriteLine($"Hero hp: {hero.hitpoints}, Boss hp: {boss.hitpoints}");
            Console.WriteLine("--------");
        }
    }

    class ActiveSpell
    {
        public Spell Spell { get; set; }
        public int Duration { get; set; }
    }
}