using System.Collections.Generic;
using BattleCity.Config;
using UnityEngine.Tilemaps;

namespace BattleCity.Level
{
    public class LevelTilePalette
    {
        private readonly List<LevelTileModel> _models = new List<LevelTileModel>();
        private readonly Dictionary<Tile, LevelTileModel> _lookupMap = new Dictionary<Tile, LevelTileModel>();
        
        public LevelTilePalette(TileMapConfig config)
        {
            foreach (var tileConfig in config.tilesModels)
            {
                foreach (var tile in tileConfig.tiles)
                {
                    if (_lookupMap.ContainsKey(tile)) continue;
                    var id = _models.Count;
                    var model = new LevelTileModel(id, tile, tileConfig);
                    _models.Add(model);
                    _lookupMap.Add(tile, model);
                }
            }
        }

        public LevelTileModel Lookup(Tile tile)
        {
            if (tile == null) return LevelTileModel.empty;
            if (!_lookupMap.ContainsKey(tile)) return LevelTileModel.empty;
            return _lookupMap[tile];
        }

        public LevelTileModel GetModel(int id)
        {
            if (id < 0 || id >= _models.Count) return LevelTileModel.empty;
            return _models[id];
        }
    }
}