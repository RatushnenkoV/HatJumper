using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace hatjumper
{
    public class GameObject
    {
        public Vector2 position, scales;
        public Rectangle DisplayRectangle => new Rectangle((int)position.X, (int)position.Y, (int)scales.X, (int)scales.Y);
        public Texture2D sprite => GetSprite();

        public GameScene scene;

        public GameObject() { }

        public GameObject(Vector2 position, Vector2 scales, GameScene scene)
        {
            this.position = position;
            this.scales = scales;
            this.scene = scene;
        }

        public bool Contains(Vector2 vector2)
        {
            return DisplayRectangle.Contains(vector2);
        }

        public virtual void Delete()
        {
            scene?.Delete(this);
        }

        public virtual void Tap() { }

        public virtual void Update(float deltaTime) { }

        public virtual Texture2D GetSprite() { return null; }
    }
}