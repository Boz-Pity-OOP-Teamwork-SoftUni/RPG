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
        private Texture2D map1, gameOver;
        private float transparency = .75f;

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
            this.map1 = this.Content.Load<Texture2D>("Sprites\\map1");
            this.gameOver = this.Content.Load<Texture2D>("Sprites\\gameOver");
            Texture2D[] sprites = { this.rightWalk, this.leftWalk, this.downWalk, this.upWalk };
            this.hero = new Hero("1", new Position(1, 310), 4, sprites);

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
            if (this.monsters.Any(x => this.hero.Visualizer.DestRectangle.Intersects(x.DestRectangle) && x.IsAlive))
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

            this.spriteBatch.Draw(this.map1, Vector2.Zero, Color.White);
            this.hero.Visualizer.Draw(this.spriteBatch);

            foreach (var monster in this.monsters)
            {
                if (monster.IsAlive)
                {
                    monster.Visualizer.Draw(this.spriteBatch);
                }
            }

            this.spriteBatch.DrawString(this.spriteFont, "EXP to Next Level: " + this.hero.XpToNextLevel, new Vector2(70, 505), Color.White);
            this.spriteBatch.DrawString(this.spriteFont, "Hero HP: " + this.hero.HealthPoints, new Vector2(70, 555), Color.White);
            this.spriteBatch.DrawString(this.spriteFont, "Level: " + this.hero.Level, new Vector2(270, 555), Color.White);

            if (this.isCollided)
            {
                if (this.isFighting)
                {
                    this.spriteBatch.DrawString(this.spriteFont, "Monster HP: " + this.fightInfo.MonsterHealth, new Vector2(475, 550), Color.White);
                }
                this.spriteBatch.DrawString(this.spriteFont, "Monster Lvl:" + this.fightInfo.MonsterLvl, new Vector2(475, 515), Color.White);
            }
            else
            {
                this.isFighting = false;
            }
            this.isCollided = false;

            if (!this.hero.IsAlive)
            {
                this.spriteBatch.Draw(this.gameOver, Vector2.Zero, Color.White * transparency);
            }

            this.spriteBatch.End();
            base.Draw(gameTime);
        }
        private void IniztializeMonsters()
        {
            Random rand = new Random();
            for (int i = 1, level = 1; i < 13; i++)
            {
                Texture2D[] sprites = { monsterSprites };
                Monster monster = new Monster("231", new Position(rand.Next(66 * (i - 1), 66 * i), rand.Next(1, 480)), 1, sprites,i);
                List<IWearableItem> items = new Loot(level).GetBasicEquipment();
                monster.Equipment.AddSet(items);
                this.monsters.Add(monster);
                level++;
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

                monster.CharacterDied += (sender, eventArgs) =>
                {
                    this.hero.XpToNextLevel -= eventArgs.XP;
                    this.hero.Inventory.AddItem(eventArgs.Drop);
                    if(eventArgs.Drop is WearableItem)
                    {
                        var item = (IWearableItem) eventArgs.Drop;
                        this.hero.Equipment.EquipItem(item);
                    }
                   
                };

                if (this.hero.IsAlive && monster.IsAlive)
                {

                    this.isFighting = true;
                    monster.Attack(this.hero);

                    this.hero.Attack(monster);
                    this.fightInfo.MonsterHealth = monster.HealthPoints;
                    this.fightInfo.MonsterLvl = monster.Level;
                }


                this.elapsed = 0;
            }
        }


    }
}