using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HarvestJump
{
    static class CollisionHelper
    {
        public static float getLowestNumber(params float[] numbers)
        {
            float lowestNumber = numbers[0];

            for (int i = 0; i < numbers.Length; i++)
            {
                if (lowestNumber > numbers[i])
                    lowestNumber = numbers[i];
            }

            return lowestNumber;
        }
    }
}
