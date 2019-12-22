using UnityEngine;

namespace CoreUtils.AnimatorUtils
{
    public class AnimatorEvents: MonoBehaviour
    {
        public event AnimatorEvent AnimatorEvent;
        private readonly AnimatorEventArgs _animatorEventArgs = new AnimatorEventArgs();

        public void InvokeAnimatorEvent(string eventName)
        {
            _animatorEventArgs.eventName = eventName;
            OnAnimatorEvent(_animatorEventArgs);
        }

        private void OnAnimatorEvent(AnimatorEventArgs e)
        {
            AnimatorEvent?.Invoke(this, e);
        }
    }
}