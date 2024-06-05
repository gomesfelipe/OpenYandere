using System.Collections.Generic;
using UnityEngine;
namespace OpenYandere.Characters.NPC
{
    [System.Serializable]
    public abstract class ActivityBase : ScriptableObject
    {
        public int startTimeMilitary;
        public int endTimeMilitary;

        public int activityPriority;
        public NPC owner;
        //Events

        public delegate void EndActivity(NPC n);
        public event EndActivity endActivity;

        public bool finished { get; set; }
        public bool started { get; set; }
        public EndActivity getEndActivityDelegate(){ return endActivity; }

        public void Reset()
        {
            started = false;
            finished = false;
        }
        //public abastract void GetActivityAnimation();
        public abstract void OnActivityStart(NPC person);

        public abstract void DoActivity(NPC person);

        public abstract void OnActivityEnd(NPC person);
    }

    [System.Serializable]
    public class Routine
    {
        public List<ActivityBase> activities = new();
    }
    /*
     Single person activity
    1. Chat Activity was assign to Student
    2. Student go to target Student 
    3. if target student is doing high priority action, then Student will give up and discard their
    own activity
    4.  if student is avaiable to talk, then the student will recieve a talking act
    5.  the student who init the talk will randomly pick a topic from global convo OR pull one memory
    from their tracker and tell them
    6. the student who heard it will react to memory ( it affact emotiontracker, relationship tracker)
    7. after some amount of time, the action will end and they will resume their other acts

     */
}
