namespace ConsoleApp32
{
    public class Program
    {
        /// <summary>
        /// Main()
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
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
                    decimal enemyClass = ParseEnemyClass();
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
        public static decimal ParseEnemyClass()
        {
            decimal enemyClass = long.Parse(Console.ReadLine());
            return enemyClass switch
            {
                1 => 0.74m,
                2 => 1.24m,
                3 => 1.74m,
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
            hero.Hp -= Math.Max(0, enemy.Atk - hero.Armory);
        }
    }

    public class Hero(long hp = 100, long dmg = 10, long armory = 1, long xp = 0, long lvl = 1, long neededXp = 100)
    {
        public long Hp { get; set; } = hp * lvl;
        public long Dmg { get; set; } = dmg * lvl;
        public long Armory { get; set; } = armory * lvl;
        public long Xp { get; set; } = xp;
        public long Lvl { get; set; } = lvl;
        public long NeededXp { get; set; } = neededXp * lvl;

        /// <summary>
        /// GainXp()
        /// </summary>
        /// <param name="xp"></param>
        public void GainXp(long xp)
        {
            Xp += xp;
            Hp = Lvl * 100;
        }

        /// <summary>
        /// LevelUp()
        /// </summary>
        public void LevelUp()
        {
            Xp -= NeededXp;
            Lvl++;
            Dmg = Lvl * 10;
            Hp = Lvl * 100;
            Armory = Lvl;
            NeededXp = Lvl * 100;
            Console.WriteLine("Поздравляем! Уровень повышен, ваши характеристики улучшены!");
        }
    }

    public class Enemy(long lvl, decimal enemyClass, long hp = 100, long atk = 4, long xp = 10, bool isLive = true)
    {
        public long Lvl { get; set; } = lvl;
        public decimal EnemyClass { get; set; } = enemyClass;
        public long Hp { get; set; } = (long)(hp * lvl * enemyClass);
        public long Atk { get; set; } = (long)(atk * lvl * enemyClass);
        public long Xp { get; set; } = (long)(xp * lvl * enemyClass);
        public bool IsLive { get; set; } = isLive;
    }
}