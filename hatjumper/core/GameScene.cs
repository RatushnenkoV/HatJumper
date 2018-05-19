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
        public Game game;

        public Vector2 screenScales;

        public GameScene(Game game, Vector2 screenScales)
        {
            this.game = game;
            this.screenScales = screenScales;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var go in gameObjects)
            {
                go.Update(gameTime);
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
                spriteBatch.Draw(go.sprite, go.displayRectangle, Color.White); 
            }

            OnAfterDraw(graphics, spriteBatch, gameTime);

            spriteBatch.End();
        }

        public void Tap(Vector2 pos)
        {
            foreach (var go in gameObjects)
            {
                if (go.displayRectangle.Contains(pos))
                {
                    go.Tap();
                }
            }
        }

        public virtual void Load()
        {

        }
    }

}