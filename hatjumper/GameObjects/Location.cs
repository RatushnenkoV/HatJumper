using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace hatjumper
{
    class Location: GameObject
    {
        private Vector2 dangersStartPosition => GetDangersStartPosition();
        private Vector2 dangerScales => GetDangerScales();

        public Texture2D dangersSprite;

        public List<GameObject> gameObjects = new List<GameObject>();
        public GameObject platform;

        public static float platformPart = 1f/9;

        public Location(Vector2 position, Vector2 scales, GameScene scene, Texture2D bgSprite, Texture2D dangersSprite, Texture2D platformSprite): base(position, scales, scene, bgSprite)
        {
            this.dangersSprite = dangersSprite;
            this.platform = new GameObject(new Vector2(position.X, position.Y + (1 - platformPart) * scales.Y), new Vector2(scales.X, platformPart * scales.Y), scene, platformSprite);
        }

        public void Attack()
        {
            gameObjects.Add(DangersStack.Pop(dangersStartPosition, dangerScales, scene, scales.Y, dangersSprite, this));
        }

        private Vector2 GetDangerScales()
        {
            return new Vector2(scales.X / 3, scales.X / 3);
        }

        private Vector2 GetDangersStartPosition()
        {
            return new Vector2(position.X + (scales.X/2) - (dangerScales.X/2), position.Y - dangerScales.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch);
            }

            platform.Draw(spriteBatch);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            List<GameObject> gameObjectsCopy = new List<GameObject>(gameObjects);
            foreach (var go in gameObjectsCopy)
            {
                go.Update(deltaTime);
            }
        }

        public override void Tap()
        {
            base.Tap();
            if (scene is MainScene)
            {
                ((MainScene)scene).TeleportCharacterTo(this);
            }
        }
    }
}