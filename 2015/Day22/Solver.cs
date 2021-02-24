using System;
using System.Linq;

namespace Day22
{
    class Solver
    {
        private readonly (int hitpoints, int mana) hero;
        private readonly (int hitpoints, int damage) boss;
        private readonly bool hardMode;

        private Game bestGame = null;

        public Solver((int hitpoints, int mana) hero, (int hitpoints, int damage) boss, bool hardMode = false) {
            this.hero = hero;
            this.boss = boss;
            this.hardMode = hardMode;
        }

        public void Solve(Game game = null, bool debug = false) {
            var spells = Spells.AvailableSpells(game?.HeroMana ?? hero.mana, game?.ActiveSpells.Where(s => s.Duration > 1).Select(s => s.Spell));
            if (spells.Length == 0) spells = new Spell[] {null};
            foreach (var spell in spells) {
                var newGame = game == null ? new Game(hero, boss, hardMode) : Game.Clone(game);
                var winner = newGame.PlayRound(spell);

                if (winner == "Hero") {
                    if (newGame.TotalMana < (bestGame?.TotalMana ?? int.MaxValue)) {
                        if (debug) {
                            Console.WriteLine($"New best game mana:{newGame.TotalMana}");
                            newGame.Log();
                        }

                        bestGame = newGame;
                        continue;
                    }
                }

                if (winner == "Boss") {
                    continue;
                }

                if (newGame.TotalMana >= (bestGame?.TotalMana ?? int.MaxValue)) continue;

                Solve(newGame, debug);
            }
        }

        public Game BestGame => bestGame;
    }
}