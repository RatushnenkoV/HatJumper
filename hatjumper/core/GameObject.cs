using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace hatjumper
{
    class GameObject
    {
        public Vector2 position, scales;
        public Rectangle displayRectangle => new Rectangle((int)position.X, (int)position.Y, (int)scales.X, (int)scales.Y);
        public Texture2D sprite => GetSprite();

        public GameObject() { }

        public GameObject(Vector2 position, Vector2 scales)
        {
            this.position = position;
            this.scales = scales;
        }

        public bool contains(Vector2 vector2)
        {
            return displayRectangle.Contains(vector2);
        }

        public virtual void Tap() { }

        public virtual void Update(GameTime gameTime) { }

        public virtual Texture2D GetSprite() { return null; }
    }
}