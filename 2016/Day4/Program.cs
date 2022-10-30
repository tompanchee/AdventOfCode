using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day4
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var data = Parse(input);

            Console.WriteLine("Solving puzzle 1...");
            var sectorSum = data
                .Where(d => d.room.CalculateCheckSum() == d.checkSum)
                .Select(d => d.room.Sector)
                .Sum();
            Console.WriteLine($"Sum of valid sectors is {sectorSum}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var deciphered = data
                .Where(d => d.room.CalculateCheckSum() == d.checkSum)
                .Select(d => d.room)
                .ToDictionary(r => r.Decipher(), r => r.Sector);
            var secretRoomSector = deciphered
                .Where(d => d.Key == "northpole object storage")
                .Select(p => p.Value)
                .SingleOrDefault();
            Console.WriteLine($"North Pole objects are in sector {secretRoomSector}");
        }

        static IList<(Room room, string checkSum)> Parse(string[] input) {
            return (from line in input
                    let room = Room.FromInput(line[..line.IndexOf('[')])
                    let checkSum = line[(line.IndexOf('[') + 1)..^1]
                    select (room, checkSum))
                .ToList();
        }
    }
}