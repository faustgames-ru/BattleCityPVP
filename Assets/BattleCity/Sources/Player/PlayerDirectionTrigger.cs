using System;
using UnityEngine;

namespace BattleCity.Player
{
    [Serializable]
    public class PlayerDirectionTrigger
    {
        [SerializeField]
        private Vector3 direction;
        [SerializeField]
        private string trigger;

        private int? _triggerId;

        public PlayerDirectionTrigger() { }
        public PlayerDirectionTrigger(Vector3 direction, string trigger)
        {
            this.direction = direction;
            this.trigger = trigger;
        }

        public Vector3 Direction => direction;

        private int GetTriggerId()
        {
            _triggerId = _triggerId ?? Animator.StringToHash(trigger);
            return _triggerId.Value;
        }

        public void SetAnimatorTrigger(Animator animator)
        {
            animator.SetTrigger(GetTriggerId());
        }

        public static float Dot(PlayerDirectionTrigger value, Vector3 direction)
        {
            return Vector3.Dot(value.direction, direction);
        }

        public void Transform(Transform cameraTransform)
        {
            direction = cameraTransform.InverseTransformDirection(direction);
        }
    }
}