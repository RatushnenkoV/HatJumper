using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace hatjumper
{
    class BonusCoins : Bonus
    {
        public BonusCoins(Vector2 position, Vector2 scales, GameScene scene, float maxY, Location location) : base(position, scales, scene, maxY, location)
        {
            this.defaultSprite = HJGame.activeGame.Content.Load<Texture2D>("BonusCoins");
        }

        public override void Hit(Character character)
        {
            if (scene is MainScene)
            {
                ((MainScene)scene).bonusController.MoneyBonusAdd();
            }
            base.Hit(character);
        }
    }
}