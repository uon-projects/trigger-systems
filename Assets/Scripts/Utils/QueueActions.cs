using System;

namespace Trigger
{
    [Serializable]
    public class QueueActions
    {
        public Action[] actions;

        private int index;

        public QueueActions()
        {
            Init();
        }


        private void Init()
        {
            index = -1;
        }


        public Action Next()
        {
            index++;

            return Current();
        }


        public void Recharge()
        {
            for (var i = 0; i < actions.Length; i++) actions[i].SetState(ETriggerStates.NOT_ACTIVATED);

            Init();
        }


        public Action[] GetActions()
        {
            return actions;
        }


        public bool IsAllActionsExecuted()
        {
            for (var i = 0; i < actions.Length; i++)
                if (!actions[i].IsState(ETriggerStates.FINISHED))
                    return false;

            return true;
        }

        public Action Current()
        {
            if (actions == null || index < 0 || index >= actions.Length) return null;

            return actions[index];
        }
    }
}