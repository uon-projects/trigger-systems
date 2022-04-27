using System;
using System.Collections.Generic;

namespace Trigger
{
    [Serializable]
    public class AllowedLayers
    {
        public List<int> list;

        public bool IsAllowed(int layer)
        {
            return IsEmpty() || list.Contains(layer);
        }


        public bool IsAllowed(int[] layers)
        {
            return false;
        }

        private bool IsEmpty()
        {
            return list is {Count: 0};
        }
    }
}