namespace RolePlayingGame.Models.Events
{
    public class LevelUpEventArgs
    {
        public LevelUpEventArgs(int level, int? xpToNextLevel)
        {
            Level = level;
            XpToNextLevel = xpToNextLevel;
            Message = string.Format("Congragulations you reached level {0}", this.Level);
        }

        public int Level { get; set; }

        public int? XpToNextLevel { get; set; }

        public string Message { get; set; }
    }
}
