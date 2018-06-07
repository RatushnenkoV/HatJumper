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

        public Button btnPause;
        public bool pause;
        public Shadow shadow;

        public int score = 0;


        public MainScene() : base()
        {
            locationController = new LocationController(game, this);
            this.bonusController = new BonusController(this);
        }

        public override void Load()
        {
            locationController.GenerateLocations(locationCount, new Vector2(0, -100));
            gameObjects.AddRange(locationController.locations);
            pause = false;

            if (startLocationIdx >= 0 && startLocationIdx < locationController.locations.Count)
            {
                var character = Character.GetInstance();
                var location = locationController.locations[startLocationIdx];
                character.Initialize(new Vector2(location.scales.X, location.scales.Y*0.15F), this, location);
                gameObjects.Add(character);
            }

            
            btnPause = new Button(new Vector2(0, screenScales.Y - 100), new Vector2(100, 100), this, game.Content.Load<Texture2D>("SoundOn"), BtnPauseAction)
            {
                spriteGetDel = BtnPauseGetSprite
            };
            Score score = new Score(new Vector2(100, screenScales.Y - 100), new Vector2(screenScales.X - 100, 100), this);
            gameObjects.Add(btnPause);
            gameObjects.Add(score);

            changingScene = new ChangingSceneCloud(this, ChangingState.changingIn);
            gameObjects.Add(changingScene);
            shadow = new Shadow(new Vector2(0, 0), screenScales, this);
            gameObjects.Add(shadow);
        }

        public void Reload()
        {
            score = 0;
            if (startLocationIdx >= 0 && startLocationIdx < locationController.locations.Count)
            {
                var character = Character.GetInstance();
                var location = locationController.locations[startLocationIdx];
                character.Initialize(new Vector2(location.scales.X, location.scales.Y * 0.15F), this, location);
                gameObjects.Add(character);
            }
            pause = false;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (!pause)
            {
                sinceLastAttack += deltaTime;
                if (sinceLastAttack >= timeBetweenAttacks)
                {
                    Attack();
                    sinceLastAttack = 0;
                }
                bonusController.Update(deltaTime);
            }
        }

        void Attack()
        {
            score++;
            locationController.Attack();
        }

        public void TeleportCharacterTo(Location location)
        {
            if (!pause)
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

        public void DeactivateLocation()
        {

            // переделать красиво
            foreach (var location in locationController.locations)
            {
                if (!location.active)
                {
                    return;
                }
            }

            Random r = new Random();
            int idx = r.Next(locationController.locations.Count);
            if (Character.GetInstance().activeLocation == locationController.locations[idx])
            {
                idx++;
                if (idx >= locationController.locations.Count)
                {
                    idx = 0;
                }
            }
            locationController.locations[idx].Deactivate();
        }

        public void ActivateLocation()
        {
            foreach (var location in locationController.locations)
            {
                location.Activate();
            }
        }

        public void DeleteDangers()
        {
            foreach (var location in locationController.locations)
            {
                location.DeleteDangers();
            }
        }

        public static void BtnPauseAction()
        {
            if (HJGame.activeGame.activeScene is MainScene)
            {
                MainScene scene = (MainScene)HJGame.activeGame.activeScene;
                if (!scene.pause)
                {
                    scene.Pause();
                } else
                {
                    scene.Play();
                }
            }
        }

        public static Texture2D BtnPauseGetSprite()
        {
            if (HJGame.activeGame.activeScene is MainScene)
            {
                MainScene scene = (MainScene)HJGame.activeGame.activeScene;
                if (scene.pause)
                {
                    return scene.game.Content.Load<Texture2D>("PlayBtn");
                }
                else
                {
                    return scene.game.Content.Load<Texture2D>("PauseBtn");
                }
            }
            return null;   
        }

        public void Pause()
        {
            pause = true;
            shadow.ShadowIn();
        }

        public void Play()
        {
            shadow.ShadowOut();
            if (Character.GetInstance().dead)
            {
                Reload();
            } else 
            {
                pause = false;
            }
        }
    }
}