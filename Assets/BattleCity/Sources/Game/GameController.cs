using Photon.Pun;
using UnityEngine;

namespace BattleCity.Game
{
    public class GameController: MonoBehaviour
    {
        public string playerPrefabId = "Player";
        public Transform spawnPoint;

        private void Awake()
        {
            
        }

        public void SpawnPlayer()
        {
            PhotonNetwork.Instantiate(playerPrefabId, spawnPoint.position, spawnPoint.rotation);
        }
    }
}