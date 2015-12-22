namespace RolePlayingGame.Models
{
    using Items;
    using System;
    using System.Collections.Generic;

    public class Inventory
    {
        private const int DefaultCapacity=12;
        private readonly IList<Item> inventoryItems;
        private int capacity;

        public Inventory(int capacity = DefaultCapacity)
        {
            this.inventoryItems = new List<Item>();
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
                }

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

        public void RemoveItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Cannot add null item");
            }

            this.inventoryItems.Remove(item);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.InventoryItems);
        }
    }
}
