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
    class Shop
    {
        private Shop()
        {

        }

        private static Shop instance = new Shop();

        public static Shop getInstance()
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
    }
}