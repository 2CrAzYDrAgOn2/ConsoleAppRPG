namespace ConsoleApp32
{
    public class Hero(long hp = 100, long dmg = 10, long armory = 1, long neededXp = 100, long xp = 0, long lvl = 1)
    {
        public double DodgeChance { get; set; } = 10.0;
        public long Hp { get; set; } = (long)(hp * Math.Pow(1.2, lvl));
        public long Dmg { get; set; } = (long)(dmg * Math.Pow(1.2, lvl));
        public long Armory { get; set; } = (long)(armory * Math.Pow(1.2, lvl));
        public long NeededXp { get; set; } = (long)(neededXp * Math.Pow(1.5, lvl));
        public long Xp { get; set; } = xp;
        public long Lvl { get; set; } = lvl;

        /// <summary>
        /// GainXp()
        /// </summary>
        /// <param name="xp"></param>
        public void GainXp(long xp)
        {
            Xp += xp;
            Hp = (long)(100 * Math.Pow(1.2, Lvl));
        }

        /// <summary>
        /// LevelUp()
        /// </summary>
        public void LevelUp()
        {
            Xp -= NeededXp;
            Lvl++;
            Dmg = (long)(10 * Math.Pow(1.2, Lvl));
            Hp = (long)(100 * Math.Pow(1.2, Lvl));
            Armory = (long)(1 * Math.Pow(1.2, Lvl));
            NeededXp = (long)(100 * Math.Pow(1.5, Lvl));
            Console.WriteLine("Поздравляем! Уровень повышен, ваши характеристики улучшены!");
        }
    }
}