using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace hatjumper
{
    class Character: GameObject
    {
        static Character instance;
        public Location activeLocation;

        public bool dead;

        public Location nextLocation;
        public bool tpIn, tpOut;

        float maxH, minH = 10;
        float ySpeed;

        float changeHSpeed = 2500;
        float jumpSpeed = 400;

        int lives = 1;

        private Character()
        {
            dead = true;
            
        }

        public static Character GetInstance()
        {
            if (instance == null)
            {
                instance = new Character();
            }
            return instance;
        }

        public void Initialize(Vector2 scales, GameScene scene, Location location)
        {
            this.scales = scales;
            this.scene = scene;
            SetLocation(location);

            // переделать 
            string[] spriteNames = { "penguin", "giraffe", "pig" };
            Random r = new Random();
            int spriteNameIdx = r.Next(spriteNames.Length);
            defaultSprite = scene.game.Content.Load<Texture2D>(spriteNames[spriteNameIdx]);

            maxH = scales.Y;

            tpIn = false;
            tpOut = false;
            dead = false;
            lives = 1;
        }

        public void TeleportTo(Location location)
        {
            if (location == activeLocation)
            {
                return; 
            }

            nextLocation = location;
            TpOutStart();
        }

        public void Hit()
        {
            lives--;
            if (lives <= 0)
            {
                Kill();
            } else
            {
                scene.gameObjects.Add(new LivesCounter(new Vector2(position.X + scales.X * 3/4, position.Y - scales.Y/3), scene, lives, scales.Y/3));
            }
        }

        public void Kill()
        {
            dead = true;

            Vector2 poofScales = new Vector2(scales.Y, scales.Y);
            Vector2 poofPos = new Vector2(position.X + scales.X / 2 - poofScales.X / 2, position.Y);
            Poof poof = new Poof(poofPos, poofScales, scene);
            scene.gameObjects.Add(poof);
            Delete();

        }

        public void TpOutStart()
        {
            tpOut = true;
            tpIn = false;
        }

        public void SetLocation(Location location)
        {
            float x = location.position.X + location.scales.X / 2 - scales.X / 2;
            float y = location.platform.position.Y - scales.Y;

            position = new Vector2(x, y);
            activeLocation = location;
        }

        void TpInStart()
        {
            tpIn = true;
            tpOut = false;
        }

        public override void Update(float deltaTime)
        {
            if (tpIn)
            {
                position.Y -= changeHSpeed * deltaTime;
                scales.Y += changeHSpeed * deltaTime;

                if (scales.Y >= maxH)
                {
                    scales.Y = maxH;
                    position.Y = activeLocation.platform.position.Y - scales.Y;
                    tpIn = false;

                    ySpeed = -jumpSpeed;
                    position.Y += ySpeed * deltaTime;
                }
            }

            if (tpOut && !dead)
            {
                position.Y += changeHSpeed * deltaTime;
                scales.Y -= changeHSpeed * deltaTime;

                if (scales.Y <= minH)
                {
                    scales.Y = 0;
                    position.Y = activeLocation.platform.position.Y;
                    SetLocation(nextLocation);
                    nextLocation = null;
                    TpInStart();
                }
            }

            if (dead)
            {
                if (Math.Abs(ySpeed) < 5)
                {
                    Delete();
                }
            } else {
                if (Math.Abs((position.Y + scales.Y) - activeLocation.platform.position.Y) < 5)
                {
                    if (ySpeed >= 0)
                    {
                        ySpeed = 0;
                    }
                }
            }

            position.Y += ySpeed * deltaTime;
            ySpeed += GlobalVars.gravity;
        }

        public void setLives(int lives)
        {
            this.lives = lives;
        }
    }
}