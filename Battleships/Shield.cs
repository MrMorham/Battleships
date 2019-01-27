using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
    class Shield
    {
        public int MaxShield;
        public int CurShield;

        public Shield(int points)
        {
            this.MaxShield = points;
            this.CurShield = MaxShield;
        }

        public void ReduceShield(int amount)
        {
            CurShield = CurShield - amount;
        }

        public void RechargeShield ()
        {
            if (CurShield > 0)
            {
                CurShield = CurShield + 10;
            }
            
            if (CurShield > MaxShield)
            {
                CurShield = CurShield - (CurShield - MaxShield);
            }
        }
    }
}
