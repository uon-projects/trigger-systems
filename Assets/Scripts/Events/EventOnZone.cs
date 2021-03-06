using System.Collections.Generic;
using UnityEngine;

namespace Trigger
{
    public class EventOnZone : Event
    {
        [SerializeField] public AllowedLayers layers;


        protected List<Collider> enteredCollider;

        public override void Update()
        {
            if (!IsState(ETriggerStates.RUNNING)) return;

            if (IsState(ETriggerRunMode.SYNCHRONOUSLY))
                UpdateSync();
            else
                UpdateAsync();
        }


        private void OnTriggerEnter(Collider col)
        {
            Log("Collider entered");
            if (!layers.IsAllowed(col.gameObject.layer)) return;

            enteredCollider.Add(col);
            Run(col.gameObject);
            Log("Access allowed");
        }


        private void OnTriggerExit(Collider col)
        {
            enteredCollider.Remove(col);

            Out(col.gameObject);

            if (enteredCollider.Count == 0)
            {
                enabled = !IsAllActionsExecuted();
                Log("Update switched off");
            }

            Log("Leaved");
        }

        protected override void Init()
        {
            enteredCollider = new List<Collider>();
            enabled = false;
            countTriggered = 0;
        }

        public void Out(GameObject obj)
        {
            if (IsAllActionsExecuted()) return;

            if (IsState(ETriggerWorkMode.CHANNELING))
                Pause();
        }


        public override void End()
        {
            SetState(ETriggerStates.FINISHED);
        }
    }
}