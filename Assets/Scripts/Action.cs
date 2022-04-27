using UnityEngine;

namespace Trigger
{
    public abstract class Action : MonoBehaviour,
        IDebug,
        ITriggerStates,
        ITriggerExecute
    {
        public bool debug;

        private ETriggerStates state = ETriggerStates.NOT_ACTIVATED;

        public void Log(string msg)
        {
            if (debug) Debug.Log(name + ".action: " + msg);
        }

        public abstract void Run(GameObject obj);

        public virtual void Pause()
        {
            Log("base pause detected");

            SetState(ETriggerStates.PAUSE);
        }

        public virtual void Continue(GameObject obj)
        {
            SetState(ETriggerStates.RUNNING);
        }

        public abstract void Update();
        public abstract void End();

        public void SetState(ETriggerStates triggerState)
        {
            state = triggerState;
            enabled = triggerState == ETriggerStates.RUNNING;
        }

        public bool IsState(ETriggerStates triggerState)
        {
            return state == triggerState;
        }
    }
}