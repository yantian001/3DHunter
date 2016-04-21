using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("Returns success when enemies is died.")]
    [TaskCategory("Hunter")]
    public class IsDie : Conditional
    {

        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        private bool enteredTrigger = false;

        private GameObject targetObject;
        private DamageManager dm;

        public override void OnStart()
        {
            if (targetObject == null)
            {
                targetObject = GetDefaultGameObject(targetGameObject.Value);

            }
            if (targetObject != null)
            {
                dm = targetObject.GetComponent<DamageManager>();
            }
        }


        public override TaskStatus OnUpdate()
        {

           if(dm == null)
            {
                return TaskStatus.Failure;
            }
           else
            {
                return dm.IsDie() ? TaskStatus.Success : TaskStatus.Failure;
            }
        }

        public override void OnEnd()
        {
            enteredTrigger = false;
        }
        
        public override void OnReset()
        {
            targetGameObject = null;
            dm = null;
        }
    }
}