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
    interface ITarget
    {
        Vector2 position { get; set; }
        Vector2 velocity { get; set; }
        BoundingBox boundingBox { get; set; }
        Direction Direction { get; set; }
    }
}
