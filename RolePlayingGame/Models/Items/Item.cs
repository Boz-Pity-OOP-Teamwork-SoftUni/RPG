namespace RolePlayingGame.Models.Items
{
    using System;

    public abstract class Item : GameObject
    {
        private string name;

        protected Item(string id, string name)
            : base(id)
        {
            this.Name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Item name cannot be empty or null");
                }
                this.name = value;
            }
        }

        
    }
}
