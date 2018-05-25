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

        public ChangingSceneCloud changingScene;

        public BonusController bonusController;

        public MainScene() : base()
        {
            locationController = new LocationController(game, this);
            this.bonusController = new BonusController(this);
        }

        public override void OnBeforeDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        public override void Load()
        {
            locationController.GenerateLocations(locationCount);
            gameObjects.AddRange(locationController.locations);

            if (startLocationIdx >= 0 && startLocationIdx < locationController.locations.Count)
            {
                var character = Character.GetInstance();
                var location = locationController.locations[startLocationIdx];
                character.Initialize(new Vector2(location.scales.X, location.scales.Y*0.15F), this, location);
                gameObjects.Add(character);
            }

            changingScene = new ChangingSceneCloud(this, ChangingState.changingIn);
            gameObjects.Add(changingScene);
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
            bonusController.Update(deltaTime);
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

        public override bool CanStartScene()
        {
            return (changingScene != null) && (changingScene.position.Y + changingScene.scales.Y <= 0);
        }

        public override void OnAfterStartingScene()
        {
            base.OnAfterStartingScene();
            changingScene = null;
        }

        public void SetTimeKoef(float koef)
        {
            foreach (var location in locationController.locations)
            {
                location.SetTimeKoef(koef);
            }
        }

        public void SetAttackDel(AttackDel attackDel)
        {
            foreach (var location in locationController.locations)
            {
                location.attackDel = attackDel;
            }
        }
    }
}