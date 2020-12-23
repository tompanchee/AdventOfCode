using System;
using System.Collections.Generic;
using System.Linq;

namespace Day22.Puzzle2
{
    static class Solver
    {
        public static void Solve(IEnumerable<int> deck1, IEnumerable<int> deck2) {
            Console.WriteLine("Solving puzzle 2...");

            var (_, winningDeck) = PlayGame(deck1, deck2);

            var score = 0L;
            while (winningDeck.Count > 0) {
                score += winningDeck.Count * winningDeck.Dequeue();
            }

            Console.WriteLine($"Total score after the game is {score}");
        }

        private static (int, Queue<int>) PlayGame(IEnumerable<int> deck1, IEnumerable<int> deck2) {
            var player1 = new Queue<int>(deck1);
            var player2 = new Queue<int>(deck2);

            var seenDecks = (new List<int[]>(), new List<int[]>());

            while (player1.Count > 0 && player2.Count > 0) {
                if (IsDeckSeen()) {
                    return (1, null);
                }

                seenDecks.Item1.Add(player1.ToArray());
                seenDecks.Item2.Add(player2.ToArray());

                var p1 = player1.Dequeue();
                var p2 = player2.Dequeue();

                int winner;
                if (p1 <= player1.Count && p2 <= player2.Count) {
                    (winner, _) = PlayGame(player1.Take(p1), player2.Take(p2));
                } else {
                    winner = p1 > p2 ? 1 : 2;
                }

                if (winner == 1) {
                    player1.Enqueue(p1);
                    player1.Enqueue(p2);
                } else {
                    player2.Enqueue(p2);
                    player2.Enqueue(p1);
                }
            }

            return (player1.Count > 0 ? 1 : 2, player1.Count > 0 ? player1 : player2);

            bool IsDeckSeen() {
                return seenDecks.Item1.Any(d => d.SequenceEqual(player1.ToArray()))
                       || seenDecks.Item2.Any(d => d.SequenceEqual(player2));
            }
        }
    }
}