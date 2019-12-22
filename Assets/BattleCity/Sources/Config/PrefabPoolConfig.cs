using BattleCity.Game;
using UnityEngine;

namespace BattleCity.Config
{
    [CreateAssetMenu(fileName = "PrefabPoolConfig", menuName = "BattleCity/PrefabPoolConfig")]
    public class PrefabPoolConfig: ScriptableObject
    {
        public PrefabPoolMapping[] pools =
        {
            new PrefabPoolMapping("Player", 2, PrefabsPoolMode.EzPool),
            new PrefabPoolMapping("Projectile", 50, PrefabsPoolMode.EzPool),
        };
    }
}