using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RolePlayingGame.Characters;
namespace RolePlayingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Hero hero = new Hero("1",0,0,100,20,30,24,1.5,15);
            Monster monster = new Monster("23",1,3,150,5,30,13.5,1.3,20,100);
            
            for (int i = 0; i < 10; i++)
            {
                hero.Attack(monster);
                monster.Attack(hero);
                Console.WriteLine("Hero health = "+hero.HealthPoints);
                Console.WriteLine("Monster health = "+monster.HealthPoints);
            }
        }
    }
}
