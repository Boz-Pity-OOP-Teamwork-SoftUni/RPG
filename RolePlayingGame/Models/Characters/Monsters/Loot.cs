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

                Item wearableItem = new WearableItem(
                    id,
                   name,
                    itemType,
                    this.attackPoints*level,
                    this.defencePoints*level,
                    this.critChance*level,
                    this.dodgeChance*level);
                loot.Add(wearableItem);
            }
            Item potion = new HealthPotion(level+"HP",string.Format("Health Potion Level {0}",level)
                ,this.healthUpdate*level);
            loot.Add(potion);
        }

        public Item GetLoot()
        {
            Random rand = new Random();
            var shuffledLoot = loot.OrderBy(x => rand.Next()).ToList();
            return shuffledLoot[0];
        }


    }
}
