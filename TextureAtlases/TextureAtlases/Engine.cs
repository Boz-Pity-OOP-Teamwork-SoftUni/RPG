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
        private HeroSprite heroSprite;
        private Texture2D leftWalk, rightWalk, upWalk, downWalk;
        
        private FightInfo fightInfo;
        private List<MonsterSprite> monsterSprites = new List<MonsterSprite>();
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

            this.heroSprite = new HeroSprite(this.leftWalk,this.rightWalk,this.upWalk,this.downWalk, this.graphics, 300, 00, 1, 4, 150, "id");
            
            List<IWearableItem> items = new Loot(2).GetBasicEquipment();
            this.heroSprite.Equipment.AddSet(items);
            //this.monsterSprite.Add(); = new MonsterSprite(Content, this.graphics, 200, 400, 1, 1, 150);

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

            this.heroSprite.Update(gameTime);
            if (this.monsterSprites.Any(x => this.heroSprite.DestRectangle.Intersects(x.DestRectangle) && x.IsAlive))
            {
                Duel(this.monsterSprites.FirstOrDefault(x => this.heroSprite.DestRectangle.Intersects(x.DestRectangle)
                    && x.IsAlive), gameTime);
                this.isCollided = true;
            }
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {


            GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin();


            this.heroSprite.Draw(this.spriteBatch);
            foreach (var monsterSprite in this.monsterSprites)
            {
                if (monsterSprite.IsAlive)
                {
                    monsterSprite.Draw(this.spriteBatch);

                }
            }


            this.spriteBatch.DrawString(this.spriteFont, "Health Points: " + this.heroSprite.HealthPoints,
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
                var monster = new MonsterSprite(this.Content, this.graphics, i, i * 70, 1, 1, 150);
                List<IWearableItem> items = new Loot(level).GetBasicEquipment();
                monster.Equipment.AddSet(items);
                this.monsterSprites.Add(monster);
                if (i % 2 == 0)
                {
                    level++;
                }
            }
        }
        private void Duel(MonsterSprite monster, GameTime gameTime)
        {
            this.elapsed += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (this.elapsed >= this.fightDelay)
            {

                this.heroSprite.levelUp +=
                    (sender, eventArgs) =>
                    {
                        //Console.WriteLine(eventArgs.Message);
                    };

                monster.characterDied += (sender, eventArgs) =>
                {
                    this.heroSprite.XpToNextLevel -= eventArgs.XP;
                    this.heroSprite.Inventory.AddItem(eventArgs.Drop);
                };

                if (this.heroSprite.IsAlive && monster.IsAlive)
                {

                    this.isFighting = true;
                    monster.Attack(this.heroSprite);

                    this.heroSprite.Attack(monster);
                    this.fightInfo.MonsterHealth = monster.HealthPoints;
                }

                if (!monster.IsAlive)
                {


                }
                this.elapsed = 0;
            }
        }


    }
}