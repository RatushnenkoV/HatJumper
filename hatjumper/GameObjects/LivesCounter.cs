using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace hatjumper
{
    class LivesCounter: GameObject
    {
        float ySpeed = -10;
        int lives;
        float fontSize;
        SpriteFont font;

        public LivesCounter(Vector2 position, GameScene scene, int lives, float fontSize)
        {
            this.position = position;
            this.scene = scene;
            this.font = scene.game.Content.Load<SpriteFont>("gameFont");
            this.fontSize = fontSize;
            this.lives = lives;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            position.Y += ySpeed;
            ySpeed *= 0.9f;

            if (Math.Abs(ySpeed) <= 0.5)
            {
                Delete();
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "" + lives, position, Color.White);
            // Понять почему не работает 
            //spriteBatch.DrawString(font, ""+lives, position, Color.White, 0, new Vector2(0, 0), 100, SpriteEffects.None, 100);
        }
    }
}