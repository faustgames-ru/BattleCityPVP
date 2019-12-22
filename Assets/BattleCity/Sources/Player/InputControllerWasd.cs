using BattleCity.Config;
using UnityEngine;

namespace BattleCity.Player
{
    public class InputControllerWasd : InputControllerBase
    {
        [SerializeField]
        private InputKeyboardConfig keyboardConfig;

        private Transform _cameraTransform;
        private void Awake()
        {
            var worldCamera = Camera.main;
            if (worldCamera == null) return;
            _cameraTransform = worldCamera.GetComponent<Transform>();
        }

        private void Update()
        {
            var direction = keyboardConfig.GetInputDirection();
            if (_cameraTransform != null)
            {
                direction = _cameraTransform.TransformDirection(direction);
            }
            OnDirectionChanged(direction);
            if (keyboardConfig.GetInputFire())
            {
                OnFire();
            }
        }
    }
}