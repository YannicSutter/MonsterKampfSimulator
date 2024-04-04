namespace MonsterKampfSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Select game mode (Street Fight = 1, War = 2)");
                string mode = Console.ReadLine();
                Console.WriteLine();
                if (mode == "1")
                {
                    StreetFight();
                    break;
                }
                else if (mode == "2")
                {
                    War();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            }
        }

        public static void War()
        {
            while (true)
            {
                Console.WriteLine("First team");
                List<Monster> army1 = CreateArmy();
                Console.WriteLine("\nSecond team");
                List<Monster> army2 = CreateArmy();

                if (army1.First().GetType().Name != army2.First().GetType().Name)
                {
                    while (army1.Any() && army2.Any())
                    {
                        Tuple<Monster, int> results = Fight(army1.First(), army2.First());
                        if (results.Item1.GetType().Name == army1.First().GetType().Name)
                        {
                            army2.RemoveAt(0);
                        }
                        else
                        {
                            army1.RemoveAt(0);
                        }
                    }
                    if (army1.Any())
                    {
                        Console.WriteLine($"The {army1.First().GetType().Name}s won!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"The {army2.First().GetType().Name}s won!");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Civil wars are not allowed. Please use different types of monsters.\n");
                }

            }
        }

        public static List<Monster> CreateArmy()
        {
            List<Monster> output = new List<Monster>();
            Random random = new Random();

            while (true)
            {
                Console.WriteLine("Enter type of the monster (Orc = 1, Troll = 2, Goblin = 3)");
                string monsterType = Console.ReadLine();
                if (monsterType == "1" || monsterType == "2" || monsterType == "3")
                {
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("Enter size of army.");
                            int sizeOfArmy = Int32.Parse(Console.ReadLine());
                            for (int i = 0; i < sizeOfArmy; i++)
                            {
                                output.Add(CreateMonster(monsterType, "dummy", [random.Next(1, 1000), random.Next(20, 100), random.Next(1, 20), random.Next(1, 100)]));
                            }
                            return output;
                        }
                        catch
                        {
                            Console.WriteLine("Invalid input. Try again.");
                        }
                    }
                }
                Console.WriteLine("Invalid input. Try again.");
            }
        }

        public static void StreetFight()
        {
            while (true)
            {
                Console.WriteLine("First Monster");
                Monster monster1 = ReadMonsterStats();
                Console.WriteLine("\nSecond Monster");
                Monster monster2 = ReadMonsterStats();

                if (monster1.GetType().Name != monster2.GetType().Name)
                {
                    Tuple<Monster, int> results = Fight(monster1, monster2);

                    if (results.Item2 > 0)
                    {
                        Console.WriteLine($"\nYour winner is {results.Item1.Name}!");
                        Console.WriteLine($"The fight lasted {results.Item2} rounds");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("This fight is endless. We will never know who wins.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Civil wars are not allowed. Please use different types of monsters.\n");
                }
            }
        }

        public static Monster ReadMonsterStats()
        {
            while (true)
            {
                Console.WriteLine("Enter type of the monster (Orc = 1, Troll = 2, Goblin = 3");
                string monsterType = Console.ReadLine();
                if (monsterType == "1" || monsterType == "2" || monsterType == "3")
                {
                    while (true)
                    {
                        Console.WriteLine("Enter stats of the monster (name health attack defense speed)");
                        string monsterStatsText = Console.ReadLine();
                        try
                        {
                            string monsterName = monsterStatsText.Split()[0];
                            float[] monsterStats = Array.ConvertAll(monsterStatsText.Split().Skip(1).ToArray(), s => float.Parse(s));
                            return CreateMonster(monsterType, monsterName, monsterStats);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid Input. Try again.");
                        }
                    }
                }
                Console.WriteLine("Invalid input. Try again.");
            }
        }

        public static Monster CreateMonster(string type, string name, float[] stats)
        {
            switch (type)
            {
                case "1":
                    return new Orc(name, stats[0], stats[1], stats[2], stats[3]);
                case "2":
                    return new Troll(name, stats[0], stats[1], stats[2], stats[3]);
                default:
                    return new Goblin(name, stats[0], stats[1], stats[2], stats[3]);
            }
        }

        public static Tuple<Monster, int> Fight(Monster monster1,  Monster monster2)
        {
            int roundCount = 0;
            if (monster2.Ap -  monster1.Dp <= 0 && monster1.Ap - monster2.Dp <= 0)
            {
                return Tuple.Create(monster1, 0);
            }
            while (monster1.IsAlive && monster2.IsAlive)
            {
                if (monster1.S == monster2.S)
                {
                    Random random = new Random();
                    if (random.NextDouble() < 0.5)
                    {
                        Engage(monster1, monster2);
                    }
                    else
                    {
                        Engage(monster2, monster1);
                    }
                }
                else if (monster1.S > monster2.S)
                {
                    Engage(monster1, monster2);
                }
                else
                {
                    Engage(monster2, monster1);
                }
            }
            if (monster1.IsAlive)
            {
                return Tuple.Create(monster1, roundCount);
            }
            else
            {
                return Tuple.Create(monster2, roundCount);
            }
            
            void Engage(Monster monster1, Monster monster2)
            {
                monster1.Attack(monster2);
                roundCount++;
                if (monster2.IsAlive)
                {
                    monster2.Attack(monster1);
                    roundCount++;
                }
            }
        }
    }
}
