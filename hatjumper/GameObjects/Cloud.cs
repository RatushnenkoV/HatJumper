﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace hatjumper
{
    class Cloud: GameObject
    {
        public float maxX = 0;
        public float xSpeed = 1;

        public Cloud(Vector2 position, Vector2 scales, GameScene scene, float maxX, Texture2D cloudSprite): base(position, scales, scene, cloudSprite)
        {
            this.maxX = maxX;

            Random r = new Random();
            this.xSpeed = r.Next(50, 100);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            position.X -= xSpeed * deltaTime;
            if (position.X < -scales.X)
            {
                position.X = maxX + 100;
            }
        }
    }
}