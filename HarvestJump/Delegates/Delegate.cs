﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HarvestJump
{
    public delegate void ScreenHandler(ScreenName input);
    public delegate void CollisionHandler(ICollide collisionObject);
}
