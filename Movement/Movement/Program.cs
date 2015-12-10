namespace WindowsGame1
{
#if WINDOWS || XBOX
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            using (MovementGame1 game = new MovementGame1())
            {
                game.Run();
            }
        }
    }
#endif
}

