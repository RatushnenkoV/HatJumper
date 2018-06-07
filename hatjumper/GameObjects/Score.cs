using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace hatjumper
{
    class Score: GameObject
    {
        string text = "";
        SpriteFont font;

        public Score(Vector2 position, Vector2 scales, GameScene scene): base(position, scales, scene, null)
        {
            this.font = scene.game.Content.Load<SpriteFont>("gameFont");
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, Color.White);
            // Понять почему не работает 
            //spriteBatch.DrawString(font, ""+lives, position, Color.White, 0, new Vector2(0, 0), 100, SpriteEffects.None, 100);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            this.text = "score: " + ((MainScene)scene).score;
        }
    }
}