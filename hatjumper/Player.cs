using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace hatjumper
{
    class Player
    {
        public int money = 0 , highScore = 0;

        private Player()
        {
        }

        private static Player instance = new Player();

        public static Player getInstance()
        {
            return instance;
        }

        public void loadData()
        {
            // с сохранением и загрузкой ебанина
        }

        public void saveData()
        {

        }

        public bool checkHighScore(int score)
        {
            if (score > highScore)
            {
                highScore = score;
                return true;
            }
            return false;
        }
    }
}