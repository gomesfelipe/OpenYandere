using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenYandere.Characters.SharedTrackers
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Character))]
    public class Tracker : MonoBehaviour
    {
        [SerializeField] public bool TrackerActive;
        protected Character owner;
        protected bool init;
        void Awake()
        {
            init = false;
        }

        // Update is called once per frame
        protected void Update()
        {
            if (!init) Debug.LogWarning("Tracker not init!!");
        }

        public void Initialize(Character c)
        {
            init = true;
            owner = c;
        }

        public void ShowDebugInfo() { }
    }
}
