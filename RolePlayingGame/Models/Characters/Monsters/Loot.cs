namespace RolePlayingGame.Models.Characters.Monsters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RolePlayingGame.Enums;
    using RolePlayingGame.Interfaces;
    using RolePlayingGame.Models.Items;

    public class Loot
    {
        private static Random rand = new Random();

        private List<Item> loot;

        public WearableItemType itemType;

        private int attackPoints = 10;

        private int defencePoints = 5;

        private double critChance = 10;

        private double dodgeChance = 0.1;

        private int healthUpdate = 10;

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

                var id = (((i + 1) * level)).ToString();
                var name = "Pesho" + i + " item";

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

                this.loot.Add(wearableItem);
            }

            Item potion = new HealthPotion(level + "HP", string.Format("Health Potion Level {0}", level),
                this.healthUpdate * level);
            this.loot.Add(potion);
        }

        public Item GetLoot()
        {
            var shuffledLoot = loot.OrderBy(x => rand.Next())
                .ToList();

            return shuffledLoot[0];
        }

        public List<IWearableItem> GetBasicEquipment()
        {
            var items = this.loot.OrderBy(x => rand.Next())
                .Where(item => item is IWearableItem)
                .Cast<IWearableItem>()
                .ToList();

            return items;
        }
    }
}
