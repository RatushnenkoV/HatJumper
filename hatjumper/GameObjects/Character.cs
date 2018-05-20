using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace hatjumper
{
    class Character: GameObject
    {
        static Character instance;
        Location activeLocation;

        public bool dead;

        public Location nextLocation;
        public bool tpIn, tpOut;

        float maxH, minH = 10;
        float ySpeed;

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
            setLocation(location);

            // переделать 
            string[] spriteNames = { "penguin", "giraffe", "pig" };
            Random r = new Random();
            int spriteNameIdx = r.Next(spriteNames.Length);
            defaultSprite = scene.game.Content.Load<Texture2D>(spriteNames[spriteNameIdx]);

            maxH = scales.Y;

            tpIn = false;
            tpOut = false;
            dead = false;
        }

        public void TeleportTo(Location location)
        {
            if (location == activeLocation)
            {
                return; 
            }

            nextLocation = location;
            tpOutStart();
        }

        public void Kill()
        {
            dead = true;
            ySpeed = -1000;
        }

        public void tpOutStart()
        {
            tpOut = true;
            tpIn = false;
        }

        public void setLocation(Location location)
        {
            float x = location.position.X + location.scales.X / 2 - scales.X / 2;
            float y = location.platform.position.Y - scales.Y;

            position = new Vector2(x, y);
            activeLocation = location;
        }

        void tpInStart()
        {
            tpIn = true;
            tpOut = false;
        }

        public override void Update(float deltaTime)
        {
            float changeHSpeed = 2500;
            if (tpIn && !dead)
            {
                position.Y -= changeHSpeed * deltaTime;
                scales.Y += changeHSpeed * deltaTime;

                if (scales.Y >= maxH)
                {
                    scales.Y = maxH;
                    position.Y = activeLocation.platform.position.Y - scales.Y;
                    tpIn = false;
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
                    setLocation(nextLocation);
                    nextLocation = null;
                    tpInStart();
                }
            }

            if (dead)
            {
                ySpeed += GlobalVars.gravity;
                position.Y += ySpeed * deltaTime;
                if (position.Y > scene.game.screenScales.Y)
                {
                    Delete();
                }
            }
        }
    }
}