namespace RolePlayingGame
{
    using Models.Characters.Players;
    using Models.Characters.Monsters;
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            Hero hero = new Hero("1", 0, 0, 100, 20, 30, 24, 1.5, 15);
            Monster monster = new Monster("23", 1, 3, 150, 5, 30, 13.5, 1.3, 20, 100);
            
            while (hero.IsAlive == true && monster.IsAlive == true)
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
