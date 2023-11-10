using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.SharedTrackers;
using OpenYandere.Characters;
using OpenYandere.Characters.NPC.Prefabs;

namespace OpenYandere.Characters.SharedTrackers
{
    public class RelationshipTracker : Tracker
    {
        public struct relation
        {
            public int known;
            public int preference;
            

            public relation(int k = 0, int p = 50)
            {
                known = k;
                preference = p;
            }
            public string getAllRelation() { return "known: " + known + " preference: " + preference; }
        }
        Dictionary<Character, relation> peopleWhomIKnow;

        private void Start()
        {
            peopleWhomIKnow = new Dictionary<Character, relation>();

            //peopleWhomIKnow.Add(new Student(), new relation(55, 60));
        }

        private void Update()
        {
            
        }
        public void addRelation(Character c, int k, int p)
        {
            peopleWhomIKnow.Add(c, new relation(k, p));
        }
        public relation getRelation(Character c)
        {
            relation r;
            if (peopleWhomIKnow.TryGetValue(c, out r))
            {
                return r;
            }
            else
            {
                
                return new relation();// reuturns a default value
            }
        }
        public Dictionary<Character, relation> getAllRelation() { return peopleWhomIKnow; }
    }
}
