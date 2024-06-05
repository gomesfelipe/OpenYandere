using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.SharedMind;
using UnityEngine.Events;

namespace OpenYandere.Characters.Interactions.InteractableCompoents
{
    [RequireComponent(typeof(Animator))]
    public abstract class InteractableComponent : MonoBehaviour
    {
        public Options InteratableList;

        public UnityEvent<Dictionary<InteractableComponent, Options>> updateList;

        protected void Awake()
        {
            updateList.AddListener(UpdateRoomOptionList);
            InteratableList = new Options(new Dictionary<int, InteractableInfo>());
        }

      
        public void UpdateRoomOptionList(Dictionary<InteractableComponent, Options> list)
        {
            if (list.ContainsKey(this))
            {
                list[this] = InteratableList;
            }else
            {
                list.Add(this, InteratableList);
            }
            //Debug.Log("Updated");
        }

        
        
        public StartActivityTicket GetStartTicket(int activityIndex, Character thePerson)
        {
            StartActivityTicket tick = new(this.transform.position,activityIndex,thePerson);
            tick.Arrived.AddListener(UserArrived);
            tick.Canceled.AddListener(UserCancel);
            return tick;
        }
        
        public void UserArrived(Character user,int optionindex)
        {
            Debug.Log(user.characterName + " has arrived");
            InteratableList.value[optionindex].AddPeople(user);
        }

        public void UserCancel(Character user, int optionindex)
        {
            Debug.Log(user.characterName + " has Cancel!");
            InteratableList.value[optionindex].RemovePeople(user);
        }

        public class Options
        {
            public Dictionary<int, InteractableInfo> value;
            public Options(Dictionary<int, InteractableInfo> v)
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

            public void UpdateIncentives(Mind.Reaction data)
            {
                this.incentives = data;
            }
            public void AddPeople(Character person)
            {
              if (!peopleWhoAreUsing.Contains(person))
                {
                    peopleWhoAreUsing.Add(person);
                }
            }

            public void RemovePeople(Character person)
            {
                if (peopleWhoAreUsing.Contains(person))
                {
                    peopleWhoAreUsing.Remove(person);
                }
            }
        }
    }
}
