using System;
using UnityEngine;

namespace Ebla
{
    public abstract class BaseBehaviour<TBehaviour> : MonoBehaviour
        where TBehaviour : MonoBehaviour
    {
        public abstract event Action<TBehaviour> OnReleaseObject;

        public Transform Transform
        {
            get
            {
                if (myTransform == null)
                    myTransform = transform;

                return myTransform;
            }
        }

        private Transform myTransform;
        
        public abstract void ResetObject();

        public abstract void ReleaseObject();
    }
}
