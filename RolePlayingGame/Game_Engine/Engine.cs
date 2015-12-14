namespace RolePlayingGame.Game_Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RolePlayingGame.Interfaces;
    using RolePlayingGame.Models.Characters;
    using RolePlayingGame.Models.Characters.Monsters;
    using RolePlayingGame.Models.Characters.Players;
    using RandomNameGeneratorLibrary;
    using RolePlayingGame.Models.Items;

    public class Engine : IRunnable
    {
        Hero hero = new Hero("1", new Position(1, 1), "pesho", 1000, 200, 3000, 24, 1.5, 15, 1);
        List<Monster> monsters = new List<Monster>(); 
        
        public void Run()
        {
            hero.Equipment.AddSet(new Loot(1).GetBasicEquipment());

            IniztializeMonsters();

            foreach (var monster in monsters)
            {
                Duel(monster);
                
                if (!hero.IsAlive)
                {
                    break;                   
                }

                HealthPotion pot = this.hero.Inventory.InventoryItems
                    .Where(x => x is HealthPotion)
                    .Cast<HealthPotion>()
                    .FirstOrDefault();

                if (pot != null)
                {
                    pot.Consume(this.hero);
                }
            }
         
            Console.WriteLine(hero.Inventory);
        }
        
        private void IniztializeMonsters()
        {
            int baseHealth = 10;
            double baseDmg = 10;
            double baseDef = 10;
            double baseCrit = 0.5;
            double baseDodge = 0.5;
            double baseCritMultiplier = 0.3;
            int baseXp = 100;

            for (int i = 1; i < 10; i++)
            {
                var monster = new Monster(
                    "Monster " + (i + 1).ToString(),
                    new Position(i, i),
                    new PersonNameGenerator().GenerateRandomFirstAndLastName(),
                    baseHealth * i,
                    baseDef * i,
                    baseDmg * i,
                    baseCrit * i,
                    baseCritMultiplier * i,
                    baseDodge * i,
                    i,
                    baseXp * i);

                List<IWearableItem> items = new Loot(i).GetBasicEquipment();              
                monster.Equipment.AddSet(items);
                    
                this.monsters.Add(monster);                            
            }
        }

        private void Duel(Monster monster)
        {
            this.hero.levelUp +=
                (sender, eventArgs) =>
                {
                    Console.WriteLine(eventArgs.Message);
                };

            monster.characterDied += (sender, eventArgs) =>
            {
                this.hero.XpToNextLevel -= eventArgs.XP;
                this.hero.Inventory.AddItem(eventArgs.Drop);
            };
         
            while (hero.IsAlive && monster.IsAlive)
            {
                monster.Attack(hero);
                hero.Attack(monster);

                PrintCurrentFight(monster);
            }
        }

        private void PrintCurrentFight(Monster monster)
        {
            if (hero.IsAlive == false)
            {
                Console.WriteLine("Hero {0} is dead.", hero.Name);
                Console.WriteLine("Monster health = " + monster.HealthPoints);
            }
            else if (monster.IsAlive == false)
            {
                Console.WriteLine("Monster {0} is dead", monster.Name);
                Console.WriteLine("Hero health = " + hero.HealthPoints);
            }
            else
            {
                Console.WriteLine("Hero health = " + hero.HealthPoints);
                Console.WriteLine("Monster health = " + monster.HealthPoints);
            }
        }
    }
}
