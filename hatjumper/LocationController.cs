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
        public Vector2 screenScales => GetScreenScalers();

        List<String> types = new List<String>();
        public List<Location> locations = new List<Location>();

        public static String locationBGBaseName = "-world";
        public static String locationDangersBaseName = "-dangers";
        public static String locationPlatformBaseName = "-platform";

        public int activeCount => GetActiveLocationCount();

        public LocationController(Game game, GameScene scene)
        {
            this.game = game;
            this.scene = scene;
            // создать список возможных локаций
            types.Add("penguin");
            types.Add("pig");
            types.Add("giraffe");
        }

        public Vector2 GetScreenScalers()
        {
            return scene.screenScales;
        }

        public void GenerateLocations(int count, Vector2 position)
        {
            Random r = new Random();
            var unusedTypes = new List<string>(types);
            locations.Clear();
            for (int i = 0; i < count; i++)
            {
                int idx = r.Next(unusedTypes.Count);
                string type = unusedTypes[idx];
                unusedTypes.RemoveAt(idx);

                Location location = new Location(
                        new Vector2(position.X + i * screenScales.X / count, position.Y),
                        new Vector2(screenScales.X / count, screenScales.Y),
                        scene,
                        game.Content.Load<Texture2D>(type + locationBGBaseName),
                        game.Content.Load<Texture2D>(type + locationDangersBaseName),
                        game.Content.Load<Texture2D>(type + locationPlatformBaseName)
                    );

                locations.Add(location);
            }
        }

        public void Attack(Dangers dangers = null)
        {
            Random r = new Random();
            //С активными возможно че-то надо придумать
            var used = new HashSet<int>();
            for (int i = 0; i < activeCount-1; i++)
            {
                int idx = r.Next(locations.Count);
                if (!locations[idx].active)
                {
                    idx++;
                    if (idx >= locations.Count)
                    {
                        idx = 0;
                    }
                }
                if (!used.Contains(idx))
                {
                    locations[idx].Attack();
                    used.Add(idx);
                }
            }
        }

        public int GetActiveLocationCount()
        {
            // Переделать на краисво

            int activeCount = 0;
            foreach (var location in locations)
            {
                if (location.active)
                {
                    activeCount++;
                }
            }

            return activeCount;
        }
    }
}