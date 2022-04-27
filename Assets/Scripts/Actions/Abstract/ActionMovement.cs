using UnityEngine;

namespace Trigger
{
    public abstract class ActionMovement : Action,
        ITriggerMovementStates
    {
        public float time = 10f;

        public float step = 1f;

        public EActionMovementStates type = EActionMovementStates.ONLY_FORWARD;
        protected ETriggerMovementStates move = ETriggerMovementStates.NOT_ACTIVED;
        protected float speed;

        private void Start()
        {
            enabled = false;
        }


        public override void Update()
        {
            if (IsState(ETriggerStates.RUNNING))
            {
                UpdateNextStep();

                if (IsState(ETriggerMovementStates.FORWARD))
                    MoveToNextPosition();
                else if (IsState(ETriggerMovementStates.BACKWARD)) MoveToBackPosition();
            }
        }


        public bool IsState(ETriggerMovementStates state)
        {
            return state == move;
        }

        public void SetState(ETriggerMovementStates state)
        {
            move = state;
        }

        protected abstract void UpdateNextStep();
        protected abstract void ProccessSteps();
        protected abstract bool MoveNext();
        protected abstract bool MoveBack();
        protected abstract bool IsReachedStep();
        protected abstract void SwitchDirection();
        protected abstract void BoundStep();

        public override void End()
        {
            SetState(ETriggerStates.FINISHED);
            SetState(ETriggerMovementStates.FINISHED);
        }

        public override void Run(GameObject whoTriggered)
        {
            ProccessSteps();
            MoveToNextPosition();
        }

        public override void Pause()
        {
            Log("Pause detected");
            if (type == EActionMovementStates.ONLY_FORWARD)
                base.Pause();
            else if (type == EActionMovementStates.FORWARD_BACKWARD) SwitchDirection();
        }

        public override void Continue(GameObject obj)
        {
            Log("Continue detected");
            if (type == EActionMovementStates.FORWARD_BACKWARD)
                SwitchDirection();
            else if (type == EActionMovementStates.ONLY_FORWARD) SetState(ETriggerMovementStates.FORWARD);

            SetState(ETriggerStates.RUNNING);
        }


        private void MoveToNextPosition()
        {
            if (MoveNext() /*it.MoveNext ()*/)
            {
                SetState(ETriggerStates.RUNNING);
            }
            else
            {
                Log("Reached last pos");
                End();
            }
        }

        private void MoveToBackPosition()
        {
            Log("Moving to back pos");
            if (MoveBack())
                SetState(ETriggerStates.RUNNING);
            else
                SetState(ETriggerStates.PAUSE);
        }


        protected float CalculateSpeed(float distance, float time)
        {
            return distance / time;
        }
    }
}