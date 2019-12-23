using System;
using BattleCity.Config;
using Photon.Pun;
using UnityEngine;

namespace BattleCity.Player
{
    public class PlayerFireController : MonoBehaviour
    {
        [SerializeField]
        private string projectileId = "Projectile";
        [SerializeField]
        private float spawnOffset = 0.24f;
        [SerializeField]
        private PlayerModel playerModel;

        private float _lastFireTime;

        public void Fire(Vector3 position, Vector3 direction)
        {
            if (Time.time - _lastFireTime < playerModel.attackTimeout) return;
            var projectileGameObject = PhotonNetwork.Instantiate(projectileId, position + direction*spawnOffset, Quaternion.LookRotation(Vector3.back, direction));
            projectileGameObject.SetActive(true);
            _lastFireTime = Time.time;
        }
    }
}