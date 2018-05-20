using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace hatjumper
{
    class DangersStack
    {
        public static int maxCount = 10;
        public static Stack<Dangers> dangersStack = new Stack<Dangers>();

        public static void Push(Dangers dangers)
        {
            if (dangersStack.Count < maxCount)
            {
                dangersStack.Push(dangers);
            }
        }

        public static Dangers Pop(Vector2 position, Vector2 scales, GameScene scene, float maxY, Texture2D dangersSprite, Location location)
        {
            Dangers dangers;

            if (dangersStack.Count > 0)
            {
                dangers = dangersStack.Pop();
                dangers.position = position;
                dangers.scales = scales;
                dangers.scene = scene;
                dangers.maxY = maxY;
                dangers.defaultSprite = dangersSprite;
                dangers.loaction = location;
                dangers.ySpeed = Dangers.minYSpeed;
                dangers.active = true;
            } else
            {
                dangers = new Dangers(position, scales, scene, maxY, dangersSprite, location);
            }
            return dangers;
        }
    }

    class Dangers: GameObject
    {
        public float maxY = 0;
        public float ySpeed;
        public static float minYSpeed = 500;

        public bool active;
        public Location loaction;

        public Dangers(Vector2 position, Vector2 scales, GameScene scene, float maxY, Texture2D dangersSprite, Location location): base(position, scales, scene, dangersSprite)
        {
            ySpeed = minYSpeed;
            active = true;
            this.position = position;
            this.scales = scales;
            this.maxY = maxY;
            this.loaction = location;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            // Пределать на глобальную пременную гравитации
            ySpeed += 50;
            position.Y += ySpeed * deltaTime;

            if (active && position.Y + scales.Y >= loaction.platform.position.Y)
            {
                ySpeed *= -GlobalVars.kenetic;
                active = false;
            }

            if (position.Y >= loaction.platform.position.Y)
            {
                Delete();
            }
        }

        public override void Delete()
        {
            DangersStack.Push(this);
            loaction.gameObjects.Remove(this);
        }

    }
}