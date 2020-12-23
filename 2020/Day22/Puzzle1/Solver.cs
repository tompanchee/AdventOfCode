using System;
using System.Collections.Generic;

namespace Day22.Puzzle1
{
    static class Solver
    {
        public static void Solve(IEnumerable<int> deck1, IEnumerable<int> deck2) {
            Console.WriteLine("Solving puzzle 1...");
            var player1 = new Queue<int>(deck1);
            var player2 = new Queue<int>(deck2);

            while (player1.Count > 0 && player2.Count > 0) {
                PlayRound();
            }

            var winningDeck = player1.Count > 0 ? player1 : player2;

            var score = 0L;
            while (winningDeck.Count > 0) {
                score += winningDeck.Count * winningDeck.Dequeue();
            }

            Console.WriteLine($"Total score after the game is {score}");

            void PlayRound() {
                var p1 = player1.Dequeue();
                var p2 = player2.Dequeue();

                if (p1 > p2) {
                    player1.Enqueue(p1);
                    player1.Enqueue(p2);
                } else {
                    player2.Enqueue(p2);
                    player2.Enqueue(p1);
                }
            }
        }
    }
}