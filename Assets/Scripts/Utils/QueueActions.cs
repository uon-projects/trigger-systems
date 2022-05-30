using System;
using System.Collections.Generic;
using System.Linq;

namespace Trigger
{
    [Serializable]
    public class QueueActions
    {
        public Action[] actions;

        // private List<Action> _actions;
        // private SortedList<Action> _actions;
        // private LinkedListNode<Action> _actions;
        // private Collection<Action> _actions;
        // private Action[] _actions;
        private LinkedList<Action> _actions;

        private int _index;

        public QueueActions()
        {
            Init();
        }

        public void PrepareList()
        {
            _actions = new LinkedList<Action>(actions);
        }


        private void Init()
        {
            _index = -1;
        }


        public Action Next()
        {
            _index++;

            return Current();
        }


        public void Recharge()
        {
            foreach (var action in _actions)
                action.SetState(ETriggerStates.NOT_ACTIVATED);

            Init();
        }

        public LinkedList<Action> GetActions()
        {
            return _actions;
        }

        public bool IsAllActionsExecuted()
        {
            return _actions.All(t => t.IsState(ETriggerStates.FINISHED));
        }

        public Action Current()
        {
            if (_actions == null || _index < 0 || _index >= _actions.Count) return null;

            return actions.ElementAt(_index);
        }
    }
}