using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OpenYandere.Characters.NPC
{
    public abstract class MultiActHost : ActivityBase
    {
        [Range(0, 10)]
        public int maxCompacity;
        [Range(0,10)]
        public int minCompacity;
        public Dictionary<NPC,MultiActJoiner> participents;
        
        

        public float ActivityRad { get; internal set; }

        public abstract void findJoiner();
        public abstract void searchTarget();
        public abstract void invite(NPC npc);
        
    }
}
