using System.Text;

namespace Advent_Of_Code_2022.Puzzles
{
    public class D08_TreetopTreeHouse : IPuzzle
    {

        /*

        --- Day 8: Treetop Tree House ---
        The expedition comes across a peculiar patch of tall trees all planted carefully in a grid. The Elves explain that a previous expedition planted these trees as a reforestation effort. Now, they're curious if this would be a good location for a tree house.

        First, determine whether there is enough tree cover here to keep a tree house hidden. To do this, you need to count the number of trees that are visible from outside the grid when looking directly along a row or column.

        The Elves have already launched a quadcopter to generate a map with the height of each tree (your puzzle input). For example:

        30373
        25512
        65332
        33549
        35390
        Each tree is represented as a single digit whose value is its height, where 0 is the shortest and 9 is the tallest.

        A tree is visible if all of the other trees between it and an edge of the grid are shorter than it. Only consider trees in the same row or column; that is, only look up, down, left, or right from any given tree.

        All of the trees around the edge of the grid are visible - since they are already on the edge, there are no trees to block the view. In this example, that only leaves the interior nine trees to consider:

        The top-left 5 is visible from the left and top. (It isn't visible from the right or bottom since other trees of height 5 are in the way.)
        The top-middle 5 is visible from the top and right.
        The top-right 1 is not visible from any direction; for it to be visible, there would need to only be trees of height 0 between it and an edge.
        The left-middle 5 is visible, but only from the right.
        The center 3 is not visible from any direction; for it to be visible, there would need to be only trees of at most height 2 between it and an edge.
        The right-middle 3 is visible from the right.
        In the bottom row, the middle 5 is visible, but the 3 and 4 are not.
        With 16 trees visible on the edge and another 5 visible in the interior, a total of 21 trees are visible in this arrangement.

        Consider your map; how many trees are visible from outside the grid?

        Your puzzle answer was 1681.

        --- Part Two ---
        Content with the amount of tree cover available, the Elves just need to know the best spot to build their tree house: they would like to be able to see a lot of trees.

        To measure the viewing distance from a given tree, look up, down, left, and right from that tree; stop if you reach an edge or at the first tree that is the same height or taller than the tree under consideration. (If a tree is right on the edge, at least one of its viewing distances will be zero.)

        The Elves don't care about distant trees taller than those found by the rules above; the proposed tree house has large eaves to keep it dry, so they wouldn't be able to see higher than the tree house anyway.

        In the example above, consider the middle 5 in the second row:

        30373
        25512
        65332
        33549
        35390
        Looking up, its view is not blocked; it can see 1 tree (of height 3).
        Looking left, its view is blocked immediately; it can see only 1 tree (of height 5, right next to it).
        Looking right, its view is not blocked; it can see 2 trees.
        Looking down, its view is blocked eventually; it can see 2 trees (one of height 3, then the tree of height 5 that blocks its view).
        A tree's scenic score is found by multiplying together its viewing distance in each of the four directions. For this tree, this is 4 (found by multiplying 1 * 1 * 2 * 2).

        However, you can do even better: consider the tree of height 5 in the middle of the fourth row:

        30373
        25512
        65332
        33549
        35390
        Looking up, its view is blocked at 2 trees (by another tree with a height of 5).
        Looking left, its view is not blocked; it can see 2 trees.
        Looking down, its view is also not blocked; it can see 1 tree.
        Looking right, its view is blocked at 2 trees (by a massive tree of height 9).
        This tree's scenic score is 8 (2 * 2 * 1 * 2); this is the ideal spot for the tree house.

        Consider each tree on your map. What is the highest scenic score possible for any tree?

        Your puzzle answer was 201684.

        Both parts of this puzzle are complete! They provide two gold stars: **

        */


        public int Day => 8;

        public string Name => "Treetop Tree House";

        private readonly string[] _inputRaw;

        private readonly int[,] _input;

        private readonly int[,] _inputExample = new[,]
        {
            { 3,0,3,7,3 },
            { 2,5,5,1,2 },
            { 6,5,3,3,2 },
            { 3,3,5,4,9 },
            { 3,5,3,9,0 }
        };


        public D08_TreetopTreeHouse()
        {
            _inputRaw = FileReader.GetInput(Day);

            _input = new int[_inputRaw.Length, _inputRaw[0].Length];
            for (int m = 0; m < _inputRaw.Length; m++)
            {
                for (int n = 0; n < _inputRaw[m].Length; n++)
                {
                    _input[m, n] = _inputRaw[m][n] - '0';
                }
            }
        }


        public string Run_Part1()
        {
            int[,] input;
            input = _input;
            //input = _inputExample;

            bool[,] treeVisiblility = new bool[input.GetLength(0), input.GetLength(1)];
            int largestTree;

            //PrintGrid(input);

            // Vertical (row by row)
            for (int m = 0; m < input.GetLength(0); m++)
            {
                // Left to Right
                largestTree = -1;
                for (int i_LtR = 0; i_LtR < input.GetLength(1); i_LtR++)
                {
                    if (input[m, i_LtR] > largestTree)
                    {
                        treeVisiblility[m, i_LtR] = true;
                        largestTree = input[m, i_LtR];
                        if (largestTree == 9) break;
                    }
                }

                // Right to Left
                largestTree = -1;
                for (int i_RtL = input.GetLength(1) - 1; i_RtL >= 0; i_RtL--)
                {
                    if (input[m, i_RtL] > largestTree)
                    {
                        treeVisiblility[m, i_RtL] = true;
                        largestTree = input[m, i_RtL];
                        if (largestTree == 9) break;
                    }
                }
            }

            // Horizontal (column by column)
            for (int n = 0; n < input.GetLength(1); n++)
            {
                // Top to Bottom
                largestTree = -1;
                for (int i_TtB = 0; i_TtB < input.GetLength(0); i_TtB++)
                {
                    if (input[i_TtB, n] > largestTree)
                    {
                        treeVisiblility[i_TtB, n] = true;
                        largestTree = input[i_TtB, n];
                        if (largestTree == 9) break;
                    }
                }

                // Bottom to Top
                largestTree = -1;
                for (int i_BtT = input.GetLength(0) - 1; i_BtT >= 0; i_BtT--)
                {
                    if (input[i_BtT, n] > largestTree)
                    {
                        treeVisiblility[i_BtT, n] = true;
                        largestTree = input[i_BtT, n];
                        if (largestTree == 9) break;
                    }
                }
            }

            //PrintGrid(treeVisiblility);

            int totalTreesVisible = 0;
            for (int m = 0; m < treeVisiblility.GetLength(0); m++)
            {
                for (int n = 0; n < treeVisiblility.GetLength(1); n++)
                {
                    if (treeVisiblility[m, n]) totalTreesVisible++;
                }
            }

            return totalTreesVisible.ToString();
        }


        public string Run_Part2()
        {
            int[,] input;
            input = _input;
            //input = _inputExample;

            int[,] scenicScores = new int[input.GetLength(0), input.GetLength(1)];
            int treehouseHeight;
            int[] scenicScoreCardinal;
            for (int m = 0; m < scenicScores.GetLength(0); m++)
            {
                for (int n = 0; n < scenicScores.GetLength(1); n++)
                {
                    treehouseHeight = input[m, n];
                    scenicScoreCardinal = new int[4];

                    // North
                    for (int i_N = m - 1; i_N >= 0; i_N--)
                    {
                        scenicScoreCardinal[0]++;
                        if (input[i_N, n] >= treehouseHeight)
                            break;
                    }

                    // East
                    for (int i_E = n + 1; i_E < input.GetLength(1); i_E++)
                    {
                        scenicScoreCardinal[1]++;
                        if (input[m, i_E] >= treehouseHeight)
                            break;
                    }

                    // South
                    for (int i_S = m + 1; i_S < input.GetLength(0); i_S++)
                    {
                        scenicScoreCardinal[2]++;
                        if (input[i_S, n] >= treehouseHeight)
                            break;
                    }

                    // West
                    for (int i_W = n - 1; i_W >= 0; i_W--)
                    {
                        scenicScoreCardinal[3]++;
                        if (input[m, i_W] >= treehouseHeight)
                            break;
                    }

                    scenicScores[m, n] = scenicScoreCardinal[0] * scenicScoreCardinal[1] * scenicScoreCardinal[2] * scenicScoreCardinal[3];
                }
            }

            //PrintGrid(scenicScores);

            int highestScenicScor = 0;
            for (int m = 0; m < scenicScores.GetLength(0); m++)
            {
                for (int n = 0; n < scenicScores.GetLength(1); n++)
                {
                    if (scenicScores[m, n] > highestScenicScor)
                    {
                        highestScenicScor = scenicScores[m, n];
                    }
                }
            }

            return highestScenicScor.ToString();
        }




        private void PrintGrid(int[,] grid)
        {
            Console.WriteLine("Printing int[,] grid:\n");
            StringBuilder sb = new();
            for (int m = 0; m < grid.GetLength(0); m++)
            {
                sb.Clear();
                for (int n = 0; n < grid.GetLength(1); n++)
                {
                    sb.Append(grid[m, n]);
                }
                Console.WriteLine(sb.ToString());
            }
        }

        private void PrintGrid(bool[,] grid)
        {
            Console.WriteLine("Printing bool[,] grid:\n");
            StringBuilder sb = new();
            for (int m = 0; m < grid.GetLength(0); m++)
            {
                sb.Clear();
                for (int n = 0; n < grid.GetLength(1); n++)
                {
                    sb.Append(grid[m, n] ? 'X' : '0');
                }
                Console.WriteLine(sb.ToString());
            }
        }
    }
}
