namespace RolePlayingGame.Models.Items
{
    using Characters;
    using System;

    public class HealthPotion : Consumeable
    {
        private int healthUpdate;

        public HealthPotion(string id, string name, int healthUpdate) : base(id, name)
        {
            this.HealthUpdate = healthUpdate;
        }

        public int HealthUpdate
        {
            get { return this.healthUpdate; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Health potion cannot heal for a negative amount");
                }

                this.healthUpdate = value;
            }
        }
        public override void Consume(Character target)
        {
            target.HealthPoints += this.HealthUpdate;
        }

        public override string ToString()
        {
            string result = base.ToString() + string.Format(" . Health potion: health effect:{0}",
                this.HealthUpdate);

            return result;
        }
    }
}
