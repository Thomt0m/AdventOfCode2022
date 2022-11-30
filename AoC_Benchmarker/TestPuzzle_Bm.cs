using Advent_Of_Code_2022.Puzzles;
using Advent_Of_Code_2022;
using BenchmarkDotNet.Attributes;

namespace AoC_Benchmarker
{
    [MemoryDiagnoser]
    public class TestPuzzle_Bm
    {
        private readonly IPuzzle _testPuzzle = new TestPuzzle();

        [Benchmark]
        public void TestPuzzle_Part1()
        {
            _testPuzzle.Run_Part1();
        }

        [Benchmark]
        public void TestPuzzle_Part2()
        {
            _testPuzzle.Run_Part2();
        }
    }
}
