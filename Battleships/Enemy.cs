namespace Battleships
{
    public class Enemy
    {
        public int Health {get; set;}
        public int Velocity {get; set;}
        public int Bearing {get; set;}
        public int Distance { get; set; }
        public string Classification = "FRIGATE";
        public string Status = "OPERATIONAL";
        
        public Enemy(int health,int velocity,int bearing, int distance)
        {
            this.Health = health;
            this.Velocity = velocity;
            this.Bearing = bearing;
            this.Distance = distance;
        }
            
        public void ChangeHealth(int amount)
        {
            Health = Health + -amount;

            if (Health < 100 && Health >= 25)
            {
                Status = "DAMAGED";
            }
            if (Health < 25 && Health > 0)
            {
                Status = "CRITICAL";
            }
            if (Health <= 0)
            {
                Status = "DESTROYED";
            }
        }

        public void Classify(int health)
        {
            if(Health > 0 && Health <= 50)
            {
                Classification = "FRIGATE";
            }
            if(Health > 50 && Health <= 100)
            {
                Classification = "DESTROYER";
            }
            if(Health > 100 && Health <= 150)
            {
                Classification = "CRUISER";
            }
            if(Health > 150 && Health <= 200)
            {
                Classification = "BATTLESHIP";
            }
        }

        
    }
}