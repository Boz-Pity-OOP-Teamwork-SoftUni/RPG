using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RolePlayingGame.Models.Items;
namespace RolePlayingGame.Models.Events
{
    public class CharacterDiedEventArgs
    {
        private string message = "YOU DIED!";
        public CharacterDiedEventArgs(string characterName)
        {
            CharacterName = characterName;
            this.Drop = null;
            this.XP = null;
        }

        public CharacterDiedEventArgs(string message, string characterName, int xp, Item drop)
        {
            this.message = message;
            CharacterName = characterName;
            XP = xp;
            Drop = drop;
        }

        public string CharacterName { get; set; }
        public string Message { get { return this.message; } }
        public int? XP { get; set; }
        public Item Drop { get; set; }
    }
}
