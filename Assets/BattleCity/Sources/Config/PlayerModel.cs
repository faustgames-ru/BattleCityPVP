using UnityEngine;

namespace BattleCity.Config
{
    [CreateAssetMenu(fileName = "PlayerModel", menuName = "BattleCity/PlayerModel")]
    public class PlayerModel : ScriptableObject
    {
        public float respawnCount = 5;
        public float attackTimeout = 0.5f;
        public float moveVelocity = 1f;
        public float projectileVelocity = 4f;
        public DamageSpawnerModel damageSpawner = new DamageSpawnerModel();
        public DamageReceiverModel damageReceiver = new DamageReceiverModel();
    }
}