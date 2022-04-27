using UnityEngine;

namespace Trigger
{
    public abstract class Event : MonoBehaviour, IDebug, ITriggerExecute
    {
        public QueueActions queue;


        [SerializeField] protected ETriggerRunMode runMode = ETriggerRunMode.SYNCHRONOUSLY;
        [SerializeField] protected ETriggerWorkMode workMode = ETriggerWorkMode.FIRE;


        public bool once = true;


        public bool debug;


        [SerializeField] protected int countTriggered;


        private ETriggerStates state = ETriggerStates.NOT_ACTIVATED;


        protected GameObject whoTriggered;

        private void Start()
        {
            Init();
        }

        public void Log(string msg)
        {
            if (debug) Debug.Log(name + ".trigger: " + msg);
        }

        public abstract void Update();


        public void Run(GameObject obj)
        {
            if (IsState(ETriggerStates.NOT_ACTIVATED) || IsState(ETriggerStates.FINISHED))
            {
                Log("Starting trigger");
                NewStart(obj);
            }
            else if (IsState(ETriggerStates.PAUSE))
            {
                Log("Continuing trigger");
                Continue(obj);
            }
        }

        public void Continue(GameObject obj)
        {
            Log("Continue");
            SetState(ETriggerStates.RUNNING);

            if (IsState(ETriggerRunMode.ASYNCHRONOUSLY))
                foreach (var action in queue.GetActions())
                    action.Continue(obj);
            else
                queue.Current().Continue(obj);

            whoTriggered = obj;
        }

        public void Pause()
        {
            Log("Pause");
            SetState(ETriggerStates.PAUSE);
            if (IsState(ETriggerRunMode.ASYNCHRONOUSLY))
            {
                foreach (var action in queue.GetActions()) action.Pause();
            }
            else
            {
                if (queue.Current() != null)
                    queue.Current().Pause();
            }
        }

        public abstract void End();


        protected bool UpdateSyn()
        {
            if (queue.Current() == null)
            {
                Log("No action left");

                if (!once) queue.Recharge();

                whoTriggered = null;
                SetState(ETriggerStates.FINISHED);
                return false;
            }

            if (queue.Current().IsState(ETriggerStates.FINISHED))
            {
                Log("Current action is over, poping next action");
                PopAction(whoTriggered);
                return true;
            }

            return CheckOver();
        }


        protected bool UpdateAsyn()
        {
            return CheckOver();
        }


        private bool CheckOver()
        {
            if (IsAllActionsExecuted())
            {
                Log("All actions done!");
                SetState(ETriggerStates.FINISHED);

                return true;
            }

            return false;
        }

        protected bool IsExecuting()
        {
            if (IsState(ETriggerRunMode.SYNCHRONOUSLY))
                return queue.Current() != null &&
                       queue.Current().IsState(ETriggerStates.RUNNING);

            return IsAllActionsExecuted();
        }

        private void NewStart(GameObject obj)
        {
            if (once && countTriggered >= 1) return;

            whoTriggered = obj;

            if (IsState(ETriggerRunMode.SYNCHRONOUSLY))
                RunSynchronously(obj);
            else
                RunAsynchronously(obj);

            SetState(ETriggerStates.RUNNING);
            countTriggered++;
        }


        protected void PopAction(GameObject obj)
        {
            queue.Next();
            if (queue.Current() != null)
            {
                if (whoTriggered != null) whoTriggered = obj;

                Log("Found next action");
                queue.Current().Run(whoTriggered);
            }
        }


        protected void RunSynchronously(GameObject obj)
        {
            PopAction(obj);
        }


        protected void RunAsynchronously(GameObject obj)
        {
            whoTriggered = obj;
            Log("Starting asyn actions");
            foreach (var action in queue.GetActions()) action.Run(obj);
        }


        protected bool IsAllActionsExecuted()
        {
            return queue.IsAllActionsExecuted();
        }

        public void SetState(ETriggerStates state)
        {
            this.state = state;
            enabled = state == ETriggerStates.RUNNING;
        }

        public bool IsState(ETriggerStates state)
        {
            return this.state == state;
        }

        public void SetState(ETriggerRunMode runMode)
        {
            this.runMode = runMode;
        }

        public bool IsState(ETriggerRunMode runMode)
        {
            return this.runMode == runMode;
        }

        public void SetState(ETriggerWorkMode workMode)
        {
            this.workMode = workMode;
        }

        public bool IsState(ETriggerWorkMode workMode)
        {
            return this.workMode == workMode;
        }


        protected abstract void Init();
    }
}