namespace Advent_Of_Code_2022
{
    public class FileReader
    {
        private const string InputDir = "Inputs\\";
        private const string FileExtension = ".txt";

        private static string GetPath(int day) => $"{InputDir}{day:D2}{FileExtension}";
        private static string GetPath(string fileName) => $"{InputDir}{fileName}{FileExtension}";


        public static string[] GetInput(int day)
        {
            string path = GetPath(day);
            //if (!File.Exists(path)) return Array.Empty<string>();
            return File.ReadAllLines(path);
        }

        public static string[] GetInput(string fileName)
        {
            return File.ReadAllLines(GetPath(fileName));
        }


        public int[] GetInputAsInts(int day)
        {
            string[] strings = GetInput(day);
            int[] ints = new int[strings.Length];

            for (int i = 0; i < strings.Length; i++)
            {
                //if (!int.TryParse(strings[i], out ints[i]))
                //    Debug.WriteLine($"Unable to cast Character to Integer at line {i} in Input file {GetPath(day)}");
                ints[i] = int.Parse(strings[i]);
            }
            return ints;
        }
    }
}
