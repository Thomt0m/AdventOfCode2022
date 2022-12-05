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
                //new D03_RucksackReorganization(),
                //new D04_CampCleanup(),
                new D05_SupplyStacks(),
            };

            foreach (IPuzzle puzzle in puzzles)
            {
                Console.WriteLine($"Day {puzzle.Day}: {puzzle.Name}");

                string answer1;
                try { answer1 = puzzle.Run_Part1(); }
                catch (NotImplementedException) { answer1 = "Not yet completed"; }
                string answer2;
                try { answer2 = puzzle.Run_Part2(); }
                catch (NotImplementedException) { answer2 = "Not yet completed"; }

                Console.WriteLine($"Part 1: {answer1}");
                Console.WriteLine($"Part 2: {answer2}\n");
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