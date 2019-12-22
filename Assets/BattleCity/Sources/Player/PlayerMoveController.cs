using UnityEngine;

namespace BattleCity.Player
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D body;

        public void SetVelocity(Vector3 velocity)
        {
            body.velocity = velocity;
        }

        public Vector3 GetVelocity()
        {
            return body.velocity;
        }

        public Vector3 GetPosition()
        {
            return body.position;
        }
    }
}