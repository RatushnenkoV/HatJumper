using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
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

        public static Dangers Pop(Vector2 position, Vector2 scales, GameScene scene, float maxY, Texture2D dangersSprite)
        {
            Dangers dangers;

            if (dangersStack.Count > 0)
            {
                dangers = dangersStack.Pop();
                dangers.position = position;
                dangers.scales = scales;
                dangers.scene = scene;
                dangers.maxY = maxY;
                dangers.dangersSprite = dangersSprite;
            } else
            {
                dangers = new Dangers(position, scales, scene, maxY, dangersSprite);
            }
            return dangers;
        }
    }

    class Dangers: GameObject
    {
        public float maxY = 0;
        public float ySpeed;

        bool active;

        public Texture2D dangersSprite;

        public Dangers(Vector2 position, Vector2 scales, GameScene scene, float maxY, Texture2D dangersSprite): base(position, scales, scene)
        {
            ySpeed = 500;
            active = true;
            this.position = position;
            this.scales = scales;
            this.maxY = maxY;
            this.dangersSprite = dangersSprite;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            // Пределать на глобальную пременную гравитации
            ySpeed += 50;
            position.Y += ySpeed * deltaTime;

            if (active && position.Y >= 1000)
            {
                ySpeed *= (float)(-0.2);
                active = false;
            }

            if (position.Y >= maxY + 100)
            {
                Delete();
            }
        }

        public override void Delete()
        {

        }

        public override Texture2D GetSprite()
        {
            return dangersSprite;
        }
    }
}