using System;
using UnityEngine;
using UnityEngine.Experimental.LowLevel;

namespace BattleCity.Player
{
    [Serializable]
    public class InputDirectionKey
    {
        [SerializeField]
        private Vector3 direction;
        [SerializeField]
        private KeyCode[] keyCodes;

        private bool _status;
        private float _pressedTime;

        public Vector3 Direction => direction;

        public InputDirectionKey() { }

        public InputDirectionKey(Vector3 direction, params KeyCode[] keyCodes)
        {
            this.direction = direction;
            this.keyCodes = keyCodes;
        }

        public void UpdateStatus()
        {
            var newStatus = GetKeyPressed();
            if (newStatus == _status) return;
            if (newStatus)
            {
                _pressedTime = Time.time;
            }
            _status = newStatus;
            return;
        }

        private bool GetKeyPressed()
        {
            foreach (var code in keyCodes)
            {
                if (Input.GetKey(code))
                    return true;
            }
            return false;
        }

        public static bool IsPressedFilter(InputDirectionKey key, bool value)
        {
            key.UpdateStatus();
            return key._status == value;
        }

        public static float GetPressTimeDelta(InputDirectionKey key, float time)
        {
            return key._pressedTime - time;
        }
    }
}