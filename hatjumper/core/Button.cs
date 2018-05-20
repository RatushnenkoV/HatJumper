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
        public bool IsEnabled => (enabledDel == null) || enabledDel.Invoke();
        
        public Texture2D disabledSprite;


        public override void Tap()
        {
            if (IsEnabled && action != null)
            {
                action.Invoke();
            }
        }

        public override Texture2D GetSprite()
        {
            return IsEnabled ? defaultSprite : disabledSprite;
        }

        public Button(Vector2 position, Vector2 scales, GameScene scene, Texture2D enabledSprite, Action action): base(position, scales, scene, enabledSprite)
        {
            this.action = action;
            this.enabledDel = IsEnabledGet;
        }

        public Button(Vector2 position, Vector2 scales, GameScene scene, Texture2D enabledSprite, Texture2D disabledSprite, Action action) : this(position, scales, scene, enabledSprite, action)
        {
            this.disabledSprite = disabledSprite;
        }

        public bool IsEnabledGet()
        {
            return true;
        }
    }
}