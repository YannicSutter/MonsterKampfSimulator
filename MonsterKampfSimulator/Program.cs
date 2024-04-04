namespace MonsterKampfSimulator
{
    internal class Program
    {
        static void Main(string[] args)
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
