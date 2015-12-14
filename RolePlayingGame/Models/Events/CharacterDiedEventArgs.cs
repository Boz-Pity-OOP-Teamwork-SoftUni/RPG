namespace RolePlayingGame.Models.Events
{
    using RolePlayingGame.Models.Items;

    public class CharacterDiedEventArgs
    {
        private string message = "YOU DIED!";

        public CharacterDiedEventArgs(string message, string characterName, int? xp, Item drop)
        {
            this.message = message;
            CharacterName = characterName;
            XP = xp;
            Drop = drop;
        }

        public CharacterDiedEventArgs(string characterName)
            :this(characterName,null,null,null)
        {
            CharacterName = characterName;
            
        }

        public string CharacterName { get; set; }

        public string Message { get { return this.message; } }

        public int? XP { get; set; }

        public Item Drop { get; set; }
    }
}
