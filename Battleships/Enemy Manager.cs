using System;
using System.Collections.Generic;

namespace Battleships
{
    public class EnemyManager
    {
        public List<string> Designations = new List<string> { "ALPHA", "BRAVO", "CHARLIE", "DELTA", "ECHO", "FOXTROT", "GOLF", "HOTEL", "INDIA", "JULIETT", "KILO", "LIMA", "MIKE", "NOVEMBER", "OSCAR", "PAPA", "QUEBEC", "ROMEO", "SIERRA", "TANGO", "UNIFORM", "VICTOR", "WHISKEY", "XRAY", "YANKEE", "ZULU" };
        public List<string> Available = new List<string> { };
        public SortedDictionary<string, Enemy> Enemies = new SortedDictionary<string, Enemy>();
        public List<string> DestroyedEnemies = new List<string> { };
        public Random Rand = new Random();

        public int EnemyCount = 0;

        public void NewEnemy ()
        {
            int Percentage = Rand.Next(0, 101);

            if (Percentage > 50 && EnemyCount < 26)
            {
                Enemy BadGuy = new Enemy(GenerateHealth(), GenerateSpeed(), GenerateBearing(), GenerateDistance());
                BadGuy.Classify(BadGuy.Health);
                Enemies.Add(Designations[0], BadGuy);
                Designations.Remove(Designations[0]);
                EnemyCount++;
            }
            
            //Designations.Sort();
        }

        public int GenerateHealth ()
        {
            int health;
            health = Rand.Next(25, 201);
            return health;
        }

        public int GenerateBearing ()
        {
            int bearing;
            bearing = Rand.Next(0, 360);
            return bearing;
        }

        public int GenerateDistance ()
        {
            int distance;
            distance = Rand.Next(200, 501);
            return distance;
        }

        public int GenerateSpeed ()
        {
            int speed;
            speed = Rand.Next(5, 21);
            return speed;
        }

        public void Move ()
        {
            foreach (KeyValuePair<string, Enemy> instance in Enemies)
            {
                instance.Value.Distance = instance.Value.Distance - instance.Value.Velocity;
            }
            
        }

        public void CollectDead ()
        {
            foreach (KeyValuePair<string, Enemy> instance in Enemies)
            {
                if (instance.Value.Status == "DESTROYED")
                {
                    DestroyedEnemies.Add(instance.Key);
                }
            }
        }

        public void DestroyDead ()
        {
            foreach (string enemy in DestroyedEnemies)
            {
                DestroyEnemy(enemy);
            }
            DestroyedEnemies.Clear();
        }


        public void DestroyEnemy (string enemy)
        {
            Designations.Add(enemy);
            Enemies.Remove(enemy);
            EnemyCount--;
        }
    }

}