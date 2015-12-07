using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayingGame.Characters.Players
{
    public class Player1 : Player
    {
        private const string player1Id = "1";
        private const int player1X = 0;
        private const int player1Y = 0;
        private const int player1Health = 500;
        private const int player1Defense = 50;
        private const int player1Attack = 30;

        public Player1()
            : base(player1Id, player1X, player1Y, player1Health, player1Defense, player1Attack)
        {
        }
    }
}
