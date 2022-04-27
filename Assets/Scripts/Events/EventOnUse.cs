using System.Collections.Generic;
using UnityEngine;

namespace Trigger
{
    public class EventOnUse : Event
    {
        [SerializeField] public AllowedLayers layers;

        private readonly List<EventOnUseTransmitter> transmitters = new List<EventOnUseTransmitter>();

        public override void Update()
        {
            if (IsState(ETriggerStates.RUNNING) || IsState(ETriggerStates.PAUSE))
            {
                if (IsState(ETriggerWorkMode.FIRE))
                {
                    if (queue.Current() == null ||
                        queue.Current().IsState(ETriggerStates.FINISHED))
                        PopAction(whoTriggered);
                }
                else if (IsState(ETriggerWorkMode.CHANNELING))
                {
                    if (IsState(ETriggerRunMode.ASYNCHRONOUSLY))
                    {
                        if (!UpdateAsync()) Continue();
                    }
                    else
                    {
                        if (!UpdateSync()) Continue();
                    }
                }

                if (IsAllActionsExecuted()) End();
            }
        }


        protected override void Init()
        {
            enabled = false;
            countTriggered = 0;
        }

        public void AddTransmitter(EventOnUseTransmitter transmitter)
        {
            transmitters.Add(transmitter);
            Log("Registered new transmitter " + transmitter);
        }


        public void Signal(GameObject whoTriggered)
        {
            if (!IsState(ETriggerStates.FINISHED))
            {
                if (IsAllTransmitter()) Run(whoTriggered);
            }
            else
            {
                var subject = whoTriggered.GetComponent<SimpleController>();
                subject.Used();

                Log("It's used");
            }

            enabled = true;
        }


        public override void End()
        {
            Log("Used");
            whoTriggered.BroadcastMessage("Used");
            SetState(ETriggerStates.FINISHED);
        }

        private void Continue()
        {
            var isUsing = IsAllTransmitter();

            if (isUsing)
            {
                if (IsState(ETriggerStates.PAUSE)) Continue(whoTriggered);
            }
            else
            {
                Pause();
            }
        }

        private bool IsAllTransmitter()
        {
            var ret = true;
            for (var i = 0; i < transmitters.Count; i++)
            {
                ret = transmitters[i].IsUsing();
                if (!ret) break;
            }

            return ret;
        }
    }
}