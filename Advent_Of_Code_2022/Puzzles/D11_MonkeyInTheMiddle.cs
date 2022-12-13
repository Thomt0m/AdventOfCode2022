namespace Advent_Of_Code_2022.Puzzles
{
    public class D11_MonkeyInTheMiddle : IPuzzle
    {
        public int Day => 11;

        public string Name => "Monkey in the Middle";

        private readonly string[] _input;

        private readonly string[] _inputExample =
        {
            "Monkey 0:",
            "  Starting items: 79, 98",
            "  Operation: new = old * 19",
            "  Test: divisible by 23",
            "    If true: throw to monkey 2",
            "    If false: throw to monkey 3",
            "",
            "Monkey 1:",
            "  Starting items: 54, 65, 75, 74",
            "  Operation: new = old + 6",
            "  Test: divisible by 19",
            "    If true: throw to monkey 2",
            "    If false: throw to monkey 0",
            "",
            "Monkey 2:",
            "  Starting items: 79, 60, 97",
            "  Operation: new = old * old",
            "  Test: divisible by 13",
            "    If true: throw to monkey 1",
            "    If false: throw to monkey 3",
            "",
            "Monkey 3:",
            "  Starting items: 74",
            "  Operation: new = old + 3",
            "  Test: divisible by 17",
            "    If true: throw to monkey 0",
            "    If false: throw to monkey 1",
        };


        public D11_MonkeyInTheMiddle()
        {
            _input = FileReader.GetInput(Day);
        }


        public string Run_Part1()
        {
            string[] input;
            //input = _inputExample;
            input = _input;

            Monkey[] monkeys = GetMonkeys(input);

            int numberOfRounds = 20;
            int[] monkeyBusyness = new int[monkeys.Length];
            long worryLevel;
            int reliefAmount = 3;
            Monkey monkey;
            long item;
            //string[,] operationComponents = GetOperationComponents(input);
            //string[] testComponents = GetTestComponents(input);
            for (int round = 0; round < numberOfRounds; round++)
            {
                //Console.WriteLine($"---- ROUND {round} ----");
                for (int m = 0; m < monkeys.Length; m++)
                {
                    monkey = monkeys[m];
                    //Console.WriteLine($"Monkey {monkey.Name}:");
                    while (monkey.Items.Any())
                    {
                        item = monkey.Items.First();
                        monkeyBusyness[m]++;
                        //Console.WriteLine($"  Monkey {monkey.Name} inspects an item with a worry level of {item}");
                        worryLevel = monkey.Operation(item);
                        //Console.WriteLine("    Worry level is " + operationComponents[m, 0] == "*" ? "multiplied " : "increased " + $"by {operationComponents[m, 1]} to {worryLevel}");
                        worryLevel /= reliefAmount;
                        //Console.WriteLine($"    Monkey gets bored with item. Worry level is divided by {reliefAmount} to {worryLevel}.");
                        //string testResult = monkey.Test(worryLevel) ? "" : "not ";
                        //Console.WriteLine($"    Current worry level is {testResult}divisible by {testComponents[m]}.");
                        monkeys[monkey.Test(worryLevel) ? monkey.OnTrueThrowTo : monkey.OnFalseThrowTo].Items.Add(worryLevel);
                        monkey.Items.RemoveAt(0);
                        //string targetMonkey = monkey.Test(worryLevel) ? monkey.OnTrueThrowTo.ToString() : monkey.OnFalseThrowTo.ToString();
                        //Console.WriteLine($"    Item with worry level {worryLevel} is thrown to monkey {targetMonkey}");
                    }
                    //Console.WriteLine($"  Monkey {monkey.Name} has inspected {monkeyBusyness[m]} items");
                }

                //foreach (Monkey monkey1 in monkeys)
                //    Console.WriteLine(monkey1);
                
            }

            int largest = 0;
            int secondLargest = 0;
            for (int i = 0; i < monkeyBusyness.Length; i++)
            {
                if (monkeyBusyness[i] > largest)
                {
                    secondLargest = largest;
                    largest = monkeyBusyness[i];
                    //Console.WriteLine($"Monkey {i} handled {largest} items");
                }
                else if (monkeyBusyness[i] > secondLargest)
                {
                    secondLargest = monkeyBusyness[i];
                    //Console.WriteLine($"Monkey {i} handled {secondLargest} items");
                }
            }

            return (largest * secondLargest).ToString();
        }

        public string Run_Part2()
        {
            string[] input;
            //input = _inputExample;
            input = _input;

            int[] testValues = GetTestDivisionValues(input);
            int lcm = GetLowestCommonMultiple(testValues);

            Monkey[] monkeys = GetMonkeys(input);

            int numberOfRounds = 10000;
            int[] monkeyBusyness = new int[monkeys.Length];
            long worryLevel;
            Monkey monkey;
            long item;
            for (int round = 0; round < numberOfRounds; round++)
            {
                for (int m = 0; m < monkeys.Length; m++)
                {
                    monkey = monkeys[m];
                    while (monkey.Items.Any())
                    {
                        item = monkey.Items[0];
                        monkeyBusyness[m]++;
                        worryLevel = monkey.Operation(item);
                        worryLevel %= lcm;
                        monkeys[monkey.Test(worryLevel) ? monkey.OnTrueThrowTo : monkey.OnFalseThrowTo].Items.Add(worryLevel);
                        monkey.Items.RemoveAt(0);
                    }
                }

                //if (round % 1000 == 0)
                //{
                //    Console.WriteLine($"== After round {round} ==");
                //    for (int x = 0; x < monkeys.Length; x++)
                //    {
                //        Console.WriteLine($"Monkey {x} handled {monkeyBusyness[x]} items");
                //    };
                //}
            }

            //for (int x = 0; x < monkeys.Length; x++)
            //{
            //    Console.WriteLine($"Monkey {x} handled {monkeyBusyness[x]} items");
            //};

            int largest = 0;
            int secondLargest = 0;
            for (int i = 0; i < monkeyBusyness.Length; i++)
            {
                if (monkeyBusyness[i] > largest)
                {
                    secondLargest = largest;
                    largest = monkeyBusyness[i];
                }
                else if (monkeyBusyness[i] > secondLargest)
                {
                    secondLargest = monkeyBusyness[i];
                }
            }

            return ((long)largest * secondLargest).ToString();
        }



        private static Monkey[] GetMonkeys(string[] input)
        {
            Monkey[] monkeys = new Monkey[(input.Length + 1) / 7];

            int ix7;
            for (int i = 0; i < monkeys.Length; i++)
            {
                ix7 = i * 7;
                monkeys[i] = new(
                    name: i.ToString(),
                    items: GetItems(input[ix7 + 1]),
                    operation: GetOperation(input[ix7 + 2]),
                    test: GetTest(input[ix7 + 3]),
                    onTrueThrowTo: GetTargetMonkey(input[ix7 + 4]),
                    onFalseThrowTo: GetTargetMonkey(input[ix7 + 5])
                    );
            }

            return monkeys;
        }

        private static List<long> GetItems(string itemString)
        {
            return new List<long>(itemString[18..].Split(", ").Select(x => long.Parse(x)));
        }

        private static Func<long, long> GetOperation(string operationString)
        {
            string[] components = operationString[19..].Split(' ');

            return (components[1], components[2]) switch
            {
                ("*", "old") => new Func<long, long>(x => x * x),
                ("+", "old") => new Func<long, long>(x => x + x),
                ("*", _) => new Func<long, long>(x => x * long.Parse(components[2])),
                ("+", _) => new Func<long, long>(x => x + long.Parse(components[2])),
                _ => throw new Exception($"Unknown components in {nameof(operationString)} {operationString}")
            };
        }

        private static Func<long, bool> GetTest(string testString)
        {
            return new Func<long, bool>(x => x % long.Parse(testString[21..]) == 0);
        }

        private static int GetTargetMonkey(string targetMonkeyString)
        {
            return int.Parse(targetMonkeyString.Split(' ').Last());
        }

        private static int[] GetTestDivisionValues(string[] input)
        {
            int[] testComponents = new int[(input.Length + 1) / 7];
            for (int i = 0; i < testComponents.Length; i++)
                testComponents[i] = int.Parse(input[i * 7 + 3][21..]);
            return testComponents;
        }

        // copied from https://www.geeksforgeeks.org/lcm-of-given-array-elements/
        private static int GetLowestCommonMultiple(int[] element_array)
        {
            int lcm_of_array_elements = 1;
            int divisor = 2;

            while (true)
            {
                int counter = 0;
                bool divisible = false;
                for (int i = 0; i < element_array.Length; i++)
                {

                    // lcm_of_array_elements (n1, n2, ... 0) = 0.
                    // For negative number we convert into
                    // positive and calculate lcm_of_array_elements.
                    if (element_array[i] == 0)
                    {
                        return 0;
                    }
                    else if (element_array[i] < 0)
                    {
                        element_array[i] = element_array[i] * (-1);
                    }
                    if (element_array[i] == 1)
                    {
                        counter++;
                    }

                    // Divide element_array by divisor if complete
                    // division i.e. without remainder then replace
                    // number with quotient; used for find next factor
                    if (element_array[i] % divisor == 0)
                    {
                        divisible = true;
                        element_array[i] = element_array[i] / divisor;
                    }
                }

                // If divisor able to completely divide any number
                // from array multiply with lcm_of_array_elements
                // and store into lcm_of_array_elements and continue
                // to same divisor for next factor finding.
                // else increment divisor
                if (divisible)
                {
                    lcm_of_array_elements *= divisor;
                }
                else
                {
                    divisor++;
                }

                // Check if all element_array is 1 indicate
                // we found all factors and terminate while loop.
                if (counter == element_array.Length)
                {
                    return lcm_of_array_elements;
                }
            }
        }

        // debug method
        private static string[,] GetOperationComponents(string[] input)
        {
            string[,] operationComponents = new string[(input.Length + 1) / 7, 2];
            int ix7;
            string[] components;
            for (int i = 0; i < operationComponents.GetLength(0); i++)
            {
                ix7 = i * 7;
                components = input[ix7 + 2][23..].Split(' ');
                operationComponents[i, 0] = components[0];
                operationComponents[i, 1] = components[1];
            }
            return operationComponents;
        }

        // debug method
        private static string[] GetTestComponents(string[] input)
        {
            string[] testComponents = new string[(input.Length + 1) / 7];
            for (int i = 0; i < testComponents.Length; i++)
                testComponents[i] = input[i * 7 + 3][21..];
            return testComponents;
        }


        private class Monkey
        {
            public string Name;
            public List<long> Items;
            public Func<long, long> Operation;
            public Func<long, bool> Test;
            public int OnTrueThrowTo;
            public int OnFalseThrowTo;

            public Monkey(string name, IEnumerable<long> items, Func<long, long> operation, Func<long, bool> test, int onTrueThrowTo, int onFalseThrowTo)
            {
                Name = name;
                Items = items.ToList();
                Operation = operation;
                Test = test;
                OnTrueThrowTo = onTrueThrowTo < 0 ? throw new Exception($"param {nameof(onTrueThrowTo)} cant be smaller than 0") : onTrueThrowTo;
                OnFalseThrowTo = onFalseThrowTo < 0 ? throw new Exception($"param {nameof(onFalseThrowTo)} cant be smaller than 0") : onFalseThrowTo;
            }

            public override string ToString()
            {
                string result = $"Monkey {Name}:\n";
                result += $"  Current Items: ";
                foreach (int item in Items)
                    result += item + ", ";
                return result;
            }
        }
    }
}
