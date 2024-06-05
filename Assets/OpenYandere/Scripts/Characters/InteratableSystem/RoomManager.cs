using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Managers.Traits;

namespace OpenYandere.Characters.Interactions.InteractableCompoents
{
    internal class RoomManager : Singleton<RoomManager>
    {
        public List<InteractableComponent> interactList;
        public Dictionary<InteractableComponent, InteractableComponent.Options> RoomInteractables;
        // Start is called before the first frame update
        void Start()
        {
            RoomInteractables = new Dictionary<InteractableComponent, InteractableComponent.Options>();
           var allInteract=(InteractableComponent[]) FindObjectsByType(typeof(InteractableComponent), FindObjectsSortMode.None);
            interactList = new List<InteractableComponent>( allInteract) ;
            updateAllInteractList();
        }

        // Update is called once per frame
        void Update()
        {
            updateAllInteractList();
        }

        public Dictionary<InteractableComponent, InteractableComponent.Options> getRoomInteractables()
        {
            return this.RoomInteractables;
        }
         public void  updateAllInteractList()
        {
            foreach ( InteractableComponent i in interactList)
            {
                i.updateList.Invoke(RoomInteractables);  
            }
        }
         
    }
}
