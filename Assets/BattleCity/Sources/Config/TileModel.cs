using UnityEngine;
using UnityEngine.Tilemaps;

namespace BattleCity.Config
{
    [CreateAssetMenu(fileName = "TileModel", menuName = "BattleCity/TileModel")]
    public class TileModel : ScriptableObject
    {
        public Tile[] tiles;
        public int health;
        public int armor;
        public bool immortal;
    }
}
