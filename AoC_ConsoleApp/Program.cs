using Advent_Of_Code_2022;
using Advent_Of_Code_2022.Puzzles;

namespace AoC_ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            PrintHeader();

            // Add/remove any puzzles you wish to run here
            List<IPuzzle> puzzles = new()
            {
                //new D01_CalorieCounting(),
                //new D02_RockPaperScissors(),
                new D03_RucksackReorganization(),
            };

            foreach (IPuzzle puzzle in puzzles)
            {
                Console.WriteLine($"Day {puzzle.Day}: {puzzle.Name}");
                Console.WriteLine($"Part 1: {puzzle.Run_Part1()}");
                Console.WriteLine($"Part 2: {puzzle.Run_Part2()}\n");
            }
        }


        static void PrintHeader()
        {
            Console.WriteLine(
                "==================    Advent of Code  2022    ==================\n\n"
                );
        }

    }
}