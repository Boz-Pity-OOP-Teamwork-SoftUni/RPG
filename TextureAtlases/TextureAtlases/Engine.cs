using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RolePlayingGame.Models.Characters.Players;

namespace TextureAtlases
{
    using System.Globalization;
    using System.Reflection.Emit;
    using System.Threading;
    using RolePlayingGame.Interfaces;
    using RolePlayingGame.Models.Characters;
    using RolePlayingGame.Models.Characters.Monsters;
    using RolePlayingGame.Models.Items;
    using System.Threading.Tasks;
    using Sprites;

    public class Engine : Microsoft.Xna.Framework.Game
    {
        readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private Hero hero;
        private Texture2D leftWalk, rightWalk, upWalk, downWalk, monsterSprites;
        
        private FightInfo fightInfo;
        private List<Monster> monsters = new List<Monster>();
        private bool isCollided = false;
        private bool isFighting = false;
        private double elapsed = 0;
        private double fightDelay = 500;
        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            Content.RootDirectory = "Content";
           

        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            this.rightWalk = Content.Load<Texture2D>("Sprites\\rightWalk");
            this.leftWalk = Content.Load<Texture2D>("Sprites\\leftWalk");
            this.upWalk = Content.Load<Texture2D>("Sprites\\upWalk");
            this.downWalk = Content.Load<Texture2D>("Sprites\\downWalk");
            this.spriteFont = this.Content.Load<SpriteFont>("Sprites\\SpriteFont");
            this.monsterSprites = this.Content.Load<Texture2D>("Sprites\\monsterSprite");
            Texture2D[] sprites = {this.rightWalk, this.leftWalk, this.downWalk, this.upWalk};
            this.hero = new Hero("1", new Position(300, 400),4, sprites);
            
            List<IWearableItem> items = new Loot(2).GetBasicEquipment();
            this.hero.Equipment.AddSet(items);
           
            this.fightInfo = new FightInfo();
            IniztializeMonsters();
            
            this.IsMouseVisible = true;
        }

        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            this.hero.Update(gameTime);
            if (this.monsters.Any(x => this.hero.DestRectangle.Intersects(x.DestRectangle) && x.IsAlive))
            {
                Duel(this.monsters
                    .FirstOrDefault(x => this.hero.Visualizer.DestRectangle.Intersects(x.Visualizer.DestRectangle)
                    && x.IsAlive), gameTime);
                this.isCollided = true;
            }
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();
            this.hero.Visualizer.Draw(this.spriteBatch);
            foreach (var monster in this.monsters)
            {
                if (monster.IsAlive)
                {
                    monster.Visualizer.Draw(this.spriteBatch);
                }
            }
            this.spriteBatch.DrawString(this.spriteFont, "Health Points: " + this.hero.HealthPoints,
                new Vector2(400, 50), Color.Black);

            if (this.isCollided)
            {
                if (this.isFighting)
                {
                    this.spriteBatch.DrawString(this.spriteFont, "Monster HP: " + this.fightInfo.MonsterHealth,
                                new Vector2(400, 100), Color.Black);


                }
                this.spriteBatch.DrawString(this.spriteFont, "Collision!!!", Vector2.One, Color.Black);

            }
            else
            {
                this.isFighting = false;
            }
            this.isCollided = false;

            this.spriteBatch.End();

            base.Draw(gameTime);

        }
        private void IniztializeMonsters()
        {
            for (int i = 1, level = 1; i < 8; i++)
            {
                Texture2D[] sprites = {monsterSprites};
                Monster monster = new Monster("231",new Position(1,70*i),1,sprites);
                List<IWearableItem> items = new Loot(level).GetBasicEquipment();
                monster.Equipment.AddSet(items);
                this.monsters.Add(monster);
                if (i % 2 == 0)
                {
                    level++;
                }
            }
        }
        private void Duel(Monster monster, GameTime gameTime)
        {
            this.elapsed += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (this.elapsed >= this.fightDelay)
            {

                this.hero.levelUp +=
                    (sender, eventArgs) =>
                    {
                    };

                monster.characterDied += (sender, eventArgs) =>
                {
                    this.hero.XpToNextLevel -= eventArgs.XP;
                    this.hero.Inventory.AddItem(eventArgs.Drop);
                };

                if (this.hero.IsAlive && monster.IsAlive)
                {

                    this.isFighting = true;
                    monster.Attack(this.hero);

                    this.hero.Attack(monster);
                    this.fightInfo.MonsterHealth = monster.HealthPoints;
                }

             
                this.elapsed = 0;
            }
        }


    }
}