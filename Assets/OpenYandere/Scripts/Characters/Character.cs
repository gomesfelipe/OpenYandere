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
using OpenYandere.Characters.SharedMind;
using System;


namespace OpenYandere.Characters
{
    [DisallowMultipleComponent]
    [System.Serializable, RequireComponent(typeof(CharacterAnimator), typeof(RagdollEnabler), typeof(Mind))]
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

        
        public Mind mind;
       

        protected void Awake()
        {
            // Suponhamos que o personagem tenha um Animator.
            animator = GetComponent<Animator>();
            ragdollEnabler = GetComponent<RagdollEnabler>();
            mind = GetComponent<Mind>();

            health = maxHealth;
      
        }
        protected void Start()
        {
            if (health <= 0)
            {
                Die();
            }
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

