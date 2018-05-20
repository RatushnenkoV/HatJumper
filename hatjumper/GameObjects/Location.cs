using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
            return new Vector2(scales.X / 3, scales.X / 3);
        }

        private Vector2 GetDangersStartPosition()
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", position.X, (scales.X / 2), (dangerScales.X / 2), position.X + (scales.X / 2) - (dangerScales.X / 2));
            return new Vector2(position.X + (scales.X/2) - (dangerScales.X/2), position.Y - dangerScales.Y);
        }

        public override Texture2D GetSprite()
        {
            return bacgroundSprite;
        }
    }
}