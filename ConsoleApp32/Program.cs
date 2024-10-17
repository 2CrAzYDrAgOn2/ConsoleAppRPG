namespace ConsoleApp32
{
    public class Program
    {
        /// <summary>
        /// Main()
        /// </summary>
        /// <param name="args"></param>
        public static void Main()
        {
            Hero hero = new();
            while (true)
            {
                try
                {
                    GetHero(hero);
                    Console.Write($"Введите номер уровня врага, которого вы хотите убить: ");
                    long level = Math.Abs(long.Parse(Console.ReadLine()));
                    Console.Write($"Введите 1, чтобы убить легкого врага. Введите 2, чтобы убить среднего врага. Введите 3, чтобы убить босса.");
                    double enemyClass = ParseEnemyClass();
                    Enemy enemy = new(level, enemyClass);
                    GetEnemy(enemy);
                    StartGame(hero, enemy);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// StartGame()
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="enemy"></param>
        public static void StartGame(Hero hero, Enemy enemy)
        {
            while (enemy.IsLive)
            {
                Console.WriteLine("Атакует герой:");
                AtkHero(hero, enemy);
                GetEnemy(enemy);
                if (!enemy.IsLive)
                {
                    ProcessVictory(hero, enemy);
                    return;
                }
                if (ExitOrContinue()) return;
                Console.WriteLine("Атакует враг:");
                AtkEnemy(hero, enemy);
                GetHero(hero);
                if (hero.Hp <= 0)
                {
                    while (true)
                    {
                        Console.WriteLine("Вы проиграли! Запустите игру заново...");
                        Console.ReadKey();
                    }
                }
                if (ExitOrContinue()) return;
            }
        }

        /// <summary>
        /// ProcessVictory()
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="enemy"></param>
        public static void ProcessVictory(Hero hero, Enemy enemy)
        {
            Console.WriteLine($"Вы победили! Вам начислено {enemy.Xp} опыта! Ваше здоровье восстановлено!\n");
            hero.GainXp(enemy.Xp);
            if (hero.Xp >= hero.NeededXp)
            {
                hero.LevelUp();
            }
        }

        /// <summary>
        /// ExitOrContinue()
        /// </summary>
        /// <returns></returns>
        public static bool ExitOrContinue()
        {
            Console.WriteLine("Нажмите Enter для продолжения. Введите 1 для выхода из боя.");
            string key = Console.ReadLine();
            return key == "1";
        }

        /// <summary>
        /// ParseEnemyClass()
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static double ParseEnemyClass()
        {
            double enemyClass = byte.Parse(Console.ReadLine());
            return enemyClass switch
            {
                1 => 0.74,
                2 => 1.24,
                3 => 1.74,
                _ => throw new ArgumentException("Неверный выбор класса врага.")
            };
        }

        /// <summary>
        /// GetHero()
        /// </summary>
        /// <param name="hero"></param>
        public static void GetHero(Hero hero)
        {
            Console.WriteLine($"Герой:\n\nЗдоровье: {hero.Hp}\nАтака: {hero.Dmg}\nЗащита: {hero.Armory}\nОпыт: {hero.Xp}/{hero.NeededXp}\nУровень: {hero.Lvl}\n");
        }

        /// <summary>
        /// GetEnemy()
        /// </summary>
        /// <param name="enemy"></param>
        public static void GetEnemy(Enemy enemy)
        {
            Console.WriteLine($"Враг:\n\nУровень: {enemy.Lvl}\nЗдоровье: {enemy.Hp}\nАтака: {enemy.Atk}\n");
        }

        /// <summary>
        /// AtkHero()
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="enemy"></param>
        public static void AtkHero(Hero hero, Enemy enemy)
        {
            Random random = new();
            if (random.Next(0, 100) < enemy.DodgeChance)
            {
                Console.WriteLine("Враг уклонился от атаки!");
                return;
            }
            enemy.Hp -= hero.Dmg;
            if (enemy.Hp <= 0)
            {
                enemy.IsLive = false;
            }
        }

        /// <summary>
        /// AtkEnemy()
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="enemy"></param>
        public static void AtkEnemy(Hero hero, Enemy enemy)
        {
            Random random = new();
            if (random.Next(0, 100) < hero.DodgeChance)
            {
                Console.WriteLine("Герой уклонился от атаки!");
                return;
            }
            hero.Hp -= Math.Max(0, enemy.Atk - hero.Armory);
        }
    }
}