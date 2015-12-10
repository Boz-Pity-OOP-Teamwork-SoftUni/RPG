namespace RolePlayingGame.Interfaces
{
    using RolePlayingGame.Enums;

    public interface IWearableItem
    {
        WearableItemType ItemType { get; }

        int AttackPoints { get; set; }

        int DefencePoints { get; set; }

        double CritChance { get; set; }

        double CritDmg { get; set; }

        double DodgeChance { get; set; }
    }
}
