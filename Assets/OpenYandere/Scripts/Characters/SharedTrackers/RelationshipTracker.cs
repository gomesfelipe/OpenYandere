using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.SharedTrackers;
using OpenYandere.Characters;
using OpenYandere.Characters.NPC.Prefabs;
using System;

namespace OpenYandere.Characters.SharedTrackers
{
    public class RelationshipTracker : Tracker
    {
        public struct relation
        {
          //  public int known;
          //  public int preference;
          
            public enum relationKey
            {
                known,
                preference,
            }
            public Dictionary<relationKey, int> value;
            public int max;
            public int min;
            public relation(Dictionary<relationKey, int> v, int ma = 0, int mi = 0)
            {
                value = v;
                max = ma;
                min = mi;
            }
            public static relation operator +(relation a, relation b)
            {
                foreach (KeyValuePair<relationKey, int> k in b.value)
                {
                    a.value[k.Key] = Math.Clamp(a.value[k.Key] + k.Value, a.min, a.max);
                }
                return new relation(a.value);
            }
            public string getAllRelation() { return "known: " + value[relationKey.known] + 
                    " preference: " + value[relationKey.preference]; }
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
        public void addRelation(Character c, relation r)
        {
            peopleWhomIKnow.data.Add(c, r);
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
            foreach (KeyValuePair<Character, relation> c in data.data)
            {
                peopleWhomIKnow.data[c.Key] += data.data[c.Key];
            }        
        }
        public relationshipData getAllRelation() { return peopleWhomIKnow; }

        public class relationshipData
        {
            public Dictionary<Character, relation> data;
            public relationshipData(Dictionary<Character, relation> d) { data = d; }
        }
    }
}
