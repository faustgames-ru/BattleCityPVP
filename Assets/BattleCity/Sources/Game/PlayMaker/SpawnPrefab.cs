using System;
using HutongGames.PlayMaker;
using Photon.Pun;
using UnityEngine;

namespace BattleCity.Game.PlayMaker
{
    [Serializable]
    [ActionCategory("BattleCity")]
    [HutongGames.PlayMaker.Tooltip("Instantiate Photon Network instance, without proxies and fsm sync")]
    public class SpawnPrefab : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(Transform))]
        [HutongGames.PlayMaker.Tooltip("An AnimatorEvents component is required")]
        public FsmOwnerDefault spawnPoint;

        [RequiredField]
        [UnityEngine.Tooltip("Prefab name")]
        public FsmString prefabId;

        private Transform _spawnPoint;

        private Transform SpawnPoint
        {
            get
            {
                if (_spawnPoint == null)
                {
                    _spawnPoint = spawnPoint.GameObject.Value.GetComponent<Transform>();
                }
                return _spawnPoint;
            }
        }

        public override void Reset()
        {
            prefabId = null;
            spawnPoint = null;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            PhotonNetwork.Instantiate(prefabId.Value, SpawnPoint.position, SpawnPoint.rotation);
            Finish();
        }
    }
}