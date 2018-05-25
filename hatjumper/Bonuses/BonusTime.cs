using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace hatjumper
{

    class BonusTime : Bonus
    {
        public BonusTime(Vector2 position, Vector2 scales, GameScene scene, float maxY, Location location) : base(position, scales, scene, maxY, location)
        {
            this.defaultSprite = HJGame.activeGame.Content.Load<Texture2D>("BonusTime");
        }

        public override void Hit(Character character)
        {
            if (scene is MainScene)
            {
                ((MainScene)scene).bonusController.TimeBonusAdd();
            }
            base.Hit(character);
        }
    }
}