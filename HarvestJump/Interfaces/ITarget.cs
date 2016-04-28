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
    public interface ITarget
    {
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        Vector2 MidPositionX { get;}
        BoundingBox BoundingBox { get; set; }
        Direction Direction { get; set; }
        void HandleHit();
        bool GotHit { get; set; }
        double HitTimer { get; set; }
    }
}
