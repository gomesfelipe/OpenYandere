using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace OpenYandere.Characters.Interactions.InteractableCompoents
{
    
    public class StartActivityTicket : MonoBehaviour
    {
        public int optionIndex;
        public InteractableComponent Caller;
        public Character requester;
        public Vector3 StartingPosition;

        public UnityEvent<Character,int> Arrived { get; set; }
        public UnityEvent<Character, int> Canceled { get; set; }
        public StartActivityTicket(Vector3 pos,int index,Character requester)
        {
            this.StartingPosition = pos;
            optionIndex = index;
            this.requester = requester;
        }

        public void invokeArrived()
        {
            Arrived?.Invoke(requester, this.optionIndex);
        }

        public void invokeCancel()
        {
            Arrived?.Invoke(requester, this.optionIndex);
        }
    }
}