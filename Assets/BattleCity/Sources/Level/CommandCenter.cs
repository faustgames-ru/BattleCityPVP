using UnityEngine;

namespace BattleCity.Level
{
    public class CommandCenter : MonoBehaviour
    {
        private Transform _cameraTransform;
        private Transform _transform;

        private void Awake()
        {
            var worldCamera = Camera.main;
            if (worldCamera != null)
            {
                _cameraTransform = worldCamera.GetComponent<Transform>();
            }

            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            _transform.rotation = _cameraTransform.rotation;
        }
    }
}