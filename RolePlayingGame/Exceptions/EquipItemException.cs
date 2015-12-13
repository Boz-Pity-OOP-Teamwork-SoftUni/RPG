using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayingGame.Exceptions
{
    public class EquipItemException:Exception
    {
        public EquipItemException(string message)
            :base(message)
        {
            
        }
    }
}
