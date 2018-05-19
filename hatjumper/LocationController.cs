using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System;

namespace hatjumper
{
    class LocationController
    {
        Game game;
        GameScene scene;
        public Vector2 screenScales => getScreenScalers();

        List<String> types = new List<String>();
        public List<Location> locations = new List<Location>();

        public static String locationBGBaseName = "";
        public static String locationDangersBaseName = "";

        public LocationController(Game game, GameScene scene)
        {
            this.game = game;
            this.scene = scene;
            // создать список возможных локаций
            types.Add("0");
            types.Add("1");
            types.Add("2");
        }

        public Vector2 getScreenScalers()
        {
            return scene.screenScales;
        }

        public void generateLocations(int count)
        {
            Random r = new Random();
            var unusedTypes = new List<string>(types);
            for (int i = 0; i < count; i++)
            {
                int idx = r.Next(unusedTypes.Count);
                string type = unusedTypes[idx];
                unusedTypes.RemoveAt(idx);

                Location location = new Location(new Vector2(i * screenScales.X / count, 0), new Vector2(screenScales.X / count, screenScales.Y), scene);
                location.bacgroundSprite = game.Content.Load<Texture2D>(locationBGBaseName);
                location.dangersSprite = game.Content.Load<Texture2D>(locationDangersBaseName);

                locations.Add(location);
            }
        }

        public List<GameObject> getDangersList()
        {
            var res = new List<GameObject>();
            Random r = new Random();

            var used = new HashSet<int>();
            for (int i = 0; i < locations.Count-1; i++)
            {
                int idx = r.Next(locations.Count);
                if (!used.Contains(idx))
                {
                    res.Add(locations[idx].GetDangers());
                    used.Add(idx);
                }
            }

            return res;
        }
    }
}