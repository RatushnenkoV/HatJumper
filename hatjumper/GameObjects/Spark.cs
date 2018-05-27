using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace hatjumper
{
    class Spark: GameObject
    {
        Vector2 speed;
        float dSpeed = 0.8f;
        int maxSpeed = 25;

        static Random random = new Random();

        public Spark(Vector2 position, Vector2 scales, GameScene scene)
        {
            this.position = position;
            this.scales = scales;
            this.scene = scene;
            this.speed = new Vector2(random.Next(-maxSpeed, maxSpeed), random.Next(-maxSpeed, maxSpeed));
            this.defaultSprite = scene.game.Content.Load<Texture2D>("White");
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            speed *= dSpeed;

            position += speed;

            if (speed.Length() <= 2)
            {
                Delete();
            }
        }

        public static void GenerateSparks(Vector2 position, GameScene scene)
        {
            int count = random.Next(5, 15);
            Vector2 scales = new Vector2(5, 5);

            for (int i = 0; i < count; i++)
            {
                Spark spark = new Spark(position - (scales / 2), new Vector2(10, 10), scene);
                scene.gameObjects.Add(spark);
            }
        }
    }
}