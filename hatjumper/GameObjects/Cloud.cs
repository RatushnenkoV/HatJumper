
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace hatjumper
{
    class Cloud: GameObject
    {
        public int maxX = 0;
        public int xSpeed = 1;

        public Texture2D cloudSprite;

        public Cloud(Vector2 position, Vector2 scales, int maxX, Texture2D cloudSprite)
        {
            this.position = position;
            this.scales = scales;
            this.maxX = maxX;
            this.cloudSprite = cloudSprite;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            position.X -= xSpeed;
            if (position.X < -scales.X)
            {
                position.X = maxX + 100;
            }
        }

        public override Texture2D GetSprite()
        {
            return cloudSprite;
        }
    }
}