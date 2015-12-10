namespace RolePlayingGame.Models.Items
{
    using RolePlayingGame.Interfaces;
    using Characters;

    public abstract class Consumeable : Item, IConsumeable
    {
        public Consumeable(string id, string name)
            : base(id, name)
        {
        }

        public abstract void Consume(Character target);
    }
}
