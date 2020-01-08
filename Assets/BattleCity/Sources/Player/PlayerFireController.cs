using System;
using BattleCity.Config;
using BattleCity.Game.Damage;
using CoreUtils.EnumsUtils;
using Photon.Pun;
using UnityEngine;

namespace BattleCity.Player
{
    public class PlayerFireController : MonoBehaviour
    {
        [SerializeField] private string projectileId = "Projectile";
        [SerializeField] private float spawnOffset = 0.24f;
        [SerializeField] private PlayerModel playerModel;
        [SerializeField] private AudioSource fireSound;
        [SerializeField] private Transform fireDirection;

        private float _lastFireTime;

        public void Fire(Vector3 position, Vector3 direction)
        {
            if (Time.time - _lastFireTime < playerModel.attackTimeout) return;
            direction = (fireDirection.position - position).normalized;
            var projectileGameObject = PhotonNetwork.Instantiate(projectileId, position + direction*spawnOffset, Quaternion.LookRotation(Vector3.back, direction));
            projectileGameObject.SetActive(true);
            _lastFireTime = Time.time;
            PlayFileSoundFx();
        }

        public void PlayFileSoundFx()
        {
            if (fireSound == null) return;
            fireSound.Play();
        }
    }
}