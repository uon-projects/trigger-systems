using UnityEngine;

namespace Trigger
{
    public class ActionRotationToward : ActionRotation
    {
        public GameObject rotateToward;

        protected override Quaternion RotateTowards()
        {
            var direction = rotateToward.transform.rotation * Quaternion.Inverse(transform.rotation);

            return transform.rotation * direction;
        }
    }
}