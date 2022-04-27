using UnityEngine;

namespace Trigger
{
    public class ActionSwap : Action
    {
        public GameObject aObject;
        public GameObject bObject;

        private void Start()
        {
            enabled = false;
            if (aObject == null || bObject == null) Debug.LogError("Object(s) not initialized");
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
            SwapPosition(aObject.transform, bObject.transform);
            SetState(ETriggerStates.FINISHED);
        }

        private void SwapPosition(Transform a, Transform b)
        {
            Log("swapped positions between gameobjects: " + a.position + "->" + b.position);
            var tmp = a.position;
            a.position = b.position;
            b.position = tmp;
        }
    }
}