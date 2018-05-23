using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace hatjumper
{
    public enum ChangingState { changingIn, changingOut};

    class ChangingSceneCloud : GameObject
    {
        public float ySpeed;

        public ChangingSceneCloud(GameScene scene, ChangingState state)
        {
            if (state == ChangingState.changingIn)
            {
                this.position = new Vector2(0, 0);
                this.defaultSprite = scene.game.Content.Load<Texture2D>("CloudIn");
            } else
            {
                this.position = new Vector2(0, scene.screenScales.Y + 200);
                this.defaultSprite = scene.game.Content.Load<Texture2D>("CloudOut");
            }
            this.scales = new Vector2(scene.screenScales.X, scene.screenScales.Y * 1.5f);
            this.scene = scene;
            this.ySpeed = -1500;
        }

        public override void Update(float deltaTime)
        {
            position.Y += ySpeed * deltaTime;
        }
    }
}