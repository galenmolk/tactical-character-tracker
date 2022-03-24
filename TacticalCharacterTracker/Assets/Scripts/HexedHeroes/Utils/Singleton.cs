using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static bool _quitting = false;
        private static readonly object _lockObject = new object();

        [Tooltip("This will mark this GameObject to persist between scenes")]
        [SerializeField]
        private bool _persistent = true;

        public static T Instance
        {
            get
            {
                if (_quitting)
                {
                    Debug.LogWarningFormat("[{0}] Instance will not be returned because the application is quitting.", typeof(T));
                    return null;
                }
                lock (_lockObject)
                {
                    if (_instance == null)
                        FindAndSetInstance();

                    return _instance;
                }
            }
        }

        #region  Methods

        /// <summary>
        /// Meant to be overridden in the child classes as an init.
        /// </summary>
        protected virtual void OnAwake() { }

        protected virtual void OnApplicationQuit()
        {
            _quitting = true;
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Debug.LogWarningFormat("[{0}] Instance was already set and is not this, destroying this component", typeof(T));
                DestroySelf();
            }
            else
            {
                FindAndSetInstance();

                if (_persistent)
                    DontDestroyOnLoad(gameObject);

                OnAwake();
            }
        }

        private void DestroySelf()
        {
            if (Application.isEditor)
                DestroyImmediate(this);
            else
                Destroy(this);
        }

        private static void FindAndSetInstance()
        {
            if (_instance != null)
                return;

            else
            {
                T[] allInstances = FindObjectsOfType<T>();

                if (allInstances.Length > 0)
                {
                    if (allInstances.Length == 1)
                    {
                        _instance = allInstances[0];
                        return;
                    }
                    else
                    {
                        Debug.LogWarningFormat("[{0}] There should never be more than one Singleton of type {0} in the scene, but {1} were found. The first " +
                                               "instance found will be used, and all others will be destroyed.", typeof(T), allInstances.Length);

                        for (var i = 1; i < allInstances.Length; i++)
                        {
                            Destroy(allInstances[i]);
                        }
                        _instance = allInstances[0];
                        return;
                    }
                }

                // Should an instance be created here if one isn't found? 
                Debug.LogWarningFormat("[{0}] There were no instances of type {0} in the scene, mInstance is null", typeof(T));
            }
        }
        #endregion
    }