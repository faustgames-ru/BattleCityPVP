using BattleCity.Config;
using Photon.Pun;
using UnityEngine;

namespace BattleCity.Player
{
    public class PlayerInputBroker : MonoBehaviour
    {
        [SerializeField] private PlayerModel playerModel;
        [SerializeField] private PlayerMoveController moveController;
        [SerializeField] private PlayerDirectionController directionController;
        [SerializeField] private PlayerFireController fireController;
        [SerializeField] private InputControllerBase inputController;
        [SerializeField] private PhotonView photonView;

        private void Awake()
        {
            inputController.DirectionChanged += InputOnDirectionChanged;
            inputController.Fire += InputControllerOnFire;
        }

        private void InputControllerOnFire(object sender)
        {
            if (!gameObject.activeInHierarchy) return;
            if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
            fireController.Fire(moveController.GetPosition(), directionController.GetDirection());
        }

        private void InputOnDirectionChanged(object sender, Vector3 direction)
        {
            if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
            directionController.SetDirection(direction);
            moveController.SetVelocity(direction * playerModel.moveVelocity);
        }

        private void FixedUpdate()
        {
            InputControllerOnFire(null);
        }
    }
}