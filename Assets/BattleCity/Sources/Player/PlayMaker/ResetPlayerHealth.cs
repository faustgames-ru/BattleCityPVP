using System;
using HutongGames.PlayMaker;

namespace BattleCity.Player.PlayMaker
{
    [Serializable]
    [ActionCategory("BattleCity")]
    [Tooltip("Set player health to default value")]
    public class ResetPlayerHealth: FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(PlayerObserver))]
        [Tooltip("An AnimatorEvents component is required")]
        public FsmOwnerDefault player;

        public override void OnEnter()
        {
            base.OnEnter();
            var playerObserver = player.GameObject.Value.GetComponent<PlayerObserver>();
            playerObserver.ResetPlayerHealth();
        }
    }
}