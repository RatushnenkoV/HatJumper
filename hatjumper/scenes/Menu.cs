using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace hatjumper
{

    class Menu: GameScene
    {
        Texture2D background;

        ChangingSceneCloud changingSceneCloud;

        public Menu() : base() { }

        public override void OnBeforeDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.OnBeforeDraw(graphics, spriteBatch, gameTime);
            
            if (background != null)
            {
                spriteBatch.Draw(background, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
            }
        }

        public static void btnPlayClick()
        {
            HJGame.activeGame?.ChangeScene(new MainScene());
        }

        public static void btnShopClick()
        {

        }

        public static void btnSoundClick()
        {

        }

        public static void btnVibroClick()
        {

        }

        public override void Load()
        {
            background = game.Content.Load<Texture2D>("Background");

            // Облака
            gameObjects.Add(new Cloud(new Vector2(100, screenScales.Y*1/4), new Vector2(340, 165), this, (int)screenScales.Y, game.Content.Load<Texture2D>("Cloud1")));
            gameObjects.Add(new Cloud(new Vector2(300, screenScales.Y*2/4), new Vector2(352, 173), this, (int)screenScales.Y, game.Content.Load<Texture2D>("Cloud2")));
            gameObjects.Add(new Cloud(new Vector2(500, screenScales.Y*3/4), new Vector2(358, 165), this, (int)screenScales.Y, game.Content.Load<Texture2D>("Cloud3")));

            // кнопка Play
            gameObjects.Add(new Button(new Vector2(screenScales.X / 2, screenScales.Y / 2), new Vector2(200, 100), this, game.Content.Load<Texture2D>("Play"), btnPlayClick));
        }

        public override void OnBeforeEndingScene(GameScene nextScene)
        {
            base.OnBeforeEndingScene(nextScene);

            changingSceneCloud = new ChangingSceneCloud(this, ChangingState.changingOut);
            gameObjects.Add(changingSceneCloud);
        }

        public override bool CanEndScene()
        {
            if (changingSceneCloud != null)
            {
                Console.WriteLine("Позиция облака = {0}, конец облака = {1}", changingSceneCloud.position.Y, changingSceneCloud.position.Y + changingSceneCloud.scales.Y);
                return changingSceneCloud.position.Y + changingSceneCloud.scales.Y <= screenScales.Y;
            }

            return false;
        }
    }
}