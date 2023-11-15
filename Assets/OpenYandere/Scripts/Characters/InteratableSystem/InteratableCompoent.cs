using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.SharedMind;
using UnityEngine.Events;

namespace OpenYandere.Characters.Interactions.InteractableCompoents
{
    [RequireComponent(typeof(Animator))]
    public abstract class InteratableCompoent : MonoBehaviour
    {
        public options InteratableList;

        public UnityEvent<Dictionary<InteratableCompoent, options>> updateList;

        protected void Awake()
        {
            updateList.AddListener(updateRoomOptionList);
            InteratableList = new options(new Dictionary<int, InteractableInfo>());
        }

      
        public void updateRoomOptionList(Dictionary<InteratableCompoent, options> list)
        {
            if (list.ContainsKey(this))
            {
                list[this] = InteratableList;
            }else
            {
                list.Add(this, InteratableList);
            }
            Debug.Log("Updated");
        }

        
        
        public StartActivityTicket getStartTicket(int activityIndex, Character thePerson)
        {
            StartActivityTicket tick = new StartActivityTicket(this.transform.position,activityIndex,thePerson);
            tick.Arrived.AddListener(userArrived);
            tick.Canceled.AddListener(userCancel);
            return tick;
        }
        
        public void userArrived(Character user,int optionindex)
        {
            Debug.Log(user.characterName + " has arrived");
            InteratableList.value[optionindex].addPeople(user);
        }

        public void userCancel(Character user, int optionindex)
        {
            Debug.Log(user.characterName + " has Cancel!");
            InteratableList.value[optionindex].removePeople(user);
        }

        public class options
        {
            public Dictionary<int, InteractableInfo> value;
            public options(Dictionary<int, InteractableInfo> v)
            {
                this.value = v;
            }
        }
        public class InteractableInfo
        {
            public string infoName { get; private set; }
            public Mind.Reaction incentives { get; private set; }
            public int maxNumberOfParticipents { get; private set; }
            public List<Character> peopleWhoAreUsing { get; private set; }

            public InteractableInfo(string name,Mind.Reaction incentives, int max)
            {
                this.infoName = name;
                this.incentives = incentives;
                this.maxNumberOfParticipents = max;

            }

            public void updateIncentives(Mind.Reaction data)
            {
                this.incentives = data;
            }
            public void addPeople(Character person)
            {
              if (!peopleWhoAreUsing.Contains(person))
                {
                    peopleWhoAreUsing.Add(person);
                }
            }

            public void removePeople(Character person)
            {
                if (peopleWhoAreUsing.Contains(person))
                {
                    peopleWhoAreUsing.Remove(person);
                }
            }
        }
    }
}
