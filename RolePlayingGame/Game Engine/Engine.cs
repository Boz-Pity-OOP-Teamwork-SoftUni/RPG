using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RolePlayingGame.Interfaces;
using RolePlayingGame.Models.Characters;
using RolePlayingGame.Models.Characters.Monsters;
using RolePlayingGame.Models.Characters.Players;
using RandomNameGeneratorLibrary;
using RolePlayingGame.Exceptions;
using RolePlayingGame.Models.Events;
using RolePlayingGame.Models.Items;

namespace RolePlayingGame.Game_Engine
{
    public class Engine:Runnable
    {
        Hero hero = new Hero("1", new Position(1, 1), "pesho", 1000, 200, 3000, 24, 1.5, 15, 1);
        List<Monster> monsters = new List<Monster>(); 



        public void Run()
        {
           hero.Equipment.AddSet(new Loot(1).GetBasicEquipment());

            iniztializeMonsters();
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
         
            Console.WriteLine(
                this.hero.Inventory.InventoryItems);
        }


        private void iniztializeMonsters()
        {
            int baseHealth = 10;
            double baseDmg = 10;
            double baseDef = 10;
            double baseCrit = 0.5;
            double baseDodge = 0.5;
            double baseCritMultiplier = 0.3;
            int    baseXp = 100;
            for (int i = 1; i < 10; i++)
            {
                var monster = new Monster(
                  "Monster "+(i+1).ToString(),
                  new Position(i,i),
                  new PersonNameGenerator().GenerateRandomFirstAndLastName(),
                  baseHealth*i,
                  baseDef*i,
                  baseDmg*i,
                  baseCrit*i,
                  baseCritMultiplier*i,
                  baseDodge*i,
                  i,
                  baseXp*i);    
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
                hero.Attack(monster);
                monster.Attack(hero);

                Console.WriteLine("Hero health = " + hero.HealthPoints);
                Console.WriteLine("Monster health = " + monster.HealthPoints);
            }
        }

        

    }
}
