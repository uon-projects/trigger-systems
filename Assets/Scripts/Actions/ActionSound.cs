using UnityEngine;

namespace Trigger
{
    public class ActionSound : Action
    {
        public AudioSource audioSource;

        private void Start()
        {
            if (audioSource == null)
            {
                Debug.LogError("audio source not initiliazed");
                return;
            }

            enabled = false;
        }

        public override void Update()
        {
            if (!audioSource.isPlaying) End();
        }


        public override void End()
        {
            SetState(ETriggerStates.FINISHED);
        }

        public override void Run(GameObject obj)
        {
            Log("playing sound");
            audioSource.Play();
            SetState(ETriggerStates.RUNNING);
        }
    }
}