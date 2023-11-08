using UnityEngine;
using OpenYandere.Characters.Player;
using OpenYandere.Managers.Traits;
namespace OpenYandere.Managers
{
    internal class CameraManager : Singleton<CameraManager>
    {
        [Header("References:")]
        public PlayerCamera PlayerCamera;

        public void lockCamera()
        {
            PlayerCamera.setLocked(true);
        }

        public void lockCameraToParrael()

        {

        }
        public void unlockCamera()
        {
            PlayerCamera.setLocked(false);
        }
    }
}