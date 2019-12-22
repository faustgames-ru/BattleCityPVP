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
        private Transform spawnPoint;
        [SerializeField]
        Rigidbody2D ownerPlayerBody;
        [SerializeField]
        private PlayerModel playerModel;

        private float _lastFireTime;

        public void Fire(Vector3 direction)
        {
            if (Time.time - _lastFireTime < playerModel.attackTimeout) return;
            var projectileGameObject = PhotonNetwork.Instantiate(projectileId, spawnPoint.position, Quaternion.LookRotation(Vector3.back, direction));
            var projectile = projectileGameObject.GetComponent<Projectile>();
            projectile.ownerPlayerBody = ownerPlayerBody;
            projectileGameObject.SetActive(true);
            _lastFireTime = Time.time;
        }
    }
}