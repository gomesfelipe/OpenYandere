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
            public static relation operator +(relation a, relation b)
            {
                int k = a.known + b.known;
                int p = a.preference + b.preference;
                return new relation(k,p);
            }
            public string getAllRelation() { return "known: " + known + " preference: " + preference; }
        }

        private relationshipData peopleWhomIKnow;

        private void Start()
        {
            peopleWhomIKnow = new relationshipData( new Dictionary<Character, relation>());

            //peopleWhomIKnow.Add(new Student(), new relation(55, 60));
        }

        private void Update()
        {
            
        }
        public void addRelation(Character c, int k, int p)
        {
            peopleWhomIKnow.data.Add(c, new relation(k, p));
        }
        public relation getRelation(Character c)
        {
            relation r;
            if (peopleWhomIKnow.data.TryGetValue(c, out r))
            {
                return r;
            }
            else
            {
                
                return new relation();// reuturns a default value
            }
        }
        public void updateRelationship(relationshipData data)
        {
            foreach (KeyValuePair<Character,relation> e in data.data)
            {
                peopleWhomIKnow.data[e.Key] += e.Value; //combine value
            }
        }
        public Dictionary<Character, relation> getAllRelation() { return peopleWhomIKnow.data; }

        public class relationshipData
        {
            public Dictionary<Character, relation> data;
            public relationshipData(Dictionary<Character, relation> d) { data = d; }
        }
    }
}
