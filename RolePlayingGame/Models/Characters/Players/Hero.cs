namespace RolePlayingGame.Models.Characters.Players
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.Remoting.Messaging;
    using Interfaces;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using RolePlayingGame.Models.Events;
    using IUpdateable = Interfaces.IUpdateable;

    public class Hero : Character, IUpdateable
    {
        private const int XpBase = 200;
        private int? xpToNextLvl;
        public event LevelUpEventHandler levelUp;
        private const string DefaultName = "Hero";
        private const int DefaultHealthPoints = 5000;
        private const int DefaultDefensePoints = 20;
        private const int DefaultAttackPoints = 40;
        private const double DefaultCritChance = 5.0;
        private const double DefaultCritMultiplier = 1.5;
        private const double DefaultDodgeChance = 20;
        private const int DefaultLevel = 1;
        private const int ScreenWidth = 800;
        private const int ScreenHeight = 500;
        private KeyboardState ks;
        private Texture2D currentAnim;
        public Hero(string id, Position position, int totalFrames, Texture2D[] spriteAnimations)
            : base(id, position, DefaultHealthPoints, DefaultName, DefaultDefensePoints, DefaultAttackPoints,
                  DefaultCritChance, DefaultCritMultiplier, DefaultDodgeChance, DefaultLevel, totalFrames, spriteAnimations)
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
            if (!IsAlive)
            {
                return;
            }
       
            
            this.ks = Keyboard.GetState();

            if (this.ks.IsKeyDown(Keys.Right) || this.ks.IsKeyDown(Keys.D))
            {
                if (this.Visualizer.Position.X + 2f < ScreenWidth - this.Visualizer.SpriteAnimations[0].Width / 4)
                {
                    this.Visualizer.Position = new Position(this.Visualizer.Position.X+2,this.Visualizer.Position.Y);
                }
                this.Visualizer.CurrentAnim = this.Visualizer.SpriteAnimations[0];
                this.Visualizer.Animate(gameTime);
            }
            else if (this.ks.IsKeyDown(Keys.Left) || this.ks.IsKeyDown(Keys.A))
            {
                if (this.Visualizer.Position.X - 2f > 0)
                {
                    this.Visualizer.Position = new Position(this.Visualizer.Position.X - 2, this.Visualizer.Position.Y);
                }
                this.Visualizer.CurrentAnim = this.Visualizer.SpriteAnimations[1];
                this.Visualizer.Animate(gameTime);
            }
            else if (this.ks.IsKeyDown(Keys.Down) || this.ks.IsKeyDown(Keys.S))
            {
                if (this.Visualizer.Position.Y + 2f < ScreenHeight - this.Visualizer.SpriteAnimations[0].Height)
                {
                    this.Visualizer.Position = new Position(this.Visualizer.Position.X, this.Visualizer.Position.Y + 2);
                }
                this.Visualizer.CurrentAnim = this.Visualizer.SpriteAnimations[2];
                this.Visualizer.Animate(gameTime);
            }
            else if (this.ks.IsKeyDown(Keys.Up) || this.ks.IsKeyDown(Keys.W))
            {
                if (this.Visualizer.Position.Y - 2f > 0)
                {
                    this.Visualizer.Position = new Position(this.Visualizer.Position.X, this.Visualizer.Position.Y-2);
                }
                this.Visualizer.CurrentAnim = this.Visualizer.SpriteAnimations[3];
                this.Visualizer.Animate(gameTime);
            }
            else
            {
                this.Visualizer.CurrentFrame = 0;
            }


        }

      

    }
}
