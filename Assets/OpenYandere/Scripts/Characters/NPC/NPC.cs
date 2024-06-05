using OpenYandere.Characters.Player;
using OpenYandere.Managers;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;
using OpenYandere.Characters.SharedTrackers;
using OpenYandere.Characters.Sense;
using System;
using OpenYandere.Characters.Interactions.InteractableCompoents;

namespace OpenYandere.Characters.NPC
{

	[RequireComponent(typeof(NPCMovement))]
	public abstract class NPC : Character
	{
        // i think a State machine is needed to keep logic clean,heres some ideas
        //1.NPCFreeTimeState <- Ai finds things to do
        //2.NPCLessonState <-go to class and handle class stuff
        //3.NPCCustomEventState <- for special events like Fire alarm or school hall meetings
        public NPCMovement NPCMovement => _npcMovement;
        [SerializeField] protected NPCMovement _npcMovement;
        
        public float detectionDistance = 10.0f, dangerDistance = 5.0f;
        public bool isPlayerDetected = false, isInDanger = false;
        public float fieldOfViewAngle = 120.0f; 
        public LayerMask viewMask;

        [Header("Activity/Task")]
        public Routine dailyRoutine;
        public Routine RequestOrEmergenRoutine;

        public List<Character> people;

        private int currentActivityIndex = 0;


        private void Awake()
        {
            base.Awake();
            _npcMovement=GetComponent<NPCMovement>();
            
            

           // RequestOrEmergenRoutine = new Routine();

        }
        protected void Start()
        {
            if (ClockSystem.Instance != null) { ClockSystem.Instance.OnTimeChanged += CheckActivity; }
            if (dailyRoutine.activities.Count > 0)
            {
                dailyRoutine.activities[0].OnActivityStart(this);
            }
            if (RequestOrEmergenRoutine.activities.Count > 0)
            {
                RequestOrEmergenRoutine.activities[0].Reset();
            }
        }
        void FixedUpdate()
        {
            
            DetectPlayer();
            CheckRequest();
            FindInteractable();
            if (isInDanger)
            {
                _npcMovement.FleeFromPlayer();
                //StartCoroutine(LookAtWeaponAndReact());
            }
        }

       private void FindInteractable()
        {
            InteractableComponent targetobject;
            foreach(KeyValuePair<InteractableComponent,InteractableComponent.Options> v in RoomManager.Instance.getRoomInteractables())
            {
                foreach(KeyValuePair<int,InteractableComponent.InteractableInfo> info in v.Value.value)
                {
                     if(mind.checkIncentives(info.Value.incentives))
                    {
                        Debug.Log(characterName + " wants " + info.Value.infoName);
                    }
                }
            }// NoteToMe: current checking system have a couple of issue
            // 1.It cant really pick highest priorities
            // 2. it should return a startactivityTicket
        }

        
        public void AddRequest(ActivityBase ab) { this.RequestOrEmergenRoutine.activities.Add(ab); }
        private void CheckRequest()
        {
           // Debug.Log("request");
            if (RequestOrEmergenRoutine.activities.Count == 0) { return; }
            ActivityBase currentRequest = RequestOrEmergenRoutine.activities[0];
          
            if (!currentRequest.started) currentRequest.OnActivityStart(this);
            currentRequest.DoActivity(this);

            if(currentRequest.finished)
            {
                currentRequest.OnActivityEnd(this);
                RequestOrEmergenRoutine.activities.Remove(currentRequest);
            }
        }
       

        private void CheckActivity()// this only update by the clock delegate...
        {   
            if (RequestOrEmergenRoutine.activities.Count > 0) { return; }// only do daily routine when no request or emergency

            if ((currentActivityIndex >= dailyRoutine.activities.Count) || dailyRoutine.activities.Count == 0) return;

            ActivityBase currentActivity = dailyRoutine.activities[currentActivityIndex];
            int currentTime = ClockSystem.Instance.GetTimeMilitary();

            if (currentTime >= currentActivity.startTimeMilitary && currentTime <= currentActivity.endTimeMilitary)
            {
                currentActivity.DoActivity(this);
            }
            else if (currentTime > currentActivity.endTimeMilitary)
            {
                currentActivity.OnActivityEnd(this);
                currentActivityIndex++;
                if (currentActivityIndex < dailyRoutine.activities.Count)
                {
                    dailyRoutine.activities[currentActivityIndex].OnActivityStart(this);
                    CheckActivity();
                }
            }
        }

        protected void OnDestroy()
        {
            if (ClockSystem.Instance != null)
            {
                ClockSystem.Instance.OnTimeChanged -= CheckActivity;
            }
            
        }

        private void DetectPlayer()
        {
            ViewSenses vs =(ViewSenses) mind.getSenses<ViewSenses>();
            if (vs!=null)
            {
                people=vs.Isaw();
            }
            /*
            Vector3 directionToPlayer = player.transform.position - this.transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;
            float angleBetweenNPCAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleBetweenNPCAndPlayer < fieldOfViewAngle * 0.5f)
            {
                if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer.normalized, out RaycastHit hit, detectionDistance, viewMask))
                {
                    if (hit.collider.gameObject == player)
                    {
                        isPlayerDetected = true;
                        isInDanger = distanceToPlayer < dangerDistance && GameManager.Instance.equipmentManager.GetWeapon() != null;
                    }
                    else
                    {
                        isPlayerDetected = false;
                        isInDanger = false;
                    }
                }
            }
            else
            {
                isPlayerDetected = false;
                isInDanger = false;
            } */
        }

        IEnumerator LookAtWeaponAndReact()
        {
            // Defina a posição do IK Target para a arma do jogador.
            Vector3 weaponPosition = EquipmentManager.Instance.GetWeaponSlot().position; // Supondo que você tenha um método que retorne a posição da arma.
            headIKTarget.position = weaponPosition;

            // Espere um momento para o NPC reconhecer.
            yield return new WaitForSeconds(1.5f);

            // Reação (por exemplo, recuar ou levantar as mãos).
            animator.SetTrigger("ReactToWeapon"); // Supondo que você tenha uma animação de reação.

            yield return new WaitForSeconds(1f); // Espere a reação terminar.

            // Inicie a fuga.
            _npcMovement.FleeFromPlayer();
        }

    }
}
