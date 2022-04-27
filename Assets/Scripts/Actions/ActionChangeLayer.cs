using UnityEngine;

namespace Trigger
{
    public class ActionChangeLayer : Action
    {
        public int layer;
        public GameObject gObject;

        public void Start()
        {
            if (gObject == null) gObject = gameObject;
        }

        public override void Update()
        {
            End();
        }

        public override void Run(GameObject obj)
        {
            Log("Changing layer from " + gObject.layer + " to " + layer);
            gObject.layer = layer;
            End();
        }

        public override void End()
        {
            SetState(ETriggerStates.FINISHED);
        }
    }
}