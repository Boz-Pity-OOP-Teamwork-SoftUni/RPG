using System;

namespace SimpleRectangleCollision
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (CollisionGame1 game = new CollisionGame1())
            {
                game.Run();
            }
        }
    }
#endif
}

