﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RolePlayingGame.Characters;
namespace RolePlayingGame.Interfaces
{
    public interface IAttack
    {
        double AttackPoints { get; set; }
        void Attack(Character target);
    }
}
