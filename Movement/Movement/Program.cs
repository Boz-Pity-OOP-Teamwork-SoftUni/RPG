using System;

namespace WindowsGame1
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MovementGame1 game = new MovementGame1())
            {
                game.Run();
            }
        }
    }
#endif
}

