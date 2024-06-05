using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.NPC;

namespace OpenYandere.Characters.NPC
{

    [CreateAssetMenu(fileName = "TrackingTargetActivity", menuName = "NPC/LomotionActivity/TrackingTargetActivity")]
    [System.Serializable]
    public class TrackingTargetActivity : ActivityBase
    {
        public GameObject Tracktarget;
        public int TrackInterval=3;
        public float Distance = 5;

        private Vector3 _ActualTarget;
        private int timer=0;

        private void Awake()
        {
            endActivity += OnActivityEnd;
        }
        public override void DoActivity(NPC person)
        {
            timer++;
            if (timer>TrackInterval)
            {
                Debug.Log("tracking...");
                float dist = Vector3.Distance(Tracktarget.transform.position, _ActualTarget);
               if ( dist > Distance)
               {
                    Vector3 circOuterSide = ( Tracktarget.transform.position + Vector3.Normalize( _ActualTarget-Tracktarget.transform.position )*2 );
                    _ActualTarget = circOuterSide;

                    person.NPCMovement.NavigationAgent.SetDestination(_ActualTarget);
                    

                }
                timer = 0;        
            }            
        }

        public override void OnActivityEnd(NPC person)
        {
            Debug.Log(person.characterName + " will stop following you");
            person.NPCMovement.NavigationAgent.SetDestination(person.transform.position);
            finished = true;
        }

        public override void OnActivityStart(NPC person)
        {
            Debug.Log("START");
            _ActualTarget = person.gameObject.transform.position;
            Tracktarget = person.player;
           // person.NPCMovement.NavigationAgent.SetDestination(Tracktarget.position);
            started = true;
        }


    }
}
