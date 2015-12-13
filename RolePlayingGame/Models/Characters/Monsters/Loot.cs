using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RolePlayingGame.Enums;
using RolePlayingGame.Interfaces;
using RolePlayingGame.Models.Items;
using RandomNameGeneratorLibrary;
namespace RolePlayingGame.Models.Characters.Monsters
{
    public class Loot
    {
        private List<Item> loot;
        public WearableItemType itemType;
        private int attackPoints=10;
        private int defencePoints=5;
        private double critChance=10;
        private double dodgeChance=0.1;
        private int healthUpdate=10;
        public Loot(int level)
        {
            this.loot = new List<Item>(12);
            Init(level);
        }


        private void Init(int level)
        {
            for (int i = 0; i < Enum.GetValues(typeof(WearableItemType)).Length; i++)
            {
                itemType = (WearableItemType)Enum.ToObject(typeof(WearableItemType), i);
                
                var id = (((i+1)*level)).ToString();
                var name = new PersonNameGenerator().GenerateRandomMaleFirstName() +
                           new PlaceNameGenerator().GenerateRandomPlaceName();

                Item wearableItem = null;
                if (itemType == WearableItemType.Shield)
                {
                     wearableItem = new WearableItem(
                  id,
                 name,
                  itemType,
                  0,
                  this.defencePoints * level + i,
                  0,
                  0);
                }
                else
                {
                     wearableItem = new WearableItem(
                  id,
                 name,
                  itemType,
                  this.attackPoints * level + i,
                  this.defencePoints * level + i,
                  this.critChance * level + i,
                  this.dodgeChance * level + i);
                }   
              
                
                loot.Add(wearableItem);
            }
            Item potion = new HealthPotion(level+"HP",string.Format("Health Potion Level {0}",level)
                ,this.healthUpdate*level);
            loot.Add(potion);
        }

        public Item GetLoot()
        {
            Random rand = new Random();
            var shuffledLoot = loot.OrderBy(x => rand.Next())
                .ToList();
            return shuffledLoot[0];
        }

        public List<IWearableItem> GetBasicEquipment()
        {
            Random rand = new Random();
            var items = this.loot.OrderBy(x=>rand.Next())
                .Where(item=>item is IWearableItem)
                .Cast<IWearableItem>()
                .ToList();
            return items;
        } 

    }
}
