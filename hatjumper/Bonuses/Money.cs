using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace hatjumper
{

    class Money : Bonus
    {
        public Money(Vector2 position, Vector2 scales, GameScene scene, float maxY, Location location) : base(position, scales, scene, maxY, location)
        {
            this.defaultSprite = HJGame.activeGame.Content.Load<Texture2D>("Coin");
        }

        public override void Hit(Character character)
        {
            Player.GetInstance().money++;
            base.Hit(character);
        }
    }
}