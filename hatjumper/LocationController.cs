﻿using Microsoft.Xna.Framework;
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

        public static String locationBGBaseName = "-world";
        public static String locationDangersBaseName = "-dangers";
        public static String locationPlatformBaseName = "-platform";

        public LocationController(Game game, GameScene scene)
        {
            this.game = game;
            this.scene = scene;
            // создать список возможных локаций
            types.Add("penguin");
            types.Add("pig");
            types.Add("giraffe");
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

                Location location = new Location(
                        new Vector2(i * screenScales.X / count, 0),
                        new Vector2(screenScales.X / count, screenScales.Y),
                        scene,
                        game.Content.Load<Texture2D>(type + locationBGBaseName),
                        game.Content.Load<Texture2D>(type + locationDangersBaseName),
                        game.Content.Load<Texture2D>(type + locationPlatformBaseName)
                    );

                locations.Add(location);
            }
        }

        public void Attack()
        {
            Random r = new Random();

            var used = new HashSet<int>();
            for (int i = 0; i < locations.Count-1; i++)
            {
                int idx = r.Next(locations.Count);
                if (!used.Contains(idx))
                {
                    locations[idx].Attack();
                    used.Add(idx);
                }
            }
        }
    }
}