namespace RolePlayingGame.Interfaces
{
    using Models.Characters;

    public interface IConsumeable
    {
        void Consume(Character target);
    }
}
