using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
    class Player
    {
        public int Health = 1000;

        public Turret AlphaTurret = new Turret(30, 45, 50);
        public Turret BravoTurret = new Turret(50, 100, 20);

        public Shield PlayerShield = new Shield(500);
    }
}
