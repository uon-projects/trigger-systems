using UnityEngine;

namespace Trigger
{
    public abstract class ActionRotation : ActionMovement
    {
        protected int iStep;
        protected Quaternion lastStep;
        protected Quaternion[] steps;

        protected override void UpdateNextStep()
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, steps[iStep], Time.deltaTime * speed);
        }

        protected abstract Quaternion RotateTowards();

        protected override void ProccessSteps()
        {
            var count = Mathf.CeilToInt(1f / step) + 1;
            lastStep = RotateTowards(); //transform.rotation *  Quaternion.Euler(rotation,0f,0f);
            steps = new Quaternion[count];

            var cut = 0f;
            for (var i = 0; i < count; i++, cut += step) steps[i] = Quaternion.Lerp(transform.rotation, lastStep, cut);

            iStep = 0;
            SetState(ETriggerMovementStates.FORWARD);
            Log("Calculated Steps " + (steps.Length - 1));

            speed = CalculateSpeed(Quaternion.Angle(steps[0], steps[count - 1]), time);
            Log("Speed " + speed);
        }

        private Quaternion AddDegreesToQuaternion(Quaternion to, Vector3 dir, float degrees)
        {
            return to * Quaternion.Euler(degrees * dir.x,
                degrees * dir.y,
                degrees * dir.z);
        }

        protected override bool IsReachedStep()
        {
            return transform.rotation == steps[iStep];
        }

        protected override void BoundStep()
        {
            if (iStep < 0) iStep = 0;
            else if (iStep > steps.Length) iStep = steps.Length - 1;
        }


        protected override void SwitchDirection()
        {
            if (IsState(ETriggerMovementStates.FORWARD))
            {
                SetState(ETriggerMovementStates.BACKWARD);
                iStep--;
            }
            else if (IsState(ETriggerMovementStates.BACKWARD))
            {
                SetState(ETriggerMovementStates.FORWARD);
                iStep++;
            }

            BoundStep();
            Log("Switched dir");
        }

        protected override bool MoveNext()
        {
            if (IsReachedStep()) iStep++;

            return iStep < steps.Length;
        }

        protected override bool MoveBack()
        {
            if (IsReachedStep())
            {
                Log("back segment");
                return false;
            }

            return iStep >= 0;
        }
    }
}