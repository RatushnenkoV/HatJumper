using Microsoft.Xna.Framework;
using System;

namespace hatjumper
{

    class Bonus : Dangers
    {
        public Bonus() { }

        public Bonus(Vector2 position, Vector2 scales, GameScene scene, float maxY, Location location) : base(position, scales, scene, maxY, null, location)
        {

        }

        public override void Hit(Character character)
        {
            Delete();
        }

        public override void HitFloor()
        {
            Poof poof = new Poof(position, scales, scene);
            scene.gameObjects.Add(poof);
            Delete();
        }

        public static Bonus GetBonus(Vector2 position, Vector2 scales, GameScene scene, float maxY, Location location)
        {
            return new BonusHelmet(position, scales, scene, maxY, location);
            /*
            Random r = new Random();
            int k = r.Next(4);

            switch (k)
            {
                case 0: return new BonusCoins(position, scales, scene, maxY, location);
                case 1: return new Money(position, scales, scene, maxY, location);
                case 2: return new BonusTime(position, scales, scene, maxY, location);
                case 3: return new BonusHelmet(position, scales, scene, maxY, location);
            }
            return null;
            */
        }
    }
   
}