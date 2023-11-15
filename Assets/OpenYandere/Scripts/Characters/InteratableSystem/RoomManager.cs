using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Managers.Traits;

namespace OpenYandere.Characters.Interactions.InteractableCompoents
{
    internal class RoomManager : Singleton<RoomManager>
    {
        public List<InteratableCompoent> interactList;
        public Dictionary<InteratableCompoent, InteratableCompoent.options> RoomInteractables;
        // Start is called before the first frame update
        void Start()
        {
            RoomInteractables = new Dictionary<InteratableCompoent, InteratableCompoent.options>();
           var allInteract=(InteratableCompoent[]) FindObjectsByType(typeof(InteratableCompoent), FindObjectsSortMode.None);
            interactList = new List<InteratableCompoent>( allInteract) ;
            updateAllInteractList();
        }

        // Update is called once per frame
        void Update()
        {
            updateAllInteractList();
        }

        public Dictionary<InteratableCompoent, InteratableCompoent.options> getRoomInteractables()
        {
            return this.RoomInteractables;
        }
         public void  updateAllInteractList()
        {
            foreach ( InteratableCompoent i in interactList)
            {
                i.updateList.Invoke(RoomInteractables);  
            }
        }
         
    }
}
