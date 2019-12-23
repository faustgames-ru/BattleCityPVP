using BattleCity.Config;
using BattleCity.Game;
using BattleCity.Game.Damage;
using UnityEngine.Tilemaps;

namespace BattleCity.Level
{
    public class LevelTileModel
    {
        public readonly int id;
        public readonly Tile tile;
        private readonly TileModel _config;

        public static readonly LevelTileModel empty = new LevelTileModel(-1, null, null);

        public LevelTileModel(int id, Tile tile, TileModel config)
        {
            this.id = id;
            this.tile = tile;
            _config = config;
        }

        public DamageReceiver CreateDamageReceiver()
        {
            if (_config == null)
                return new DamageReceiver();
            return new DamageReceiver(_config.damageReceiver);
        }
    }
}