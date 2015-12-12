using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RolePlayingGame.Models.Items;
namespace RolePlayingGame.Models.Events
{
    public class HeroDiedEventArgs:CharacterDiedEventArgs
    {
        private  string message = "YOU DIED!";
        public HeroDiedEventArgs(string characterName)
            :base(characterName)
        {
        
        }
       
    }
}
