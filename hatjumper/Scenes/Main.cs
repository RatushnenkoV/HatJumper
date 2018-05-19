using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace hatjumper
{
    class Main: GameScene
    {
        LocationController locationController;

        public int locationCount = 3;

        public Main(Game game, Vector2 screenScales) : base(game, screenScales)
        {
            locationController = new LocationController(game, this);
        }

        public override void OnBeforeDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var location in locationController.locations)
            {
                spriteBatch.Draw(location.sprite, location.displayRectangle, Color.White);
            }
        }

        public override void Load()
        {
            locationController.generateLocations(locationCount);
        }
    }
}