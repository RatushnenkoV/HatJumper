using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
namespace hatjumper
{
    class Location: GameObject
    {
        private Vector2 dangersStartPosition => GetDangersStartPosition();
        private Vector2 dangerScales => GetDangerScales();

        public Texture2D bacgroundSprite, dangersSprite;

        public Location(Vector2 position, Vector2 scales, GameScene scene): base(position, scales, scene)
        {

        }

        public Dangers GetDangers()
        {
            return DangersStack.Pop(dangersStartPosition, dangerScales, scene, scales.Y, dangersSprite);
        }

        private Vector2 GetDangerScales()
        {
            return new Vector2(scales.Y / 3, scales.Y / 3);
        }

        private Vector2 GetDangersStartPosition()
        {
            return new Vector2(position.X + 1 / 2 * scales.X - 1/2 * dangerScales.X, position.Y - dangerScales.Y);
        }
    }
}