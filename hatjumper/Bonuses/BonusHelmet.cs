using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace hatjumper
{
    class BonusHelmet: Bonus
    {
        public BonusHelmet(Vector2 position, Vector2 scales, GameScene scene, float maxY, Location location) : base(position, scales, scene, maxY, location)
        {
            this.defaultSprite = HJGame.activeGame.Content.Load<Texture2D>("BonusHemlet");
        }

        public override void Hit(Character character)
        {
            character.setLives(3);
            base.Hit(character);
        }
    }
}