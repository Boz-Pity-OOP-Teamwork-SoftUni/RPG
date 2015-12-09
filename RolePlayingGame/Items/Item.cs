using System;

namespace RolePlayingGame
{
    public abstract class Item : GameObject
    {
        private string name;

        protected Item(string id, string name) : base(id)
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
                };
            }

        }

        public override string ToString()
        {
            return String.Format("Item: {0}, Name={1}",base.ToString(),this.Name);
        }
    }
}
