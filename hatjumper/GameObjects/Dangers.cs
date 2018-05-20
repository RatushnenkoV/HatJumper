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

        public Texture2D dangersSprite;

        public Dangers(Vector2 position, Vector2 scales, GameScene scene, float maxY, Texture2D dangersSprite): base(position, scales, scene)
        {
            this.position = position;
            this.scales = scales;
            this.maxY = maxY;
            this.dangersSprite = dangersSprite;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            ySpeed += 1;
            position.Y += ySpeed * deltaTime;

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