using UnityEngine;

namespace CoreUtils.Collections
{
    public class FixedTileMap<T>
    {
        public int SizeX { get; }
        public int SizeY { get; }
        public T[] tiles { get; }

        public T this[Vector3Int position]
        {
            get => tiles[GetIndex(position.x, position.y)];
            set => tiles[GetIndex(position.x, position.y)] = value;
        }

        public FixedTileMap(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            tiles = new T[SizeX * SizeY];
        }

        private int GetIndex(int x, int y)
        {
            return Mathf.Clamp(y, 0, SizeY - 1) * SizeX + Mathf.Clamp(x, 0, SizeX - 1);
        }
    }
}