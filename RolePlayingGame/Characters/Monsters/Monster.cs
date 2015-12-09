namespace RolePlayingGame.Characters.Monsters
{
    using Interfaces;

    internal class Monster : Character, IAttack
    {
        public Monster(string id, int x, int y, int healthPoints, int defensePoints, int attackPoints)
            : base(id, x, y, healthPoints, defensePoints, attackPoints)
        {
        }
    }
}
