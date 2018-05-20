using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace hatjumper
{
    class MainScene: GameScene
    {
        LocationController locationController;

        public int locationCount = 3;

        public float sinceLastAttack = 0;
        public float timeBetweenAttacks = 1;

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

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            sinceLastAttack += deltaTime;
            if (sinceLastAttack >= timeBetweenAttacks)
            {
                Attack();
                sinceLastAttack = 0;
            }
        }

        void Attack()
        {
            gameObjects.AddRange(locationController.getDangersList());
        }
    }
}