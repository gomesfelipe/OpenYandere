using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OpenYandere.Characters.NPC
{
    public abstract class MultiActJoiner : ActivityBase
    {
        public MultiActHost host;
      
        public abstract void joinHost(MultiActHost host);
    }
}
