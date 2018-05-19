using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace hatjumper
{
    public class Button : GameObject
    {
        public delegate void Action();
        public Action action;

        public delegate bool EnabledDel();
        public EnabledDel enabledDel;
        public bool isEnabled => (enabledDel == null) || enabledDel.Invoke();

        public Texture2D enabledSprite;
        public Texture2D disabledSprite;


        public override void Tap()
        {
            if (isEnabled && action != null)
            {
                action.Invoke();
            }
        }

        public override Texture2D GetSprite()
        {
            return isEnabled ? enabledSprite : disabledSprite;
        }

        public Button(Vector2 position, Vector2 scales, GameScene scene, Texture2D enabledSprite, Action action): base(position, scales, scene)
        {
            this.action = action;
            this.enabledSprite = enabledSprite;
            this.enabledDel = isEnabledGet;
        }

        public Button(Vector2 position, Vector2 scales, GameScene scene, Texture2D enabledSprite, Texture2D disabledSprite, Action action) : this(position, scales, scene, enabledSprite, action)
        {
            this.disabledSprite = disabledSprite;
        }

        public bool isEnabledGet()
        {
            return true;
        }
    }
}