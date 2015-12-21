namespace RolePlayingGame.Models.Characters.Players
{
    using System.Collections.Generic;
    using System;
    using System.Runtime.Remoting.Messaging;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using RolePlayingGame.Models.Events;
    using IUpdateable = Interfaces.IUpdateable;

    public class Hero : Character, IUpdateable
    {
        private const int XpBase = 100;
        private int? xpToNextLvl;
        public event LevelUpEventHandler levelUp;
        private const string defaultName = "Hero";
        private const int defaultHealthPoints = 5000;
        private const int defaultDefensePoints = 20;
        private const int defaultAttackPoints = 40;
        private const double defaultCritChance = 5.0;
        private const double defaultCritMultiplier = 1.5;
        private const double defaultDodgeChance = 20;
        private const int defaultLevel = 1;
        private const int ScreenWidth = 800;
        private const int ScreenHight = 600;
        private KeyboardState ks;
        private Texture2D currentAnim;
        public Hero(string id, Position position, int totalFrames, Texture2D[] spriteAnimations)
            : base(id, position, defaultHealthPoints, defaultName, defaultDefensePoints, defaultAttackPoints,
                  defaultCritChance, defaultCritMultiplier, defaultDodgeChance, defaultLevel, totalFrames, spriteAnimations)
        {
            this.XpToNextLevel = this.Level * XpBase;
            this.currentAnim = spriteAnimations[0];
        }

        public int? XpToNextLevel
        {
            get
            {
                return this.xpToNextLvl;
            }
            set
            {
                this.xpToNextLvl = value;

                if (this.xpToNextLvl <= 0)
                {
                    this.OnLevelUp();
                }
            }
        }




        protected virtual void OnLevelUp()
        {
            if (this.levelUp != null)
            {
                this.Level++;
                this.XpToNextLevel = this.Level * XpBase + (XpBase * 2);
                UpdateStats();
                LevelUpEventArgs args = new LevelUpEventArgs(this.Level, this.XpToNextLevel);
            }
        }

        private void UpdateStats()
        {
            this.AttackPoints += Math.Pow(this.Level, 4);
            this.DefensePoints += Math.Pow(this.Level, 4);
            this.CriticalChance += Math.Pow(this.Level, 4);
            this.DodgeChance += Math.Pow(this.Level, 4);
            this.CriticalMultiplier += Math.Pow(this.Level, 4) / 10;
        }

        public void Update(GameTime gameTime)
        {
            this.ks = Keyboard.GetState();

            if (this.ks.IsKeyDown(Keys.Right) || this.ks.IsKeyDown(Keys.D))
            {
                if (this.Visualizer.Position.X + 2f < ScreenWidth - this.Visualizer.SpriteAnimations[0].Width / 4)
                {
                    this.Visualizer.Position = new Position(this.Visualizer.Position.X+2,this.Visualizer.Position.Y);
                }
                this.currentAnim = this.Visualizer.SpriteAnimations[0];
                this.Visualizer.Animate(gameTime);
            }
            else if (this.ks.IsKeyDown(Keys.Left) || this.ks.IsKeyDown(Keys.A))
            {
                if (this.Visualizer.Position.X - 2f > 0)
                {
                    this.Visualizer.Position = new Position(this.Visualizer.Position.X - 2, this.Visualizer.Position.Y);
                }
                this.currentAnim = this.Visualizer.SpriteAnimations[1];
                this.Visualizer.Animate(gameTime);
            }
            else if (this.ks.IsKeyDown(Keys.Up) || this.ks.IsKeyDown(Keys.W))
            {
                if (this.Visualizer.Position.Y - 2f > 0)
                {
                    this.Visualizer.Position = new Position(this.Visualizer.Position.X, this.Visualizer.Position.Y-2);
                }
                this.currentAnim = this.Visualizer.SpriteAnimations[3];
                this.Visualizer.Animate(gameTime);
            }
            else if (this.ks.IsKeyDown(Keys.Down) || this.ks.IsKeyDown(Keys.S))
            {
                if (this.Visualizer.Position.Y + 2f < ScreenHight - this.Visualizer.SpriteAnimations[0].Height)
                {
                    this.Visualizer.Position = new Position(this.Visualizer.Position.X, this.Visualizer.Position.Y + 2);
                }
                this.currentAnim = this.Visualizer.SpriteAnimations[2];
                this.Visualizer.Animate(gameTime);
            }
            else
            {
                this.Visualizer.CurrentFrame = 0;
            }


        }




    }
}
