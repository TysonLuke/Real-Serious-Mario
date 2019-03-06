using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSM
{
    namespace WorldEntity
    {
        public abstract class Platform : MonoBehaviour
        {
            protected Vector3 origin;

            private void Awake()
            {
                origin = transform.position;
            }

            public virtual void Enable()
            {
                // Return to origin location.
                transform.position = origin;
                CancelInvoke();
            }
            
            protected void Disable()
            {
                transform.position = new Vector3(-500, -500, -500);
                RSM.Levels.LevelController.Instance.DisableObject(this);
            }

        }
    }
}
