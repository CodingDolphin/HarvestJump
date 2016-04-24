using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace HarvestJump
{
    public interface ISmart
    {
        AIState aiState
        {
            get;
            set;
        }

        Direction Direction { get; set; }
        Vector2 position { get; set; }
        float chaseTreshold { get; set; }
        BoundingBox boundingBox { get; set; }
        void HandleWaypoint(Direction direction);
        void Chase(Vector2 target);
    }
}
