using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.SharedTrackers;
using OpenYandere.Characters;

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

        }
        Dictionary<Character, relation> peopleWhomIKnow;

        private void Start()
        {
            peopleWhomIKnow = new Dictionary<Character, relation>();
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
    }
}
