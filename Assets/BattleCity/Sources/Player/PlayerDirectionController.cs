using CoreUtils.Collections;
using UnityEngine;

namespace BattleCity.Player
{
    public class PlayerDirectionController: MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private PlayerDirectionTrigger[] triggers =
        {
            new PlayerDirectionTrigger(Vector3.left, "Left"),
            new PlayerDirectionTrigger(Vector3.right, "Right"),
            new PlayerDirectionTrigger(Vector3.up, "Up"),
            new PlayerDirectionTrigger(Vector3.down, "Down"),
        };

        private PlayerDirectionTrigger _currentDirection;
        
        public void SetDirection(Vector3 direction)
        {
            if (direction == Vector3.zero) return;
            var directionTrigger = triggers.Max(PlayerDirectionTrigger.Dot, direction);
            if (directionTrigger == _currentDirection) return;
            _currentDirection = directionTrigger;
            _currentDirection.SetAnimatorTrigger(animator);
        }

        public Vector3 GetDirection()
        {
            if (_currentDirection == null) return Vector3.zero;
            return _currentDirection.Direction;
        }
    }
}