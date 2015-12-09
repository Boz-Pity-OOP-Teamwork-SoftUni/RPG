using System;

namespace RolePlayingGame
{
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

        public override string ToString()
        {
            return string.Format("Id={0}", this.Id);
        }
    }
}
