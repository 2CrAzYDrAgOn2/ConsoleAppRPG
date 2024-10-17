namespace ConsoleApp32
{
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
}