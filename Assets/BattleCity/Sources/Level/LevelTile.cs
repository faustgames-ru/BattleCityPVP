using BattleCity.Game;
using BattleCity.Game.Damage;
using Photon.Pun;
using UnityEngine;

namespace BattleCity.Level
{
    public class LevelTile
    {
        public LevelTileModel model;
        public Vector3Int position;
        public readonly DamageReceiver damageReceiver;

        public LevelTile(LevelTileModel model, Vector3Int position)
        {
            this.model = model;
            this.position = position;
            damageReceiver = model.CreateDamageReceiver();
        }

        public void ToPhotonStream(PhotonStream stream)
        {
            stream.SendNext(model.id);
        }

        public void FromPhotonStream(PhotonStream stream, LevelTilePalette palette)
        {
            var id = (int)stream.ReceiveNext();
            model = palette.GetModel(id);
        }
    }
}