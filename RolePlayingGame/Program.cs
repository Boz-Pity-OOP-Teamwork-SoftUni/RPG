using RolePlayingGame.Enums;
using RolePlayingGame.Models.Characters;
using RolePlayingGame.Models.Events;
using RolePlayingGame.Models.Items;

namespace RolePlayingGame
{
    using Models.Characters.Players;
    using Models.Characters.Monsters;
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {

            Character hero = new Hero("1", 0, 0, "pesho", 10, 200, 300, 24, 1.5, 15, 1);
            Character monster = new Monster("23", 1, 3, "gosho", 150, 50, 30, 13.5, 1.3, 20, 3, 100);
            hero.characterDied += (sender, eventArgs) =>
            {
                Console.WriteLine(eventArgs.Message);
            };
            monster.characterDied += (sender, eventArgs) =>
            {
                Console.WriteLine(eventArgs.Message);
                Console.WriteLine(eventArgs.Drop);

                Console.WriteLine(eventArgs.XP);
            };
            while (hero.IsAlive == true && monster.IsAlive)
            {
                hero.Attack(monster);
                monster.Attack(hero);

                Console.WriteLine("Hero health = " + hero.HealthPoints);
                Console.WriteLine("Monster health = " + monster.HealthPoints);
            }

            string dead = hero.IsAlive == true ? "Monster" : "Hero";

            Console.WriteLine("{0}{1} is dead", Environment.NewLine, dead);
        }
    }
}
