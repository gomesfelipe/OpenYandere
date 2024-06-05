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
       
        void Awake()
        {
            owner = GetComponent<Character>();
        }

        // Update is called once per frame
        protected void Update()
        {
           
        }
        
        public void ShowDebugInfo() { }
    }
}
