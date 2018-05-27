using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace hatjumper
{
    class BonusMinusOne: Bonus
    {
        public BonusMinusOne(Vector2 position, Vector2 scales, GameScene scene, float maxY, Location location) : base(position, scales, scene, maxY, location)
        {
            this.defaultSprite = HJGame.activeGame.Content.Load<Texture2D>("BonusMinusOne");
        }

        public override void Hit(Character character)
        {
            if (scene is MainScene)
            {
                ((MainScene)scene).bonusController.MinusOneBonusAdd();
            }
            base.Hit(character);
        }
    }
}