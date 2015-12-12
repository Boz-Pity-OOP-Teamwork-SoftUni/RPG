using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RolePlayingGame.Models.Items;

namespace RolePlayingGame.Models.Events
{
    public class MonsterDiedEventArgs:CharacterDiedEventArgs
    {
        public MonsterDiedEventArgs(string characterName, int xp, Item loot)
            :base(characterName)
        {
            this.XP = xp;
            this.Loot = loot;
            
        }
        public int XP { get; set; }
        public Item Loot { get; }
    }
}
