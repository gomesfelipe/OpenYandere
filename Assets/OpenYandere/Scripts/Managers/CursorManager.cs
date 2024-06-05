using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Managers.Traits;

namespace OpenYandere.Managers
{
    internal class CursorManager : Singleton<CursorManager>
    {
        private bool _locked;
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void lockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _locked = true;
            Debug.Log("CursorManager:locking Mouse");
        }

        public void unlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            _locked = false;
            Debug.Log("CursorManager:unlocking Mouse");
        }

        public bool getCursorState() { return _locked; }
    }
}
