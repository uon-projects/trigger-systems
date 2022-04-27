using System;
using UnityEngine;

namespace Trigger
{
    public class ActionAnimation : Action
    {
        public GameObject gObject;

        [SerializeField] public AnimationBoolKeys[] animationParams;
        private Animator animator;

        private void Start()
        {
            if (gObject == null)
            {
                Debug.LogError("GameObject not initiliazed");
                return;
            }

            if (animationParams.Length == 0)
            {
                Debug.LogError("animationName not initiliazed");
                return;
            }

            animator = gObject.GetComponent<Animator>();
            enabled = false;
        }


        public override void Update()
        {
            End();
        }

        public override void Run(GameObject obj)
        {
            if (animator == null)
            {
                Debug.LogError("Can't find animator");
                Log("Can't find animator");
                return;
            }

            Log("Preps params for animation");

            for (var i = 0; i < animationParams.Length; i++)
                animator.SetBool(animationParams[i].name, animationParams[i].key);

            enabled = true;
        }

        public override void End()
        {
            SetState(ETriggerStates.FINISHED);
        }

        [Serializable]
        public struct AnimationBoolKeys
        {
            public string name;
            public bool key;
        }
    }
}