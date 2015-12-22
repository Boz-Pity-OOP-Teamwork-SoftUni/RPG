namespace RolePlayingGame.Models.Characters.Monsters
{
    using RolePlayingGame.Models.Events;
    using Microsoft.Xna.Framework.Graphics;

    public class Monster : Character
    {
        private  int experience = 5;
        private readonly Loot loot;
        private const string DefaultName = "MonsterGRRR";
        private const int DefaultHealthPoints = 750;
        private const int DefaultDefensePoints = 10;
        private const int DefaultAttackPoints = 10;
        private const double DefaultCritChance = 2.0;
        private const double DefaultCritMultiplier = 1.2;
        private const double DefaultDodgeChance = 10;
        private const int DefaultLevel = 1;

        public Monster(string id, Position position, int totalFrames, Texture2D[] spriteAnimations,int level)
            : base(id, position, DefaultHealthPoints, DefaultName, DefaultDefensePoints, DefaultAttackPoints,
                DefaultCritChance, DefaultCritMultiplier, DefaultDodgeChance, DefaultLevel, totalFrames, spriteAnimations)
        {
            this.Experience = experience*level;
            this.loot = new Loot(DefaultLevel);
            this.Level = level;
            this.CharacterDiedEventArgs =
                new CharacterDiedEventArgs(string.Format("{0} died", DefaultName), DefaultName, experience, loot.GetLoot());
        }

        public int Experience
        {
            get { return this.experience; }
            set { this.experience = value; }
        }       
        
           
    }
}
