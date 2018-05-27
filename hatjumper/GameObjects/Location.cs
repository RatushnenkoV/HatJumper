using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace hatjumper
{
    public delegate void AttackDel(Location location);

    public class Location: GameObject
    {
        public Vector2 dangersStartPosition => GetDangersStartPosition();
        public Vector2 dangerScales => GetDangerScales();

        public Texture2D dangersSprite;

        public List<GameObject> gameObjects = new List<GameObject>();
        public GameObject platform;

        public static float platformPart = 1f/9;

        public static Random random = new Random();

        float timeKoef = 1;

        public AttackDel attackDel;

        public bool active;

        public Shadow shadow;

        public Location(Vector2 position, Vector2 scales, GameScene scene, Texture2D bgSprite, Texture2D dangersSprite, Texture2D platformSprite): base(position, scales, scene, bgSprite)
        {
            this.dangersSprite = dangersSprite;
            this.platform = new GameObject(new Vector2(position.X, position.Y + (1 - platformPart) * scales.Y), new Vector2(scales.X, platformPart * scales.Y), scene, platformSprite);
            this.attackDel = DefaultAttak;
            this.active = true;
            this.shadow = new Shadow(position, scales, scene);
        }

        public static void DefaultAttak(Location location)
        {
            Dangers dangers;
            // исправить
            if (random.Next(100) < 10)
            {
                dangers = Bonus.GetBonus(location.dangersStartPosition, location.dangerScales, location.scene, location.scene.game.screenScales.Y + 100, location);
            }
            else
            {
                dangers = DangersStack.Pop(location.dangersStartPosition, location.dangerScales, location.scene, location.scene.game.screenScales.Y + 100, location.dangersSprite, location);
            }

            location.gameObjects.Add(dangers);
        }

        public void Attack()
        {
            attackDel.Invoke(this);
        }

        private Vector2 GetDangerScales()
        {
            return new Vector2(scales.X / 3, scales.X / 3);
        }

        private Vector2 GetDangersStartPosition()
        {
            return new Vector2(position.X + (scales.X/2) - (dangerScales.X/2), position.Y - dangerScales.Y);
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            base.OnDraw(spriteBatch);

            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch);
            }

            platform.Draw(spriteBatch);
        }

        public override void OnAfterDraw(SpriteBatch spriteBatch)
        {
            shadow?.Draw(spriteBatch);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            List<GameObject> gameObjectsCopy = new List<GameObject>(gameObjects);
            foreach (var go in gameObjectsCopy)
            {
                go.Update(deltaTime*timeKoef);
            }
            shadow.Update(deltaTime);
        }

        public override void Tap()
        {
            base.Tap();

            if (!active)
            {
                return;
            }

            if (scene is MainScene)
            {
                ((MainScene)scene).TeleportCharacterTo(this);
            }
        }

        public void SetTimeKoef(float koef)
        {
            timeKoef = koef;
        }

        public void Activate()
        {
            active = true;
            shadow.ShadowOut();
        }

        public void Deactivate()
        {
            active = false;
            shadow.ShadowIn();
        }

        public void DeleteDangers()
        {
            List<GameObject> copy = new List<GameObject>(gameObjects);
            foreach (var go in copy)
            {
                if (go.GetType() == typeof(Dangers))
                {
                    scene.gameObjects.Add(new Poof(go.position, go.scales, scene));
                    go.Delete();
                }
            }
        }
    }
}