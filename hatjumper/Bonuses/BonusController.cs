namespace hatjumper
{
    class BonusController
    {
        GameScene scene;
        public BonusController(GameScene scene)
        {
            this.scene = scene;
        }

        public float moneyBonusTime = 0;
        public float timeBonusTime = 0;
        public float minusOneBonusTime = 0;

        public void MoneyBonusAdd()
        {
            moneyBonusTime = 5;
            if (scene is MainScene)
            {
                ((MainScene)scene).SetAttackDel(MoneuBonusAttack);
            }
        }

        public void MoneyBonusEnd()
        {
            moneyBonusTime = 5;
            if (scene is MainScene)
            {
                ((MainScene)scene).SetAttackDel(Location.DefaultAttak);
            }
        }

        public void TimeBonusAdd()
        {
            timeBonusTime = 5;
            if (scene is MainScene)
            {
                ((MainScene)scene).SetTimeKoef(0.5f);
            }
        }

        public void TimeBonusEnd()
        {
            if (scene is MainScene)
            {
                ((MainScene)scene).SetTimeKoef(1);
            }
        }

        public void MinusOneBonusAdd()
        {
            minusOneBonusTime = 10;
            if (scene is MainScene)
            {
                ((MainScene)scene).DeactivateLocation();
            }
        }

        public void MinusOneBonusEnd()
        {
            if (scene is MainScene)
            {
                ((MainScene)scene).ActivateLocation();
            }
        }

        public void Update(float deltaTime)
        {
            if (moneyBonusTime > 0)
            {
                moneyBonusTime -= deltaTime;
                if (moneyBonusTime <= 0)
                {
                    moneyBonusTime = 0;
                    MoneyBonusEnd();
                }
            }

            if (timeBonusTime > 0)
            {
                timeBonusTime -= deltaTime;
                if (timeBonusTime <= 0)
                {
                    timeBonusTime = 0;
                    TimeBonusEnd();
                }
            }

            if (minusOneBonusTime > 0)
            {
                minusOneBonusTime -= deltaTime;
                if (minusOneBonusTime <= 0)
                {
                    minusOneBonusTime = 0;
                    MinusOneBonusEnd();
                }
            }
        }

        public static void MoneuBonusAttack(Location location)
        {
            location.gameObjects.Add(new Money(location.dangersStartPosition, location.dangerScales, location.scene, location.scene.game.screenScales.Y + 100, location));
        }
    }
}