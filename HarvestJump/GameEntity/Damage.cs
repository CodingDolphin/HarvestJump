using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarvestJump
{
    enum DamageType
    {
        physical,
        magic,
    }
    class Damage
    {
        private int atackValue { get; set; }
        private DamageType type { get; set; }

        public Damage(int atackValue, DamageType type)
        {
            this.atackValue = atackValue;
            this.type = type;
        }
    }
}
