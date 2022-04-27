using UnityEngine;

namespace Trigger
{
    public class ActionToggle : Action
    {
        public GameObject certainObj;

        private void Start()
        {
            if (certainObj == null) Debug.Log("game object not inited");
        }

        public override void Update()
        {
            End();
        }

        public override void End()
        {
            SetState(ETriggerStates.FINISHED);
        }

        public override void Run(GameObject obj)
        {
            Toggle(certainObj != null ? certainObj : gameObject);

            End();
            Log("Object toggled");
        }

        private void Toggle(GameObject willToggle)
        {
            willToggle.SetActive(!willToggle.activeSelf);
        }
    }
}