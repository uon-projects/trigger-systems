using UnityEngine;

namespace Trigger
{
    public class EventOnUseTransmitter : MonoBehaviour,
        ITriggerUse,
        ITriggerStates,
        IDebug
    {
        public bool debug;
        public bool cap;

        public EventOnUse receiver;

        private ETriggerStates state = ETriggerStates.NOT_ACTIVATED;
        private GameObject whoUse;

        private void Start()
        {
            RegisterTransmitter();
            SetState(ETriggerStates.NOT_ACTIVATED);
        }

        public void Log(string msg)
        {
            if (debug) Debug.Log(msg);
        }

        public bool IsState(ETriggerStates State)
        {
            return this.state == State;
        }

        public void SetState(ETriggerStates state)
        {
            this.state = state;
        }

        public EUseState Use(GameObject whoUse)
        {
            if (IsState(ETriggerStates.NOT_ACTIVATED) || IsState(ETriggerStates.PAUSE))
            {
                Using(whoUse);
                receiver.Signal(whoUse);
                enabled = true;
                return EUseState.USING;
            }

            return EUseState.FAILED;
        }

        public void Using(GameObject whoUsing)
        {
            whoUse = whoUsing;
        }

        public void RegisterTransmitter()
        {
            if (receiver == null)
            {
                Debug.LogError("Receiver is null!");
                return;
            }

            receiver.AddTransmitter(this);
        }

        public void StopUse(GameObject whoUse)
        {
            if (whoUse.Equals(this.whoUse))
                this.whoUse = null;
        }

        public bool IsUsing()
        {
            return whoUse != null;
        }
    }
}