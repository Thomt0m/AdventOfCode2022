namespace Advent_Of_Code_2022.Puzzles
{
    public class TestPuzzle : IPuzzle
    {
        public int Day => 0;

        public string Name => "TestPuzzle";

        private readonly string[] _input;

        public TestPuzzle()
        {
            _input = FileReader.GetInput(Name);
        }

        public string Run_Part1()
        {
            string x = "something";
            string y = "something else";
            string xy = x + y;
            return _input[0] + xy;
        }

        public string Run_Part2()
        {
            return _input[1];
        }
    }
}
