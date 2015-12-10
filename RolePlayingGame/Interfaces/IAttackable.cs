namespace RolePlayingGame.Interfaces
{
    using RolePlayingGame.Models.Characters;

    public interface IAttackable
    {
        double AttackPoints { get; set; }

        void Attack(Character target);
    }
}
