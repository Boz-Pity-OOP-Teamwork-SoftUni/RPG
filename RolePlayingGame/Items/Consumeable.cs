
using RolePlayingGame.Characters;
using RolePlayingGame.Interfaces;
namespace RolePlayingGame.Items
{
    public abstract class Consumeable:Item,IConsumeable
    {
        
        public Consumeable(string id, string name) : base(id, name)
        {
        }

        public abstract void Consume(Character target);

      
    }
}
