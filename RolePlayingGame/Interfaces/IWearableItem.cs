
using RolePlayingGame.Enums;
namespace RolePlayingGame.Interfaces
{
    public interface IWearableItem
    {
         WearableItemType ItemType { get;}
         double AttackPoints { get; set; }
         double DefencePoints { get; set; }
         double CritChance { get; set; }
         double CritDmg { get; set; }
         double DodgeChance { get; set; }
        
    }
}
