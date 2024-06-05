using UnityEngine;
using OpenYandere.Characters.SharedTrackers;
using OpenYandere.Characters.NPC.Prefabs;
using System;

namespace OpenYandere.Characters
{
    internal struct AnimatorData
    {
        public Vector3 MoveDirection;
        public bool IsRunning;
    }
    
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Character))]
    internal class CharacterAnimator : MonoBehaviour
    {
        private AnimatorData _animatorData;
        
        [Header("References:")]
        [SerializeField] private Animator _animator;

        EmotionTracker emotionTracker;
        int attack;
        private void Start()
        {
            try
            {   
                
                //emotionTracker = (EmotionTracker)GetComponent<Student>().getTracker<EmotionTracker>();
            }catch (Exception e)
            {
                //Debug.LogWarning(e.Message);
            }
           //  Debug.Log(emotionTracker.getEmotion(EmotionTracker.emotion.joy));

           //attack = Animator.StringToHash("Attack1");
        }

        private void LateUpdate()
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (_animator.speed == 0f) return;
            
            _animator.SetFloat("Horizontal", _animatorData.MoveDirection.x);
            _animator.SetFloat("Vertical", _animatorData.MoveDirection.z);
            _animator.SetBool("Running", _animatorData.IsRunning);

           // _animator.Play(attack);
        }
        
        public void UpdateData(AnimatorData animatorData)
        {
            _animatorData = animatorData;
        }
        
        public void Resume()
        {
            _animator.speed = 1f;
        }

        public void Pause()
        {
            _animator.speed = 0f;
        }
    }
}