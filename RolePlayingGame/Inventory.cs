using System;
using System.Collections.Generic;

namespace RolePlayingGame
{
    public class Inventory
    {
        private const int Default_Capacity=12;
        private IList<Item> inventoryItems;
        private int capacity;

        public Inventory(IList<Item> inventoryItems, int capacity=Default_Capacity)
        {
            this.inventoryItems = inventoryItems;
            this.capacity = capacity;
        }

        public IList<Item> InventoryItems
        {
            get { return this.inventoryItems; }          
        }

        public int Capacity
        {
            get { return this.capacity; }
            set
            {
                if (value < 5)
                {
                    throw new ArgumentOutOfRangeException("Inventory capacity cannot be less than 5");
                };
                this.capacity = value;
            }
        }

        public void AddItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Cannot add null item");
            }
            this.inventoryItems.Add(item);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.InventoryItems);
        }
    }
}
