using UnityEngine;
using UnityEngine.Tilemaps;

namespace BattleCity.Config
{
    [CreateAssetMenu(fileName = "TileModel", menuName = "BattleCity/TileModel")]
    public class TileModel : ScriptableObject
    {
        public Tile[] tiles;
        public DamageReceiverModel damageReceiver = new DamageReceiverModel();
    }
}
