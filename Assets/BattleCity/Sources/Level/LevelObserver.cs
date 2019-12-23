using Photon.Pun;
using UnityEngine;

namespace BattleCity.Level
{
    public class LevelObserver : MonoBehaviour, IPunObservable
    {
        [SerializeField]
        private LevelController levelController;

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                levelController.ToPhotonStream(stream);
            }
            else
            {
                levelController.FromPhotonStream(stream);
            }
        }
    }
}