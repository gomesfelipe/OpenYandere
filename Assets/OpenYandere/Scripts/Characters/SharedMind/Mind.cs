using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.Sense; // change the senses to Shared to unify naming !! 
using OpenYandere.Characters.SharedTrackers;
using OpenYandere.Characters;
using System;
using System.Linq;

namespace OpenYandere.Characters.SharedMind
{
    [RequireComponent(typeof(Character)),
        DisallowMultipleComponent]
    public class Mind : MonoBehaviour
    {
        // the mind is in charge of managing the trackers and accepting Mind.Reaction class objct
        // and apply the changes
        // maybe we also add senses? but for now, nope

        public List<Tracker> allTracker;
        public List<Senses> allSenses;

        private Character owner;

        private bool _init = false;

        void Awake()
        {
            owner = GetComponent<Character>();
            allTracker = new List<Tracker>(GetComponents<Tracker>());
            allSenses = new List<Senses>(GetComponents<Senses>());

           // Debug.Log(owner.characterName);
        }

        // Update is called once per frame
        void Update()
        {

        }
        #region Adding Trackers and Senses
        public Tracker getTracker<T>()
        {
            foreach (Tracker track in allTracker)
            {
                if (track is T){return track;}
            }
            throw new Exception("no tracker " + typeof(T).Name + " was found on " + owner.characterName);
        }
        public bool addTracker(Tracker t){ allTracker.Add(t); return true;}

        public Senses getSenses<T>()
        {
            foreach (Senses s in allSenses)
            {
                if (s is T){return s;}
            }
            throw new Exception("no Senses " + typeof(T).Name + " was found on " + owner.characterName);
        }

        public bool addSenses(Senses t) {allSenses.Add(t); return true;}

        #endregion
        public void applyReaction(Reaction r)
        {
            EmotionTracker emo= allTracker.OfType<EmotionTracker>().FirstOrDefault();
            emo.updateEmotion(r.emotionReaction);

            RelationshipTracker tracker = allTracker.OfType<RelationshipTracker>().FirstOrDefault();
            tracker.updateRelationship(r.relationReaction);
        }


        public class Reaction
        {
            public EmotionTracker.emotionData emotionReaction;
            public RelationshipTracker.relationshipData relationReaction;

            public Reaction(EmotionTracker.emotionData er,
                RelationshipTracker.relationshipData rr)
            {
                this.emotionReaction = er;
                this.relationReaction = rr;
            }
        }
    }
}
