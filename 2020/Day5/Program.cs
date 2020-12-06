using System;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            var passes = input.Select(l => new BoardingPass(l)).ToList();

            Console.WriteLine("Solving puzzle 1...");
            Console.WriteLine($"Highest seat number is {passes.Max(p=>p.SeatNumber)}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var mySeat = -1;
            var seats = passes.Select(p => p.SeatNumber).ToArray();
            for (int seat=7; seat< 8*127;seat++) {
                if (!seats.Contains(seat) && seats.Contains(seat + 1) && seats.Contains(seat - 1)) {
                    mySeat = seat;
                    break;
                }
            }
            Console.WriteLine($"My seat is {mySeat}");
        }
    }

    class BoardingPass
    {
        readonly string value;

        public BoardingPass(string value) {
            this.value = value;
        }        

        public int Row => Convert.ToInt32(value.Substring(0, 7).Replace('F', '0').Replace('B', '1'),2);

        public int Seat => Convert.ToInt32(value.Substring(7).Replace('L','0').Replace('R', '1'), 2);

        public int SeatNumber => 8 * Row + Seat;
    }
}
