using UnityEngine;

namespace Trigger
{
    public class ActionMoveToward : ActionMovement
    {
        public GameObject moveTo;
        protected int iStep;
        protected Vector3[] steps;

        protected override void UpdateNextStep()
        {
            if (debug) Debug.DrawLine(transform.position, steps[iStep], Color.cyan);

            transform.position = Vector3.MoveTowards(transform.position, steps[iStep], Time.deltaTime * speed);
        }

        protected override void ProccessSteps()
        {
            var count = Mathf.CeilToInt(1f / step) + 1;
            steps = new Vector3[count];

            var cut = 0f;
            for (var i = 0; i < count; i++, cut += step)
                steps[i] = Vector3.Lerp(transform.position, moveTo.transform.position, cut);

            iStep = 0;
            SetState(ETriggerMovementStates.FORWARD);
            Log("Calculated Steps " + (steps.Length - 1));

            speed = CalculateSpeed(Vector3.Distance(steps[0], steps[count - 1]), time);
            Log("Speed " + speed);
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

        protected override bool IsReachedStep()
        {
            return
                transform.position == steps[iStep];
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
    }
}