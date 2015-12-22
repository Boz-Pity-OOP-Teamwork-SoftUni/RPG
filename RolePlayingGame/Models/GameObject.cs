namespace RolePlayingGame.Models
{
    using System;

    public abstract class GameObject
    {
        private string id;

        protected GameObject(string id)
        {
            this.Id = id;
        }

        public string Id
        {
            get { return this.id; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Id cannot be null");
                }

                this.id = value;
            }
        }
    }
}
