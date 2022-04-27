namespace Trigger
{
    internal interface ITriggerMovementStates
    {
        void SetState(ETriggerMovementStates state);
        bool IsState(ETriggerMovementStates state);
    }
}