using UnityEngine;

namespace Trigger
{
    public class ActionRotationTo : ActionRotation
    {
        public float rotation;
        public Vector3 direction;

        protected override Quaternion RotateTowards()
        {
            return AddDegreesToQuaternion(transform.rotation, direction, rotation);
        }

        private Quaternion AddDegreesToQuaternion(Quaternion to, Vector3 dir, float degrees)
        {
            return to * Quaternion.Euler(degrees * dir.x,
                degrees * dir.y,
                degrees * dir.z);
        }
    }
}