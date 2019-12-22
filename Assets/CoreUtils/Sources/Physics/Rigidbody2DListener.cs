using UnityEngine;

namespace CoreUtils.Physics
{
    public class Rigidbody2DListener : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D body;

        public event CollisionEvent CollisionEnter;
        public event CollisionEvent CollisionExit;
        public event TriggerEvent TriggerEnter;
        public event TriggerEvent TriggerExit;

        private void OnCollisionEnter2D(Collision2D other)
        {
            CollisionEnter?.Invoke(body, other);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            CollisionExit?.Invoke(body, other);

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEnter?.Invoke(body, other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TriggerExit?.Invoke(body, other);
        }
    }
}