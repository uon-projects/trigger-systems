using UnityEngine;

namespace Trigger
{
    public class ActionDebug : Action
    {
        public string msg;

        private void Start()
        {
            enabled = false;
        }

        public override void Update()
        {
            End();
        }

        public override void Run(GameObject obj)
        {
            Debug.Log(msg);
            End();
        }

        public override void End()
        {
            SetState(ETriggerStates.FINISHED);
        }
    }
}