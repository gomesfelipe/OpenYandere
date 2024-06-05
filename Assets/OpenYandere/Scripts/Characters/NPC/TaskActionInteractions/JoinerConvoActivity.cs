using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OpenYandere.Characters.NPC
{
    [CreateAssetMenu(fileName = "JoinerConvo", menuName = "NPC/MultiAct/Talk/JoinerConvo", order = 1)]
    public class JoinerConvoActivity :MultiActJoiner
{
        public override void DoActivity(NPC person)
        {
            person.animator.Play("Talk1");
            person.gameObject.transform.LookAt(this.host.owner.transform.position);
        }

        public override void joinHost(MultiActHost host)
        {
            this.host = host;
        }

        public override void OnActivityEnd(NPC person)
        {
            finished = true;
        }

        public override void OnActivityStart(NPC person)
        {
            started = true;
        }

      
    }
}
