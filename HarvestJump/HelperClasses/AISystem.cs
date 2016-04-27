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
    class AISystem
    {
        private List<Platform> platformList;
        private List<Waypoint> waypointList;
        private List<ITarget> aiTargetList;
        private Sprite debugRectangle;

        public AISystem(List<Platform> platformList)
        {
            this.platformList = platformList;
            this.aiTargetList = new List<ITarget>();
            this.waypointList = new List<Waypoint>();
            this.debugRectangle = new Sprite(Vector2.Zero);

            CreateWaypoints();
        }

        public void LoadContent(ContentManager content)
        {
            this.debugRectangle.LoadContent(content, "blackPixel");
        }

        public void addAITarget(ITarget iTargetObject)
        {
            this.aiTargetList.Add(iTargetObject);
        }

        //TODO Dont create new Waypoints very Frame

        public void CreateWaypoints()
        {
            int startPositionX;
            int startPositionY;
            int endPositionX;
            int endPositionY;
            int wayPointSize = 25;

            foreach (Platform item in platformList)
            {
                startPositionX = (int)item.Position.X;
                startPositionY = (int)item.Position.Y;

                endPositionX = (int)item.PlatformWidthPx + startPositionX;
                endPositionY = (int)item.Position.Y;

                waypointList.Add(new Waypoint(Direction.right, new BoundingBox(new Vector2(startPositionX - wayPointSize, startPositionY - wayPointSize), wayPointSize, wayPointSize)));
                waypointList.Add(new Waypoint(Direction.left, new BoundingBox(new Vector2(endPositionX, endPositionY - wayPointSize), wayPointSize, wayPointSize)));
            }
        }

        public void CheckWayoints(ISmart iSmartObject)
        {
            CreateWaypoints();

            foreach (Waypoint item in waypointList)
            {
                if (item.wayPointCollider.Intersects(iSmartObject.BoundingBox))
                {
                    iSmartObject.HandleWaypoint(item.direction);
                }
            }
        }

        public void AssignTargets(ISmart iSmartObject)
        {
            foreach (ITarget target in aiTargetList)
            {
                if (Vector2.Distance(iSmartObject.Position, target.Position) <= iSmartObject.seeRadius)
                    iSmartObject.AddTarget(target);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Game1.debug == true)
            {
                foreach (Waypoint item in waypointList)
                {
                    spriteBatch.Draw(debugRectangle.texture, new Rectangle((int)item.wayPointCollider.position.X, (int)item.wayPointCollider.position.Y,
                                                                           (int)item.wayPointCollider.width, (int)item.wayPointCollider.height), Color.White);
                }
            }

            waypointList.Clear();
        }
    }
}
