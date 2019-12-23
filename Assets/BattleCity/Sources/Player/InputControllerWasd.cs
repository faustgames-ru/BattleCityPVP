using BattleCity.Config;
using UnityEngine;

namespace BattleCity.Player
{
    public class InputControllerWasd : InputControllerBase
    {
        [SerializeField]
        private InputKeyboardConfig keyboardConfig;

        [SerializeField]
        private Vector3 rotateDirection = Vector3.zero;

        private void Update()
        {
            var direction = Quaternion.Euler(rotateDirection) * keyboardConfig.GetInputDirection();
            OnDirectionChanged(direction);
            if (keyboardConfig.GetInputFire())
            {
                OnFire();
            }
        }
    }
}