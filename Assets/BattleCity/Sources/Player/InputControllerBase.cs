using UnityEngine;

namespace BattleCity.Player
{
    public abstract class InputControllerBase : MonoBehaviour
    {
        public event DirectionEvent DirectionChanged;
        public event FireEvent Fire;

        protected void OnDirectionChanged(Vector3 direction)
        {
            DirectionChanged?.Invoke(this, direction);
        }

        protected void OnFire()
        {
            Fire?.Invoke(this);
        }
    }
}