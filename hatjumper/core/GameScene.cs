using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace hatjumper
{
    public class GameScene
    {
        public List<GameObject> gameObjects = new List<GameObject>();

        public HJGame game => getGame();
        public Vector2 screenScales => getScreenScales();

        public GameScene()
        {

        }

        public void Update(float deltaTime)
        {
            foreach (var go in gameObjects)
            {
                go.Update(deltaTime);
            }
        }

        public virtual void Delete(GameObject go)
        {
            gameObjects.Remove(go);
        }

        public virtual void OnBeforeDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public virtual void OnAfterDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            OnBeforeDraw(graphics, spriteBatch, gameTime);

            foreach (var go in gameObjects)
            {
                spriteBatch.Draw(go.sprite, go.DisplayRectangle, Color.White); 
            }

            OnAfterDraw(graphics, spriteBatch, gameTime);

            spriteBatch.End();
        }

        public void Tap(Vector2 pos)
        {
            foreach (var go in gameObjects)
            {
                if (go.DisplayRectangle.Contains(pos))
                {
                    go.Tap();
                }
            }
        }

        public virtual void Load()
        {

        }

        public HJGame getGame()
        {
            return HJGame.activeGame;
        }

        public Vector2 getScreenScales()
        {
            return game.screenScales;
        }
    }

}