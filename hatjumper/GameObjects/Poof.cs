using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace hatjumper
{
    class Poof: GameObject
    {
        Texture2D[] sprites;

        static int spritesCount = 5;
        static int spriteWH = 128;

        int spriteIdx = 0;
        float timePassed = 0;
        float maxTime = 0.5f;

        float timeIntervale;

        public Poof(Vector2 position, Vector2 scales, GameScene scene): base(position, scales, scene, null)
        {
            sprites = new Texture2D[spritesCount];

            Texture2D spriteSheet = scene.game.Content.Load<Texture2D>("Poof");
            Color[] data = new Color[spriteWH * spriteWH];
            for (var i = 0; i < spritesCount; i++)
            {
                Rectangle sourceRectangle = new Rectangle(0, i*spriteWH, spriteWH, spriteWH);
                spriteSheet.GetData(0, sourceRectangle, data, 0, data.Length);

                sprites[i] = new Texture2D(scene.game.GraphicsDevice, spriteWH, spriteWH);
                sprites[i].SetData(data);
            }

            timeIntervale = maxTime / spritesCount;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            timePassed += deltaTime;
            spriteIdx = (int)(timePassed / timeIntervale);

            if (timePassed >= maxTime)
            {
                Delete();
            }
        }


        public override Texture2D GetSprite()
        {
            return sprites[spriteIdx];
        }
    }
}