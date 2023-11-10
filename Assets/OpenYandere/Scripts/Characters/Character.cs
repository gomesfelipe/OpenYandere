using OpenYandere.Characters.Interactions.Prefabs.NPC;
using UnityEngine;
using OpenYandere.Characters.Player;
using OpenYandere.Managers;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;

using UnityEngine.AI;
using OpenYandere.Characters.SharedTrackers;
using OpenYandere.Characters.Sense;
using System;


namespace OpenYandere.Characters
{
    [System.Serializable, RequireComponent(typeof(CharacterAnimator), typeof(RagdollEnabler))]
    public abstract class Character : MonoBehaviour, IDamageable
    {
        public GameObject player;

        protected Animator animator;
        public int Id;
        public string characterName;
        public int maxHealth = 100;
        public int health;

        [Range(-100, 100)] public int trustLevel;
        public Sprite faceSprite;
        public bool IsAlive = true,canTakeDamage=true;
        public enum GenderType { Male, Female }
        public GenderType Gender;
        public Transform headIKTarget;

        public RagdollEnabler ragdollEnabler;

        [Header("Trackers (they auto add while playing)")]
        public List<Tracker> ListOfTracker;
        [Header("Senses (they auto add while playing)")]
        public List<Senses> ListOfSenses;

        protected void Awake()
        {
            // Suponhamos que o personagem tenha um Animator.
            animator = GetComponent<Animator>();
            ragdollEnabler = GetComponent<RagdollEnabler>();
            health = maxHealth;

            ListOfTracker = new List<Tracker>(GetComponents<Tracker>());
            ListOfSenses = new List<Senses>(GetComponents<Senses>());

            foreach (Tracker t in ListOfTracker)
            {
                t.Initialize(this);
            }

            foreach (Senses t in ListOfSenses)
            {
                t.Initialize(this);
            }
        }
        protected void Start()
        {
          
            if (health <= 0)
            {
                Die();
            }
        }

        public Tracker getTracker<T>()
        {
            foreach (Tracker track in ListOfTracker)
            {
                if (track is T)
                {
                    return track;
                }
            }

            throw new Exception("no tracker " + typeof(T).Name + " was found on " + characterName);
        }

        public bool addTracker(Tracker t)
        {
            ListOfTracker.Add(t);
            return true;
        }

        public Senses getSenses<T>()
        {
            foreach (Senses s in ListOfSenses)
            {
                if (s is T)
                {
                    return s;
                }
            }

            throw new Exception("no Senses " + typeof(T).Name + " was found on " + characterName);
        }

        public bool addSenses(Senses t)
        {
            ListOfSenses.Add(t);
            return true;
        }

        public void TakeDamage(int damageAmount)
        {
            health -= damageAmount;
            Debug.Log($"{characterName} has {health} health remaining.");
            if (health <= 0)
            {
                Die();
            }
        }
        protected void Die()
        {
            Debug.Log(characterName + " has died!");
            IsAlive = false;           
            ragdollEnabler.EnableRagdoll();
            //animator.SetTrigger("Die");

        }


    }
}

