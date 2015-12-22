namespace TextureAtlases.Sprites
{
    public class FightInfo
    {
        public FightInfo(int monsterHealth)
        {
            this.MonsterHealth = monsterHealth;
        }

        public FightInfo()
        {
            this.MonsterHealth = 0;
        }

        public int MonsterHealth
        {
            get;
            set;
        }
    }
}
