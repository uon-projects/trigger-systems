using UnityEngine;

namespace Trigger
{
    public class ActionReplace : Action
    {
        [Header("Object Data")] public GameObject anObject;
        public GameObject beReplacedObject;

        public bool parent;

        private void Start()
        {
            enabled = false;
            if (anObject == null || beReplacedObject == null) Debug.LogError("Object(s) not initialized");
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
            Log(anObject.name + " replaced " + beReplacedObject);

            var createdObject = Instantiate(anObject, beReplacedObject.transform.position,
                beReplacedObject.transform.rotation,
                parent ? anObject.transform : null);

            Destroy(beReplacedObject);

            createdObject.SetActive(true);
            End();
        }
    }
}