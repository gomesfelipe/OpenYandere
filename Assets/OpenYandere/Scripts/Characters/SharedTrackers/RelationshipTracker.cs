using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.SharedTrackers;
using OpenYandere.Characters;
using OpenYandere.Characters.NPC.Prefabs;
using System;
using System.Linq;

namespace OpenYandere.Characters.SharedTrackers
{
    public class RelationshipTracker : Tracker
    {
      
        public class relation: IComparable<relation>
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

            public int CompareTo(relation other)
            {
                int mytotalScore=0;
                int otherTotalScore = 0;

                otherTotalScore = (int)(other.value[relation.relationKey.known] + other.value[relation.relationKey.preference] * 0.5);
                mytotalScore = (int)(other.value[relation.relationKey.known] + other.value[relation.relationKey.preference] * 0.5);
                
                if (mytotalScore>otherTotalScore)
                {
                    return 1;
                } else if (mytotalScore < otherTotalScore)
                {
                    return -1; 
                } else
                {
                    return 0;
                }
            }
        }
        [SerializeField]public List<PrefabID> friendID;
        private relationshipData peopleWhomIKnow;
        
        private void Start()
        {
            peopleWhomIKnow = new relationshipData( new Dictionary<PrefabID, relation>());
            foreach(PrefabID p in friendID)
            {
                peopleWhomIKnow.data.Add(p, new relation(new Dictionary<relation.relationKey, int>()
                {
                    {relation.relationKey.known,100 },
                      {relation.relationKey.preference,100 }
                }));
            }
            //peopleWhomIKnow.Add(new Student(), new relation(55, 60));
        }

        private void Update()
        {
            
        }
        public void addRelation(PrefabID c, relation r)
        {
            peopleWhomIKnow.data.Add(c, r);
        }
        public relation getRelation(PrefabID c)
        {
            relation r;
            if (peopleWhomIKnow.data.TryGetValue(c, out r))
            {
                return r;
            }
            else
            {               
                return new relation(new Dictionary<relation.relationKey, int>());// reuturns a default value
            }
        }
        public void updateRelationship(relationshipData data)
        {
            foreach (KeyValuePair<PrefabID, relation> c in data.data)
            {
                peopleWhomIKnow.data[c.Key] += data.data[c.Key];
            }        
        }
        public relationshipData getAllRelation() { return peopleWhomIKnow; }
        public PrefabID[] getTopFriends(int topcount)
        {
            PrefabID[] list = peopleWhomIKnow.data.OrderByDescending(kv => kv.Value).Take(topcount)
                .Select(kv=>kv.Key).ToArray();
            return list;
        }

        public class relationshipData
        {
            public Dictionary<PrefabID, relation> data;
            
            public relationshipData(Dictionary<PrefabID, relation> d) { data = d; }
        }
    }
}
