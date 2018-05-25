using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace hatjumper
{
    class DangersStack
    {
        public static int maxCount = 10;
        public static Stack<Dangers> dangersStack = new Stack<Dangers>();
        

        public static void Push(Dangers dangers)
        {
            if (dangersStack.Count < maxCount)
            {
                dangersStack.Push(dangers);
            }
        }

        public static Dangers Pop(Vector2 position, Vector2 scales, GameScene scene, float maxY, Texture2D dangersSprite, Location location)
        {
            Dangers dangers;

            if (dangersStack.Count > 0)
            {
                dangers = dangersStack.Pop();
                dangers.position = position;
                dangers.scales = scales;
                dangers.scene = scene;
                dangers.maxY = maxY;
                dangers.defaultSprite = dangersSprite;
                dangers.loaction = location;
                dangers.ySpeed = Dangers.minYSpeed;
                dangers.active = true;
            } else
            {
                dangers = new Dangers(position, scales, scene, maxY, dangersSprite, location);
            }
            return dangers;
        }
    }

    class Dangers: GameObject
    {
        public float maxY = 0;
        public float ySpeed;
        public static float minYSpeed = 500;

        public bool active;
        public Location loaction;

        public Dangers() { }

        public Dangers(Vector2 position, Vector2 scales, GameScene scene, float maxY, Texture2D dangersSprite, Location location): base(position, scales, scene, dangersSprite)
        {
            ySpeed = minYSpeed;
            active = true;
            this.position = position;
            this.scales = scales;
            this.maxY = maxY;
            this.loaction = location;
        }

        public Dangers(Dangers dangers) : this(dangers.position, dangers.scales, dangers.scene, dangers.maxY, dangers.defaultSprite, dangers.loaction)
        {

        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            // Пределать на глобальную пременную гравитации
            ySpeed += GlobalVars.gravity;
            position.Y += ySpeed * deltaTime;

            if (active && position.Y + scales.Y >= loaction.platform.position.Y)
            {
                HitFloor();
            }

            Character character = Character.GetInstance();
            if (active && !character.dead)
            {
                if (character.DisplayRectangle.Contains(new Vector2(position.X, position.Y+scales.Y)))
                {
                    Hit(character);
                }
            }

            if (position.Y >= loaction.platform.position.Y)
            {
                Delete();
            }
        }

        public override void Delete()
        {
            if (GetType() == typeof(Dangers))
            {
                DangersStack.Push(this);
            }
            loaction.gameObjects.Remove(this);
        }

        public virtual void Hit(Character character)
        {
            ySpeed *= -GlobalVars.kenetic;
            character.Hit();
            active = false;
        }

        public virtual void HitFloor()
        {
            ySpeed *= -GlobalVars.kenetic;
            active = false;
        }
    }
}