using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BattleCity.Config
{
    [CreateAssetMenu(fileName = "TileMapConfig", menuName = "BattleCity/TileMapConfig")]
    public class TileMapConfig : ScriptableObject
    {
        public TileModel[] tilesModels;
    }
}