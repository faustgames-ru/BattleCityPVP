﻿using BattleCity.Config;
using BattleCity.Game;
using BattleCity.Game.Damage;
using CoreUtils.Collections;
using CoreUtils.EnumsUtils;
using Photon.Pun;
using UnityEngine;

namespace BattleCity.Player
{
    public class PlayerObserver: MonoBehaviour, IPunObservable, IDamageFilterInject, IGameControllerInject
    {
        [SerializeField] private PlayerModel playerModel;
        [EnumFlags]
        [SerializeField] private DamageLayer damageLayer = DamageLayer.All;

        [SerializeField] private float damageZone = 0.32f;
        [SerializeField] private GameObject view;
        [SerializeField] private PoolListener poolListener;
        [SerializeField] private DamageReceiverSingleBoundsFilter damageReceiverFilter;
        [SerializeField] private PhotonView photonView;
        [Header("FSM")]
        [SerializeField] private PlayMakerFSM playerFsm;
        [SerializeField] private string dieEvent = "PLAYER/DIE";
        
        private GameController _gameController;
        private DamageReceiver _damageReceiver;
        private DamageFilter _damageFilter;
        private Transform _transform;
        private PoolItem _poolItem;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _damageReceiver = new DamageReceiver(playerModel.damageReceiver) {damageLayer = damageLayer};
            _damageReceiver.Dead += DamageReceiverOnDead;
            damageReceiverFilter.damageReceiver = _damageReceiver;
            poolListener.GetInstance += PoolListenerOnGetInstance;
            poolListener.ReturnInstance += PoolListenerOnReturnInstance;
        }

        private void DamageReceiverOnDead()
        {
            //view.SetActive(false);
            // todo: send event to player fsm
            playerFsm.SendEvent(dieEvent);
            /*
            if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
            _gameController.MinePlayerDead();
            PhotonNetwork.Destroy(_poolItem.Instance);
            */
        }

        public void Inject(DamageFilter damageFilter)
        {
            _damageFilter = damageFilter;
        }

        public void Inject(GameController gameController)
        {
            _gameController = gameController;
        }

        private void PoolListenerOnGetInstance(PoolItem sender)
        {
            _poolItem = sender;
            _damageFilter.AddFilter(damageReceiverFilter);
            //view.SetActive(true);
        }

        private void PoolListenerOnReturnInstance(PoolItem sender)
        {
            _damageFilter.RemoveFilter(damageReceiverFilter);
        }

        private void FixedUpdate()
        {
            damageReceiverFilter.receiverBounds = new Bounds(_transform.position, new Vector3(damageZone, damageZone, 0f));
            /*
            view.SetActive(_damageReceiver.IsAlive);
            if (_damageReceiver.IsAlive) return;
            if (!photonView.IsMine && PhotonNetwork.IsConnected) return;

            _gameController.MinePlayerDead();
            PhotonNetwork.Destroy(_poolItem.Instance);
            */
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_damageReceiver.health);
            }
            else
            {
                var health = (int)stream.ReceiveNext();
                _damageReceiver.SetHealth(health);
            }
        }

        public void ResetPlayerHealth()
        {
            _damageReceiver.Reset();
        }
    }
}