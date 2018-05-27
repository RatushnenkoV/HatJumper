using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace hatjumper
{
    public class Shadow: GameObject
    {
        public float transparent;
        public float maxTransparent = 0.8f;

        public float deltaTransparent;
        public float transparentTime = 0.2f;

        bool shadowIn = false, shadowOut = false;


        public Shadow(Vector2 position, Vector2 scales, GameScene scene)
        {
            this.position = position;
            this.scales = scales;
            this.scene = scene;
            this.defaultSprite = scene.game.Content.Load<Texture2D>("Black");

            this.deltaTransparent = maxTransparent / transparentTime;
            this.transparent = 0;
            
        }

        public void ShadowIn()
        {
            shadowIn = true;
            shadowOut = false;
        }

        public void ShadowOut()
        {
            shadowOut = true;
            shadowIn = false;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (shadowIn)
            {
                transparent += deltaTransparent * deltaTime;
                if (transparent >= maxTransparent)
                {
                    transparent = maxTransparent;
                    shadowIn = false;
                }
            }

            if (shadowOut)
            {
                transparent -= deltaTransparent * deltaTime;
                if (transparent <= 0)
                {
                    transparent = 0;
                    shadowOut = false;
                }
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, DisplayRectangle, Color.White * transparent);
        }
    }
}