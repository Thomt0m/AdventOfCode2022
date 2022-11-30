namespace Advent_Of_Code_2022
{
    public interface IPuzzle
    {
        public int Day { get; }

        public string Name { get; }

        public string Run_Part1();

        public string Run_Part2();
    }
}
