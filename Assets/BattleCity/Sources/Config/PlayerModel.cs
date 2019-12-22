using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace BattleCity.Config
{
    [CreateAssetMenu(fileName = "PlayerModel", menuName = "BattleCity/PlayerModel")]
    public class PlayerModel : ScriptableObject
    {
        public int health = 100;
        public int armor = 0;
        public float attackTimeout = 0.5f;
        public float moveVelocity = 1f;
        public int attackDamage = 10;
        public float projectileVelocity = 4f;
    }
}