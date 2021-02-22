using System;

namespace Day25
{
    class Program
    {
        static void Main(string[] args) {
            var cardKey = 14205034L;
            var doorKey = 18047856L;

            Console.WriteLine("Solving puzzle 1...");
            var cardLoop = CalculateLoopCount(7L, cardKey);
            var key = CalculateEncryptionKey(doorKey, cardLoop);
            Console.WriteLine($"Encryption key is {key}");
        }

        static long CalculateLoopCount(long subject, long key) {
            var value = 1L;
            var loop = 0;

            while (value != key) {
                loop++;
                value *= subject;
                value %= 20201227;
            }

            return loop;
        }

        static long CalculateEncryptionKey(long subject, long loop) {
            var value = 1L;

            for (var i = 0L; i < loop; i++) {
                value *= subject;
                value %= 20201227;
            }

            return value;
        }
    }
}