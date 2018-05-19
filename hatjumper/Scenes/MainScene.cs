using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace hatjumper
{
    class MainScene: GameScene
    {
        LocationController locationController;

        public int locationCount = 3;

        public MainScene() : base()
        {
            locationController = new LocationController(game, this);
        }

        public override void OnBeforeDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var location in locationController.locations)
            {
                spriteBatch.Draw(location.sprite, location.DisplayRectangle, Color.White);
            }
        }

        public override void Load()
        {
            locationController.generateLocations(locationCount);
        }
    }
}