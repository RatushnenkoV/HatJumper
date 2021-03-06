﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace hatjumper
{
    class Menu: GameScene
    {
        Texture2D background;

        public Menu(Game game, Vector2 screenScales) : base(game, screenScales) { }

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
            Console.WriteLine("play");
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

            // кнопка Play
            gameObjects.Add(new Button(new Vector2(0, 0), new Vector2(100, 50), game.Content.Load<Texture2D>("Play"), btnPlayClick));

            // Облака
            gameObjects.Add(new Cloud(new Vector2(100, 100), new Vector2(200, 200), (int)screenScales.Y, game.Content.Load<Texture2D>("Play")));

            
        }
    }
}