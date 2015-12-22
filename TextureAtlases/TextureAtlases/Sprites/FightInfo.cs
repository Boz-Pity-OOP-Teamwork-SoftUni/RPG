namespace TextureAtlases.Sprites
{
    public class FightInfo 
    {
        public FightInfo(int monsterHealth, int mosterLvl)
        {
            this.MonsterLvl = mosterLvl;
            this.MonsterHealth = monsterHealth;
        }

        public FightInfo()
        {
            this.MonsterHealth = 0;
            this.MonsterLvl = 0;
        }

        public int MonsterHealth
        {
            get;
            set;
        }

        public int MonsterLvl { get; set; }

    }
}
