using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace HarvestJump
{
    class Atack
    {
        private Damage Damage { get; set; }
        private BoundingBox DamageArea { get; set; }
        private SoundEffect atackSound { get; set; }
        public Atack(Vector2 position, int widht, int height, int atackValue, DamageType damageType)
        {
            this.Damage = new Damage(atackValue, damageType);

            CreateDamageArea(position, widht, height);
        }

        private void CreateDamageArea(Vector2 position, int width, int height)
        {
            DamageArea = new BoundingBox(position, width, height);
        }
    }
}
