using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HarvestJump
{
    class WayPointManager
    {
        private List<Platform> platformList;
        private List<Waypoint> waypointList;
        private Sprite debugRectangle;

        public WayPointManager(List<Platform> platformList)
        {
            this.platformList = platformList;
            this.waypointList = new List<Waypoint>();
            this.debugRectangle = new Sprite(Vector2.Zero);

            CreateWaypoints();
        } 

        public void LoadContent(ContentManager content)
        {
            debugRectangle.LoadContent(content, "blackPixel");
        }

        public void CreateWaypoints()
        {
            int startPositionX;
            int startPositionY;
            int endPositionX;
            int endPositionY;
            int wayPointSize = 25;

            foreach (Platform item in platformList)
            {
                startPositionX = (int)item.position.X;
                startPositionY = (int)item.position.Y;

                endPositionX = (int)item.PlatformWidthPx + startPositionX;
                endPositionY = (int)item.position.Y;

                waypointList.Add(new Waypoint(Direction.right, new BoundingBox(new Vector2(startPositionX - wayPointSize, startPositionY - wayPointSize), wayPointSize, wayPointSize)));
                waypointList.Add(new Waypoint(Direction.left, new BoundingBox(new Vector2(endPositionX, endPositionY - wayPointSize), wayPointSize, wayPointSize)));
            }
        }

        public void Update(ISmart iSmartObject)
        {
            CreateWaypoints();

            foreach (Waypoint item in waypointList)
            {
                if(item.wayPointCollider.Intersects(iSmartObject.boundingBox))
                {
                    iSmartObject.HandleWaypoint(item.direction);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Waypoint item in waypointList)
            {
                spriteBatch.Draw(debugRectangle.texture, new Rectangle((int)item.wayPointCollider.position.X, (int)item.wayPointCollider.position.Y,
                                                                       (int)item.wayPointCollider.width, (int)item.wayPointCollider.height), Color.White);
            }

            waypointList.Clear();
        }
    }
}
