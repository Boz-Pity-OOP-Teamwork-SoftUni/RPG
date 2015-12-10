namespace SimpleRectangleCollision
{
#if WINDOWS || XBOX
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            using (CollisionGame1 game = new CollisionGame1())
            {
                game.Run();
            }
        }
    }
#endif
}

