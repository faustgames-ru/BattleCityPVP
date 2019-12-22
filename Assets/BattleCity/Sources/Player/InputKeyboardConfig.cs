using System.Collections.Generic;
using CoreUtils.Collections;
using UnityEngine;

namespace BattleCity.Player
{
    [CreateAssetMenu(fileName = "InputKeyboardConfig", menuName = "BattleCity/InputKeyboardConfig")]
    public class InputKeyboardConfig: ScriptableObject
    {
        [SerializeField]
        private Vector3 defaultDirection = Vector3.zero;
        [SerializeField]
        private InputDirectionKey[] directionKeys =
        {
            new InputDirectionKey(Vector3.up, KeyCode.W, KeyCode.UpArrow),
            new InputDirectionKey(Vector3.down, KeyCode.S, KeyCode.DownArrow),
            new InputDirectionKey(Vector3.left, KeyCode.A, KeyCode.LeftArrow),
            new InputDirectionKey(Vector3.right, KeyCode.D, KeyCode.RightArrow),
        };

        [SerializeField]
        private KeyCode[] fireKeys = {KeyCode.Space};

        private readonly List<InputDirectionKey> _pressedKeys = new List<InputDirectionKey>();

        public bool GetInputFire()
        {
            foreach (var fireKey in fireKeys)
            {
                if (Input.GetKey(fireKey))
                    return true;
            }
            return false;
        }

        public Vector3 GetInputDirection()
        {
            directionKeys.Filter(InputDirectionKey.IsPressedFilter, true, _pressedKeys);
            var lastPressedKey = _pressedKeys.Max(InputDirectionKey.GetPressTimeDelta, 0f);
            if (lastPressedKey == null) return defaultDirection;
            return lastPressedKey.Direction;
        }
    }
}