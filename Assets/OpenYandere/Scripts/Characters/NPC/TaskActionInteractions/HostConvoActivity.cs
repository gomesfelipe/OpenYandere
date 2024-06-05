using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.SharedTrackers;

namespace OpenYandere.Characters.NPC
{
    [CreateAssetMenu(fileName = "HostConvo", menuName = "NPC/MultiAct/Talk/HostConvo", order = 2)]
    public class HostConvoActivity : MultiActHost
    {
        public NPC target;
        public override void DoActivity(NPC person)
        {
            
            if (target==null)
             {
                searchTarget();
            }
            if ( target != null&&Vector3.Distance(target.gameObject.transform.position, person.gameObject.transform.position) > ActivityRad )
            {
                findJoiner();

            }
          

            if (target != null && !base.participents.ContainsKey(target) && Vector3.Distance(target.gameObject.transform.position, person.gameObject.transform.position) < ActivityRad)
            {
                invite(target);
            }
            
            if(participents.Count>=minCompacity)
            {
                person.animator.Play("Talk2");
                person.gameObject.transform.LookAt(target.transform.position);
            }
        }


        public override void searchTarget()
        {
            // searches the top 3 friends and chat
            RelationshipTracker rt = (RelationshipTracker)owner.mind.getTracker<RelationshipTracker>();
            if (rt != null)
            {
                PrefabID[] friends = rt.getTopFriends(3);
                if (friends.Length == 0)
                {
                    Debug.Log(owner.characterName + " has no friends....");
                    return;
                }
                target = (NPC)GlobalDataStorage.Prefab_stuff.IDtoCharacter(friends[0]);


            }
        }
        public override void findJoiner()
        {
            Vector3 pos = target.gameObject.transform.position+(owner.transform.position - target.gameObject.transform.position).normalized * (ActivityRad*0.9f);
            owner.NPCMovement.NavigationAgent.SetDestination(pos);//target.gameObject.transform.position);
        }
        public override void invite(NPC npcTarget)
        {
            JoinerConvoActivity j = CreateInstance<JoinerConvoActivity>();
           npcTarget.RequestOrEmergenRoutine.activities.Add(j);

            participents.Add(npcTarget,j);
            j.joinHost(this);
        }
        public override void OnActivityEnd(NPC person)
        {
            finished = true;
        }

        public override void OnActivityStart(NPC person)
        {
            started = true;
            owner = person;
            Init(1f);
            searchTarget();
           
        }

        public void Init(float rad)
        {
            base.ActivityRad=rad;
            base.participents = new Dictionary<NPC, MultiActJoiner>();
        }
        
    }
}
