namespace ConsoleApp32
{
    public class Enemy(long lvl, double enemyClass, long hp = 100, long atk = 4, long xp = 10, bool isLive = true)
    {
        public double DodgeChance { get; set; } = 5.0;
        public long Lvl { get; set; } = lvl;
        public double EnemyClass { get; set; } = enemyClass;
        public long Hp { get; set; } = (long)(hp * Math.Pow(1.2, lvl) * enemyClass);
        public long Atk { get; set; } = (long)(atk * Math.Pow(1.2, lvl) * enemyClass);
        public long Xp { get; set; } = (long)(xp * Math.Pow(1.2, lvl) * enemyClass);
        public bool IsLive { get; set; } = isLive;
    }
}