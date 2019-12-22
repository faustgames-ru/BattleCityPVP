using CoreUtils.AnimatorUtils;
using HutongGames.PlayMaker;

namespace CoreUtils.PlayMakerUtils
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Waits animation event from animator component")]
	public class WaitAnimatorEvent : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(AnimatorEvents))]
        [Tooltip("An AnimatorEvents component is required")]
        public FsmOwnerDefault animatorEvents;

        [RequiredField]
        [Tooltip("The event name")]
        public FsmString eventName;

        private AnimatorEvents _animatorEvents;

        private AnimatorEvents AnimatorEvents
        {
            get
            {
                if (_animatorEvents == null)
                {
                    _animatorEvents = animatorEvents.GameObject.Value.GetComponent<AnimatorEvents>();
                }
                return _animatorEvents;
            }
        }

        public override void Reset()
        {
            animatorEvents = null;
            eventName = null;
        }

        public override void OnEnter()
        {
            AnimatorEvents.AnimatorEvent += AnimationEventsOnAnimatorEvent;
		}

        private void AnimationEventsOnAnimatorEvent(object sender, AnimatorEventArgs e)
        {
            if (eventName.Value != e.eventName) return;
            AnimatorEvents.AnimatorEvent -= AnimationEventsOnAnimatorEvent;
            Finish();
        }
    }

}
