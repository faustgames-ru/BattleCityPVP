using BattleCity.Config;
using BattleCity.Game;
using BattleCity.Game.Damage;
using CoreUtils.EnumsUtils;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace BattleCity.Level
{
    public class CommandCenterObserver : MonoBehaviour, IPunObservable
    {
        [EnumFlags]
        [SerializeField] private DamageLayer damageLayer = DamageLayer.All;

        public float damageZone = 0.32f;
        public CommandCenterModel model;
        public GameObject view;
        public TextMeshPro healthView;
        public DamageReceiverSingleBoundsFilter damageFilter;
        public PhotonView photonView;
        public float rotateViewOnSlave = 180;
        public GameController gameController;
        public bool master;
        private DamageReceiver _damageReceiver;

        private void Awake()
        {
            _damageReceiver = new DamageReceiver(model.damageReceiver) {damageLayer = damageLayer};
            damageFilter.receiverBounds = new Bounds(transform.position, new Vector3(damageZone, damageZone, 0));
            damageFilter.damageReceiver = _damageReceiver;
        }

        private void FixedUpdate()
        {
            view.SetActive(_damageReceiver.IsAlive);
            healthView.text = _damageReceiver.FormatHealth();

            // crutch
            if (!PhotonNetwork.IsMasterClient)
            {
                transform.rotation = Quaternion.Euler(0, 0, rotateViewOnSlave);
            }
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
                _damageReceiver.health = health;
            }

            if (_damageReceiver.IsAlive) return;
            gameController.CommandCenterDead(master);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(damageZone, damageZone, 0));
        }
    }
}