namespace RolePlayingGame.Models.Characters.Monsters
{
    using System;
    using RolePlayingGame.Models.Events;
    using System.Collections.Generic;
    using Interfaces;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class Monster : Character
    {
        private int experience;
        private Loot loot;
        private const string defaultName = "MonsterGRRR";
        private const int defaultHealthPoints = 500;
        private const int defaultDefensePoints = 5;
        private const int defaultAttackPoints = 10;
        private const double defaultCritChance = 2.0;
        private const double defaultCritMultiplier = 1.2;
        private const double defaultDodgeChance = 10;
        private const int defaultLevel = 1;

        public Monster(string id, Position position, int totalFrames, Texture2D[] spriteAnimations)
            : base(id, position, defaultHealthPoints, defaultName, defaultDefensePoints, defaultAttackPoints,
                defaultCritChance, defaultCritMultiplier, defaultDodgeChance, defaultLevel, totalFrames, spriteAnimations)
        {
            this.Experience = experience;
            this.loot = new Loot(defaultLevel);
            this.CharacterDiedEventArgs =
                new CharacterDiedEventArgs(string.Format("{0} died", defaultName), defaultName, experience, loot.GetLoot());
        }

        public int Experience
        {
            get { return this.experience; }
            set { this.experience = value; }
        }       
        
           
    }
}
