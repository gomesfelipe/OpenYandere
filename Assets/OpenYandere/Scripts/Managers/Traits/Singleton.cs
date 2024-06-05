using UnityEngine;

namespace OpenYandere.Managers.Traits
{
    internal class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance; 
        private static readonly object Lock = new object();

        protected void Awake()
        {
            if (Instance!= this)
            {
                Debug.LogWarning(typeof(T).Name+" is duplicated! Please Remove!");
            }
        }
        public static T Instance
        {
            get
            {
                lock (Lock)
                {
                    if (_instance != null) return _instance;

                    _instance = (T)FindFirstObjectByType(typeof(T));

                    if (_instance != null) return _instance;
                    
                    Debug.LogErrorFormat("[Singleton]: Failed to find an instance of '{0}', please create an instance in the scene.",
                        typeof(T));
                    
                    return null;
                }
            }
        }
    }
}