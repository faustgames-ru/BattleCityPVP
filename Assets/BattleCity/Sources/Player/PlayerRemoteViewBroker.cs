using Photon.Pun;
using UnityEngine;

namespace BattleCity.Player
{
    public class PlayerRemoteViewBroker : MonoBehaviour
    {
        [SerializeField]
        private PlayerMoveController moveController;
        [SerializeField]
        private PlayerDirectionController directionController;
        [SerializeField]
        private PhotonView photonView;

        private void Update()
        {
            if (photonView.IsMine || !PhotonNetwork.IsConnected) return;
            directionController.SetDirection(moveController.GetVelocity().normalized);
        }
    }
}