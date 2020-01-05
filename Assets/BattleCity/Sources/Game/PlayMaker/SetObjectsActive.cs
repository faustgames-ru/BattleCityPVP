using System;
using HutongGames.PlayMaker;
using UnityEngine;

namespace BattleCity.Game.PlayMaker
{
    [Serializable]
    [ActionCategory("BattleCity")]
    [HutongGames.PlayMaker.Tooltip("Set active for game object array")]
    public class SetObjectsActive : FsmStateAction
    {
        [RequiredField]
        [UnityEngine.Tooltip("Views")]
        [ArrayEditor(typeof(GameObject))]
        public FsmArray objects;

        [RequiredField]
        [UnityEngine.Tooltip("Activate View")]
        public FsmBool value;

        public override void Reset()
        {
            objects = null;
            value = null;
        }

        public override void OnEnter()
        {
            foreach (var item in objects.Values)
            {
                var gameObject = item as GameObject;
                if (gameObject == null) continue;
                gameObject.SetActive(value.Value);
            }
            Finish();
        }
    }
}