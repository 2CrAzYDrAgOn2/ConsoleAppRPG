namespace ConsoleApp32
{
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