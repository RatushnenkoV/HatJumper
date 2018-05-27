using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace hatjumper
{
    class BonusBomb: Bonus
    {
        public BonusBomb(Vector2 position, Vector2 scales, GameScene scene, float maxY, Location location) : base(position, scales, scene, maxY, location)
        {
            this.defaultSprite = HJGame.activeGame.Content.Load<Texture2D>("BonusBomb");
        }

        public override void Hit(Character character)
        {
            if (scene is MainScene)
            {
                ((MainScene)scene).DeleteDangers();
            }
            base.Hit(character);
        }
    }
}