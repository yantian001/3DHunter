using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
    [TaskCategory("Basic/Animation")]
    [TaskDescription("Returns Success if the animation is play finished.")]
    public class IsPlayed : Conditional
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The name of the animation")]
        public SharedString animationName;

        // cache the animation component
        private Animation animation;
        private GameObject prevGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                animation = currentGameObject.GetComponent<Animation>();
                prevGameObject = currentGameObject;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (animation == null) {
                Debug.LogWarning("Animation is null");
                return TaskStatus.Failure;
            }
            
            //if (string.IsNullOrEmpty(animationName.Value)) {
            //    return animation.isPlaying ? TaskStatus.Success : TaskStatus.Failure;
            //} else {
            //    return animation.IsPlaying(animationName.Value) ? TaskStatus.Success : TaskStatus.Failure;
            //}

            if(animation.IsPlaying(animationName.Value))
            {
                if(animation[animationName.Value].normalizedTime >= 0.9)
                {
                    return TaskStatus.Success;
                }
                else
                {
                    return TaskStatus.Running;
                }
            }
            return TaskStatus.Failure;
            
        }

        public override void OnReset()
        {
            targetGameObject = null;
            animationName.Value = "";
        }
    }
}