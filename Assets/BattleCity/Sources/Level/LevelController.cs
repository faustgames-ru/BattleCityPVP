using System.Collections.Generic;
using BattleCity.Config;
using CoreUtils.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BattleCity.Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private Tilemap tilesView;

        [SerializeField]
        private TileMapConfig config;

        private LevelTilePalette _palette;
        private FixedTileMap<LevelTile> _tiles;
        private UnityTileMapAdapter _viewAdapter;
        private readonly List<LevelTile> _dirtyList = new List<LevelTile>();

        private void Awake()
        {
            _viewAdapter = new UnityTileMapAdapter(tilesView);
            _tiles = new FixedTileMap<LevelTile>(_viewAdapter.Size.x, _viewAdapter.Size.y);
            _palette = new LevelTilePalette(config);

            var i = Vector3Int.zero;
            for (i.y = 0; i.y < _tiles.SizeY; i.y++)
            {
                for (i.x = 0; i.x < _tiles.SizeX; i.x++)
                {
                    var tile = _viewAdapter[i];
                    var model = _palette.Lookup(tile);
                    _tiles[i] = new LevelTile(model, i);
                }
            }
        }

        private void Update()
        {
            ApplyDirtyListToView();
        }

        private void FromView()
        {
            var i = Vector3Int.zero;
            for (i.y = 0; i.y < _tiles.SizeY; i.y++)
            {
                for (i.x = 0; i.x < _tiles.SizeX; i.x++)
                {
                    var tile = _viewAdapter[i];
                    var model = _palette.Lookup(tile);
                    _tiles[i].model = model;
                }
            }
        }

        private void ToView()
        {
            var i = Vector3Int.zero;
            for (i.y = 0; i.y < _tiles.SizeY; i.y++)
            {
                for (i.x = 0; i.x < _tiles.SizeX; i.x++)
                {
                    _viewAdapter[i] = _tiles[i].model.tile;
                }
            }
        }

        public void GetTiles(Bounds bounds, List<LevelTile> levelTiles)
        {
            levelTiles.Clear();
            var min = _viewAdapter.WorldToCell(bounds.min);
            var max = _viewAdapter.WorldToCell(bounds.max);
            var i = Vector3Int.zero;
            for (i.y = min.y; i.y <= max.y; i.y++)
            {
                for (i.x = min.x; i.x <= max.x; i.x++)
                {
                    levelTiles.Add(_tiles[i]);
                }
            }
        }

        public void SetDirty(List<LevelTile> levelTiles)
        {
            _dirtyList.AddRange(levelTiles);
        }

        private void ApplyDirtyListToView()
        {
            foreach (var tile in _dirtyList)
            {
                if (!tile.damageReceiver.IsAlive)
                {
                    // todo: add die fx for tile
                    tile.model = LevelTileModel.empty;
                    var position = tile.position;
                    _viewAdapter[position] = tile.model.tile;
                }
            }

            _dirtyList.Clear();
        }

        public void ToPhotonStream(PhotonStream stream)
        {
            var tilesData = _tiles.tiles;
            foreach (var tile in tilesData)
            {
                tile.ToPhotonStream(stream);
            }
        }

        public void FromPhotonStream(PhotonStream stream)
        {
            var tilesData = _tiles.tiles;
            foreach (var tile in tilesData)
            {
                tile.FromPhotonStream(stream, _palette);
            }
            ToView();
        }
    }
}