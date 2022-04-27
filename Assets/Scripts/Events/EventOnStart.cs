namespace Trigger
{
    public class EventOnStart : Event
    {
        public override void Update()
        {
            Run(gameObject);
            End();
        }

        protected override void Init()
        {
            enabled = true;
            countTriggered = 0;
        }

        public override void End()
        {
            SetState(ETriggerStates.FINISHED);
            Log("OnStart.executed");
        }
    }
}