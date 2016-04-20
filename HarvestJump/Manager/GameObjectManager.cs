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
    public enum EnemyType
    {
        raptor,
    }

    class GameObjectManager
    {
        private List<GameObject> gameObjectList;
        private CollisionSystem collisionSystem { get; set; }
        private WayPointManager wayPointManager { get; set; }

        public GameObjectManager(List<Platform> platformList)
        {
            gameObjectList = new List<GameObject>();
            wayPointManager = new WayPointManager(platformList);
            collisionSystem = new CollisionSystem(platformList);
        }

        public void LoadContent(ContentManager content, string assetName)
        {
            foreach (GameObject item in gameObjectList)
            {
                item.LoadContent(content, assetName);
            }

            wayPointManager.LoadContent(content);
        }

        public void AddPlayer(Vector2 position)
        {
            gameObjectList.Add(new Player(position));
        }

        public void AddEnemy(EnemyType type, Vector2 position)
        {
            switch (type)
            {
                case EnemyType.raptor:
                    gameObjectList.Add(new Raptor(position));
                    break;
                default:
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            checkWayPoints();

            foreach (GameObject item in gameObjectList)
            {
                item.Update(gameTime);
                collisionSystem.checkCollision(item);         
            }
        }


        private void checkWayPoints()
        {
            foreach (GameObject item in gameObjectList)
            {
                if (item is Enemy)
                {
                    Enemy enemy = item as Enemy;
                    wayPointManager.Update(enemy);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject item in gameObjectList)
            {
                item.Draw(spriteBatch);
            }

            wayPointManager.Draw(spriteBatch);
        }
    }
}
