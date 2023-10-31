﻿using System.Collections.Generic;
using UnityEngine;
namespace OpenYandere.Characters.NPC
{
    [System.Serializable]
    public abstract class ActivityBase: ScriptableObject
    {
        public int startTimeMilitary;
        public int endTimeMilitary;

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