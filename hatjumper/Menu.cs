using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace hatjumper
{
    public enum Action
    {
        Play,
        Shop,
        SoundOnOff,
        VibrationOnOff
    }

    public class MainMenu
    {
        #region Declarations
        //----------------
        public List<Button> buttons = new List<Button>(4);
        Action[] actions = { Action.Play, Action.Shop, Action.SoundOnOff, Action.VibrationOnOff };

        public Button PlayBtn => buttons[0];
        public Button ShopBtn => buttons[1];
        public Button SoundBtn => buttons[2];
        public Button VibrBtn => buttons[3];
        //----------------
        #endregion

        #region Constructors
        //----------------
        /// <summary>
        /// Main menu constructor. Order of buttons: Play, Shop, Sound, Vibration.
        /// </summary>
        /// <param name="_coordinates">List of coordinates</param>
        /// <param name="_textures">List of sprites</param>
        public MainMenu(List<Vector2> _coordinates, List<Texture2D> _textures)
        {
            for (int i = 0; i <= 3; i++)
            {
                buttons.Add(new Button(_coordinates[i], actions[i], _textures[i]));
            }
        }
        //----------------
        #endregion
        
        #region Methods
        //----------------
        public void CheckTap(TouchLocation _touch, ref string _testString)
        {
            foreach (Button butt in buttons)
            {
                if (butt.displayRectangle.Contains(_touch.Position))
                {
                    butt.Action(ref _testString);
                    break;
                }
            }
        }
        //----------------
        #endregion
    }

    public class Button
    {
        #region Declarations
        //----------------
        Vector2 position;
        public Rectangle displayRectangle;
        Texture2D image;
        Action action;
        
        public Texture2D Image { get => image; set => image = value; }
        //----------------
        #endregion

        #region Constructors
        //----------------
        public Button(Vector2 _position, Action _action, Texture2D _image)
        {
            position = _position;
            displayRectangle = new Rectangle((int)_position.X, (int)_position.Y, _image.Width, _image.Height);
            action = _action;
            image = _image;
        }
        //----------------
        #endregion

        #region Methods
        //----------------
        internal void Action(ref string _testString)
        {
            switch (action)
            {
                case hatjumper.Action.Play:
                    // insert play commands here
                    _testString = "Play button";
                    break;
                case hatjumper.Action.Shop:
                    // insert shop commands here
                    _testString = "Shop button";
                    break;
                case hatjumper.Action.SoundOnOff:
                    // insert sound commands here
                    _testString = "Sound button";
                    break;
                case hatjumper.Action.VibrationOnOff:
                    // insert vibration commands here
                    _testString = "Vibration button";
                    break;
            }
        }
        //----------------
        #endregion
    }
}