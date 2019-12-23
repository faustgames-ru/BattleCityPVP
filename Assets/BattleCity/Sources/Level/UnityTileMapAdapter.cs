using UnityEngine;
using UnityEngine.Tilemaps;

namespace BattleCity.Level
{
    public class UnityTileMapAdapter
    {
        public Vector3Int Size { get; }

        private readonly Vector3Int _min;
        private readonly Tilemap _tilesMap;

        public Tile this[Vector3Int position]
        {
            get => _tilesMap.GetTile<Tile>(_min + position);
            set => _tilesMap.SetTile(_min + position, value);
        }

        public UnityTileMapAdapter(Tilemap tilesMap)
        {
            _tilesMap = tilesMap;
            var localBounds = tilesMap.localBounds;
            _min = tilesMap.LocalToCell(localBounds.min);
            var max = tilesMap.LocalToCell(localBounds.max);
            Size = new Vector3Int(max.x - _min.x + 1, max.y - _min.y + 1, 0);
        }

        public Vector3Int WorldToCell(Vector3 value)
        {
            var cell = _tilesMap.WorldToCell(value);
            return CelToModel(cell);
        }

        private Vector3Int CelToModel(Vector3Int position)
        {
            var result = position - _min;
            result.x = Mathf.Clamp(result.x, 0, Size.x - 1);
            result.y = Mathf.Clamp(result.y, 0, Size.y - 1);
            return result;
        }
    }
}