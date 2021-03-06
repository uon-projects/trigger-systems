using UnityEngine;

namespace Trigger
{
    public class ActionToggleParticle : Action
    {
        public ParticleSystem particle;
        public bool pause;

        private void Start()
        {
            enabled = false;
            if (particle == null) Debug.LogError(name + ": particle not init");
        }

        public override void Update()
        {
            End();
        }

        public override void End()
        {
            SetState(ETriggerStates.FINISHED);
        }

        public override void Run(GameObject obj)
        {
            if (particle.isStopped || particle.isPaused)
            {
                particle.Play();
                Log("Particle play");
            }
            else if (!pause)
            {
                particle.Stop();
                Log("Particle stop");
            }
            else
            {
                particle.Pause();
                Log("Particle pause");
            }

            End();
        }
    }
}