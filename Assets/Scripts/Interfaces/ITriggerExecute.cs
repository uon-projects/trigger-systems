using UnityEngine;

namespace Trigger
{
    internal interface ITriggerExecute
    {
        void Run(GameObject whoTriggered);
        void Update();
        void End();

        void Pause();
        void Continue(GameObject whoContinue);
    }
}