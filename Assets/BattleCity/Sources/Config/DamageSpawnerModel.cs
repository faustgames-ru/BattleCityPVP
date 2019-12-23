using System;

namespace BattleCity.Config
{
    [Serializable]
    public class DamageSpawnerModel
    {
        public int damage = 10;
        public float size = 0.2f;
        public float lifeTime = 0.5f;
        public bool dieOnCollision = true;
        public float dieTimeout = 0.3f;
    }
}