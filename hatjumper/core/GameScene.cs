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

        public HJGame game => GetGame();
        public Vector2 screenScales => GetScreenScales();

        public GameScene nextScene;
        bool endingScene = false;
        public bool sceneStarted = false;


        public GameScene()
        {

        }

        public virtual void Update(float deltaTime)
        {
            List<GameObject> gameObjectsClone = new List<GameObject>(gameObjects);
            foreach (var go in gameObjectsClone)
            {
                go.Update(deltaTime);
            }

            if (endingScene && CanEndScene())
            {
                game.SetActiveScene(nextScene);
            } 

            if (!sceneStarted && CanStartScene())
            {
                sceneStarted = true;
                OnAfterStartingScene();
            }
        }

        public virtual void Delete(GameObject go)
        {
            gameObjects.Remove(go);
        }

        public virtual void OnBeforeDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public virtual void OnDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var go in gameObjects)
            {
                go.Draw(spriteBatch);
            }
        }

        public virtual void OnAfterDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            OnBeforeDraw(graphics, spriteBatch, gameTime);
            OnDraw(graphics, spriteBatch, gameTime);
            OnAfterDraw(graphics, spriteBatch, gameTime);

            spriteBatch.End();
        }

        public void Tap(Vector2 pos)
        {
            List<GameObject> gameObjectsClone = new List<GameObject>(gameObjects);
            foreach (var go in gameObjectsClone)
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

        public HJGame GetGame()
        {
            return HJGame.activeGame;
        }

        public Vector2 GetScreenScales()
        {
            return game.screenScales;
        }

        public void ChangeScene(GameScene nextScene)
        {
            OnBeforeEndingScene(nextScene);
            this.nextScene = nextScene;
            endingScene = true;
        }

        public virtual void OnBeforeEndingScene(GameScene nextScene)
        {

        }

        public virtual bool CanEndScene()
        {
            return true;
        }

        public virtual void OnAfterStartingScene()
        {

        }

        public virtual bool CanStartScene()
        {
            return true;
        }
    }

}