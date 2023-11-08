using System.Collections.Generic;
using UnityEngine;
namespace OpenYandere.Characters.NPC
{
    [System.Serializable]
    public abstract class ActivityBase : ScriptableObject
    {
        public int startTimeMilitary;
        public int endTimeMilitary;


        //Events
        public delegate void EndActivity(NPC n);
        public event EndActivity endActivity;

        public bool finished { get; set; }
        public bool started { get; set; }
        public EndActivity getEndActivityDelegate(){ return endActivity; }
        public abstract void OnActivityStart(NPC person);

        public abstract void DoActivity(NPC person);

        public abstract void OnActivityEnd(NPC person);
    }

    [System.Serializable]
    public class Routine
    {
        public List<ActivityBase> activities = new();
    }
}
