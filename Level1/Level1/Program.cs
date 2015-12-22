namespace Level1
{
    using System;
    
#if WINDOWS || XBOX
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            using (Level1 game = new Level1())
            {
                game.Run();
            }
        }
    }
#endif
}

