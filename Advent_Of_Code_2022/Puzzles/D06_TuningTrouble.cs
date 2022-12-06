namespace Advent_Of_Code_2022.Puzzles
{
    public class D06_TuningTrouble : IPuzzle
    {

        /*

        --- Day 6: Tuning Trouble ---
        The preparations are finally complete; you and the Elves leave camp on foot and begin to make your way toward the star fruit grove.

        As you move through the dense undergrowth, one of the Elves gives you a handheld device. He says that it has many fancy features, but the most important one to set up right now is the communication system.

        However, because he's heard you have significant experience dealing with signal-based systems, he convinced the other Elves that it would be okay to give you their one malfunctioning device - surely you'll have no problem fixing it.

        As if inspired by comedic timing, the device emits a few colorful sparks.

        To be able to communicate with the Elves, the device needs to lock on to their signal. The signal is a series of seemingly-random characters that the device receives one at a time.

        To fix the communication system, you need to add a subroutine to the device that detects a start-of-packet marker in the datastream. In the protocol being used by the Elves, the start of a packet is indicated by a sequence of four characters that are all different.

        The device will send your subroutine a datastream buffer (your puzzle input); your subroutine needs to identify the first position where the four most recently received characters were all different. Specifically, it needs to report the number of characters from the beginning of the buffer to the end of the first such four-character marker.

        For example, suppose you receive the following datastream buffer:

        mjqjpqmgbljsphdztnvjfqwrcgsmlb
        After the first three characters (mjq) have been received, there haven't been enough characters received yet to find the marker. The first time a marker could occur is after the fourth character is received, making the most recent four characters mjqj. Because j is repeated, this isn't a marker.

        The first time a marker appears is after the seventh character arrives. Once it does, the last four characters received are jpqm, which are all different. In this case, your subroutine should report the value 7, because the first start-of-packet marker is complete after 7 characters have been processed.

        Here are a few more examples:

        bvwbjplbgvbhsrlpgdmjqwftvncz: first marker after character 5
        nppdvjthqldpwncqszvftbrmjlhg: first marker after character 6
        nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg: first marker after character 10
        zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw: first marker after character 11
        How many characters need to be processed before the first start-of-packet marker is detected?

        Your puzzle answer was 1707.

        --- Part Two ---
        Your device's communication system is correctly detecting packets, but still isn't working. It looks like it also needs to look for messages.

        A start-of-message marker is just like a start-of-packet marker, except it consists of 14 distinct characters rather than 4.

        Here are the first positions of start-of-message markers for all of the above examples:

        mjqjpqmgbljsphdztnvjfqwrcgsmlb: first marker after character 19
        bvwbjplbgvbhsrlpgdmjqwftvncz: first marker after character 23
        nppdvjthqldpwncqszvftbrmjlhg: first marker after character 23
        nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg: first marker after character 29
        zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw: first marker after character 26
        How many characters need to be processed before the first start-of-message marker is detected?

        Your puzzle answer was 3697.

        Both parts of this puzzle are complete! They provide two gold stars: **

        */


        public int Day => 6;

        public string Name => "Tuning Trouble";

        private readonly string[] _input;


        public D06_TuningTrouble()
        {
            _input = FileReader.GetInput(Day);
        }


        public string Run_Part1()
        {
            ReadOnlySpan<char> line;
            //line = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
            //line = "bvwbjplbgvbhsrlpgdmjqwftvncz";
            //line = "nppdvjthqldpwncqszvftbrmjlhg";
            //line = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg";
            //line = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";
            line = _input[0];

            return FindStartOfPacket(line, 4);
        }

        public string Run_Part2()
        {
            ReadOnlySpan<char> line;
            //line = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
            //line = "bvwbjplbgvbhsrlpgdmjqwftvncz";
            //line = "nppdvjthqldpwncqszvftbrmjlhg";
            //line = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg";
            //line = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";
            line = _input[0];

            return FindStartOfPacket(line, 14);
        }


        private string FindStartOfPacket(ReadOnlySpan<char> line, int packetLength)
        {
            bool restart = false;

            for (int start = 0; start < line.Length - packetLength; start++)
            {
                for (int p1 = start; p1 < start + packetLength - 1; p1++)
                {
                    for (int p2 = p1 + 1; p2 < start + packetLength; p2++)
                    {
                        if (line[p1] == line[p2])
                        {
                            start = p1;
                            restart = true;
                        }
                        if (restart) break;
                    }
                    if (restart) break;
                }
                if (!restart) return (start + packetLength).ToString();
                restart = false;
            }
            return "No \'start-of-packet\' marker detected";
        }
    }
}
