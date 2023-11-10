using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters;

namespace OpenYandere.Characters.Sense
{

    [RequireComponent(typeof(Character))]
    public class Senses : MonoBehaviour
    {
        [SerializeField]  public bool sensesActive;
        protected Character owner;
        protected bool init;
        void Start()
        {

        }

        // Update is called once per frame
        protected void Update()
        {
            if (!init)
            {
                Debug.LogWarning("Tracker not init!!");
                return;
            }
        }

        public void Initialize(Character c)
        {
            init = true;
            owner = c;
        }
        public void ShowDebugInfo() { }
    }
}
