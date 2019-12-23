using Photon.Pun;
using UnityEngine;

namespace BattleCity.Game
{
    public class GameController: MonoBehaviour
    {
        public PlayMakerFSM fsm;
        public string minePlayerDeadEvent = "PLAYER DEAD";
        public string loseEvent = "LOSE";
        public string victoryEvent = "VICTORY";
        public bool gameFinished;

        public void MinePlayerDead()
        {
            if (gameFinished) return;
            fsm.SendEvent(minePlayerDeadEvent);
        }

        public void CommandCenterDead(bool master)
        {
            if(gameFinished) return;

            gameFinished = true;
            if (master)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    Lose();
                }
                else
                {
                    Victory();
                }
            }
            else
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    Victory();
                }
                else
                {
                    Lose();
                }
            }
        }

        private void Lose()
        {
            fsm.SendEvent(loseEvent);
        }

        private void Victory()
        {
            fsm.SendEvent(victoryEvent);
        }
    }
}