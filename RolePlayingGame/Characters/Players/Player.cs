namespace RolePlayingGame.Characters.Players
{
    using Interfaces;

    internal class Player : Character, IAttack, IMovable
    {
        public Player(string id, int x, int y, int healthPoints, int defensePoints, int attackPoints)
            : base(id, x, y, healthPoints, defensePoints, attackPoints)
        {

        }

        public int AttackPoints { get; set; }
    }
}
