using System.Collections.Generic;
using BattleCity.Config;
using BattleCity.Game;
using BattleCity.Game.Damage;
using BattleCity.Level;
using CoreUtils.Collections;
using CoreUtils.EnumsUtils;
using Photon.Pun;
using UnityEngine;

namespace BattleCity.Player
{
    public class Projectile : MonoBehaviour, IDamageSystemInject
    {
        [SerializeField] private PoolListener poolListener;
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private PlayerModel playerModel;
        [SerializeField] private PhotonView photonView;
        [SerializeField] private GameObject view;
        [SerializeField] private ParticleSystem explosion;
        [SerializeField] private AudioSource explosionSound;
        [EnumFlags]
        [SerializeField] private DamageLayer damageLayer = DamageLayer.All;

        private PoolItem _poolItem;
        private Transform _transform;
        private DamageSystem _damageSystem;
        private DamageSpawner _damageSpawner;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _damageSpawner = new DamageSpawner(_transform, playerModel.damageSpawner) {damageLayer = damageLayer};
            _damageSpawner.Muted += DamageSpawnerOnMuted;
            _damageSpawner.Dead += DamageSpawnerOnDead;
            _damageSpawner.Collision += DamageSpawnerOnCollision;

            poolListener.GetInstance += PoolListenerOnGetInstance;
            poolListener.ReturnInstance += PoolListenerOnReturnInstance;
        }

        public void Inject(DamageSystem damageSystem)
        {
            _damageSystem = damageSystem;
        }

        private void DamageSpawnerOnMuted()
        {
            view.SetActive(false);
        }

        private void DamageSpawnerOnCollision()
        {
            PlayExplosionFx();
            PlayExplosionAudioFx();
        }

        private void PlayExplosionFx()
        {
            if (explosion == null) return;
            explosion.Play();
        }

        private void PlayExplosionAudioFx()
        {
            if (explosionSound == null) return;
            explosionSound.Play();
        }

        private void DamageSpawnerOnDead()
        {
            if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
            PhotonNetwork.Destroy(_poolItem.Instance);
        }

        private void PoolListenerOnGetInstance(PoolItem sender)
        {
            _poolItem = sender;
            view.SetActive(true);
            _damageSpawner.Reset();
            _damageSystem.AddSpawner(_damageSpawner);
        }

        private void PoolListenerOnReturnInstance(PoolItem sender)
        {
            _damageSystem.RemoveSpawner(_damageSpawner);
        }
        
        private void FixedUpdate()
        {
            if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
            if (_damageSpawner.IsMuted) return;
            body.velocity = _transform.up * playerModel.projectileVelocity;
        }
    }
}