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

        public float floorPart = 1 / 9;
        public float floorH;
        public float floorY;

        public MainScene() : base()
        {
            locationController = new LocationController(game, this);
            floorH = screenScales.Y * floorPart;
            floorY = screenScales.Y - floorH;
        }

        public override void OnBeforeDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        public override void Load()
        {
            locationController.generateLocations(locationCount);
            gameObjects.AddRange(locationController.locations);
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
            locationController.Attack();
        }

    }
}