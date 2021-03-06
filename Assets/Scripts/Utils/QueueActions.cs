using System;
using System.Linq;

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
            foreach (var action in actions)
                action.SetState(ETriggerStates.NOT_ACTIVATED);

            Init();
        }

        public Action[] GetActions()
        {
            return actions;
        }

        public bool IsAllActionsExecuted()
        {
            return actions.All(t => t.IsState(ETriggerStates.FINISHED));
        }

        public Action Current()
        {
            if (actions == null || index < 0 || index >= actions.Length) return null;

            return actions[index];
        }
    }
}