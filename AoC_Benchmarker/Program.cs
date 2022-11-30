using BenchmarkDotNet.Running;


/*
 * To run the benchmarks, make sure to build to 'Release'-config and run with 'Start Without Debugging'
 * 
 * Select which benchmarks to run by adding/removing them for the body of 'Main'
 */


namespace AoC_Benchmarker
{
    public class Program
    {

        static void Main(string[] args)
        {
            BenchmarkRunner.Run<TestPuzzle_Bm>();
        }


    }
}