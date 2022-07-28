using UnityEngine;
using UnityEngine.Pool;

namespace Ebla.Pooling
{
    public abstract class Pool<TObject> : MonoBehaviour
        where TObject : BaseBehaviour<TObject>
    {
        private Transform Transform
        {
            get
            {
                if (myTransform == null)
                {
                    myTransform = transform;
                }

                return myTransform;
            }
        }

        private Transform myTransform;

        private IObjectPool<TObject> pool;

        [SerializeField] private TObject prefab;
        [SerializeField] private bool collectionCheck;
        [SerializeField] private int defaultCapacity = 5;
        [SerializeField] private int maxSize = 50;

        public TObject Get()
        {
            return pool.Get();
        }

        private void Awake()
        {
            pool = new ObjectPool<TObject>(
                CreateObject,
                OnTakeObjectFromPool,
                OnObjectReturnedToPool,
                OnDestroyObject,
                collectionCheck,
                defaultCapacity,
                maxSize);
        }

        private TObject CreateObject()
        {
            var obj = Instantiate(prefab, transform);
            Debug.Log("Pool Create Obj Scale: " + obj.transform.localScale);
            obj.gameObject.SetActive(false);
            obj.OnReleaseObject += HandleReleaseObject;
            return obj;
        }

        private void OnTakeObjectFromPool(TObject obj)
        {
            obj.gameObject.SetActive(true);
        }

        private void OnObjectReturnedToPool(TObject obj)
        {
            obj.ResetObject();
        }

        private static void OnDestroyObject(TObject obj)
        {
            Destroy(obj.gameObject);
        }

        private void HandleReleaseObject(TObject obj)
        {
            if (obj == null)
            {
                return;
            }

            obj.Transform.SetParent(Transform);
            pool.Release(obj);
            obj.gameObject.SetActive(false);
        }
    }
}