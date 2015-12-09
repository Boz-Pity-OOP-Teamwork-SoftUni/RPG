using System;
using System.Collections.Generic;
using RolePlayingGame.Enums;
using RolePlayingGame.Interfaces;
using RolePlayingGame.Items;
namespace RolePlayingGame
{
    public class Uquipment
    {
        private Dictionary<WearableItemType,IWearableItem> equipedItems;

        public Uquipment(Dictionary<WearableItemType, IWearableItem> equipedItems)
        {
            this.equipedItems = equipedItems;
        }

        public Dictionary<WearableItemType, IWearableItem> EquipedItems
        {
            get { return this.equipedItems; }
        }

        public void EquipItem(IWearableItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Cannot wear null item");
            }
            if (this.EquipedItems.ContainsKey(item.ItemType))
            {
                SwapItems(item.ItemType,item);
            }
            else
            {
                this.equipedItems.Add(item.ItemType,item);
            }
        }

        public void UnequipItem(IWearableItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item cannot be null");
            }
            this.equipedItems.Remove(item.ItemType);
        }

        private void SwapItems(WearableItemType itemOut, IWearableItem itemIn)
        {
            this.equipedItems[itemOut] = itemIn;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.EquipedItems);
        }
    }
}
