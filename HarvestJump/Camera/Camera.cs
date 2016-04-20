using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump
{
    public class Camera
    {
        private Viewport _viewport { get; set; }
        private Vector2 speed { get; set; }

        public Camera(Viewport viewport )
        {
            _viewport = viewport;
            input = new InputManager(0);

            Rotation = 0;
            Zoom = 1;
            Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            Position = Vector2.Zero;
        }

        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public InputManager input { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }

        public void Update (GameTime gameTime)
        {
            input.Update(gameTime);

            Position += new Vector2((float)(Math.Round(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * 5)), (float)Math.Round(-GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * 5));
        }

        public void UpdateCameraMovement()
        {
            //if (currentScreen is PlayScreen)
            //{
            //    const float cameraMoveFactor = 0.2f;

            //    // Die Position die die Kamera haben soll (=player Pos)
            //    Vector2 cameraTarget = new Vector2((int)(test.platformerWorld.player.position.X - test.screenWidth * 0.5f),
            //                                       (int)(test.platformerWorld.player.position.Y - test.screenHeight * 0.5f));

            //    // Vektor, den sich die Kamera bewegt - nicht direkt dahin wo sie letztendlich sein soll durch den cameraMoveFactor
            //    Vector2 cameraMove = cameraMoveFactor * (cameraTarget - playerCamera.Position);
            //    cameraMove = new Vector2((int)cameraMove.X, (int)cameraMove.Y);

            //    playerCamera.Position += cameraMove;
            //}
        }

        public Matrix GetViewMatrix()
        {
            return
                Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }
    }
}