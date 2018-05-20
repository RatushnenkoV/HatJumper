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

        public int startLocationIdx = 1;

        public MainScene() : base()
        {
            locationController = new LocationController(game, this);
        }

        public override void OnBeforeDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        public override void Load()
        {
            locationController.generateLocations(locationCount);
            gameObjects.AddRange(locationController.locations);

            if (startLocationIdx >= 0 && startLocationIdx < locationController.locations.Count)
            {
                var character = Character.GetInstance();
                var location = locationController.locations[startLocationIdx];
                character.Initialize(new Vector2(location.scales.X, location.scales.Y*0.15F), this, location);
                gameObjects.Add(character);
            }
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

        public void TeleportCharacterTo(Location location)
        {
            Character character = Character.GetInstance();
            if (character.dead)
            {
                character.Initialize(new Vector2(location.scales.X, location.scales.Y * 0.15F), this, location);
                gameObjects.Add(character);
            }
            Character.GetInstance().TeleportTo(location);
        }

    }
}