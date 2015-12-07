using System;

namespace RolePlayingGame
{
    public abstract class GameObject
    {
        protected GameObject(string id)
        {
            this.Id = id;
        }

        public string Id { get; private set; }
    }
}

//type int or string?