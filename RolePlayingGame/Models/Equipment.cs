namespace RolePlayingGame.Models
{
    using System;
    using System.Collections.Generic;
    using RolePlayingGame.Enums;
    using RolePlayingGame.Interfaces;
    using RolePlayingGame.Exceptions;

    public class Equipment
    {
        private Dictionary<WearableItemType, IWearableItem> equipedItems;

        public Equipment()
        {
            this.equipedItems = new Dictionary<WearableItemType, IWearableItem>();
        }

        public Dictionary<WearableItemType, IWearableItem> EquipedItems
        {
            get
            {
                return this.equipedItems;
            }
        }

        public void EquipItem(IWearableItem item)
        {
            try
            {


                if (item == null)
                {
                    throw new ArgumentNullException("Cannot wear null item");
                }
                if (this.EquipedItems.ContainsKey(item.ItemType))
                {
                    SwapItems(item.ItemType, item);
                }
                else
                {
                    if ((item.ItemType == WearableItemType.OneHandWeapon
                         || item.ItemType == WearableItemType.Shield)
                        && !isEquipedWith2hWeapon())
                    {
                        this.equipedItems.Add(item.ItemType, item);
                    }
                    else if (item.ItemType == WearableItemType.TwoHandWeapon
                             && !isEquipedWithWeaponAndShield())
                    {
                        this.equipedItems.Add(item.ItemType, item);
                    }
                    else if (item.ItemType != WearableItemType.OneHandWeapon
                        && item.ItemType != WearableItemType.TwoHandWeapon
                        && item.ItemType != WearableItemType.Shield)
                    {
                        this.equipedItems.Add(item.ItemType, item);
                    }
                    else
                    {
                        throw new EquipItemException("Cannot equip this type of weapon when already holding other type");
                    }
                }

            }
            catch (EquipItemException)
            {


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

        public void AddSet(IList<IWearableItem> set)
        {
            while (this.equipedItems.Count <= 5
                && (!this.equipedItems.ContainsKey(WearableItemType.TwoHandWeapon)
                  || (!this.equipedItems.ContainsKey(WearableItemType.OneHandWeapon)
                        && !this.equipedItems.ContainsKey(WearableItemType.Shield))
                  ))
            {
                for (int i = 0; i < set.Count; i++)
                {
                    try
                    {
                        this.EquipItem(set[i]);
                    }
                    catch (EquipItemException)
                    {

                    }
                    finally
                    {
                        set.Remove(set[i]);
                    }
                }
            }
        }

        private bool isEquipedWith2hWeapon()
        {
            if (this.equipedItems.ContainsKey(WearableItemType.TwoHandWeapon))
            {
                return true;
            }

            return false;
        }

        private bool isEquipedWithWeaponAndShield()
        {
            if (this.equipedItems.ContainsKey(WearableItemType.OneHandWeapon)
                || this.equipedItems.ContainsKey(WearableItemType.Shield))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.EquipedItems);
        }
    }
}
