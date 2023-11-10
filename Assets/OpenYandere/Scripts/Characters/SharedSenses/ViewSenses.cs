using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.Sense;

namespace OpenYandere.Characters.Sense
{
    public class ViewSenses : Senses
    {
        public enum viewSenseType
        {
            cone,
            cylinder,

        }

        [SerializeField] public viewSenseType senseType;

        [SerializeField] public SensesFilter filter;
        [SerializeField] public float radius;
        [SerializeField] public float MaxSenseRange;
        [SerializeField] public float MinSenseRange;
        [SerializeField] public float currentSenseRange;

        [SerializeField] public int rayCount=1;
        [SerializeField] public LayerMask viewMask;
        [SerializeField] [Range(0, 1)] public float _alert;

        public List<Character> thingsIsee;
        public float alert { get { return _alert; } set { _alert = Mathf.Clamp(value, 0, 1); } }


        void Start()
        {
            filter = new SensesFilter(SensesFilter.Filter.friends);
            thingsIsee = new List<Character>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            base.Update();

            List<Character> c = new List<Character>();
            for (int i = 0; i < rayCount; i++)
            {

                Vector3 directionToPlayer = owner.player.transform.position - this.transform.position;

                float distanceToPlayer = directionToPlayer.magnitude;
                float angleBetweenNPCAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);

                if (angleBetweenNPCAndPlayer < radius * 0.5f)
                {
                    if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer.normalized, out RaycastHit hit, currentSenseRange, viewMask))
                    {
                        if (hit.collider.gameObject.TryGetComponent(out Character thing) == owner.player)
                        {
                            // isPlayerDetected = true;
                            //  isInDanger = distanceToPlayer < dangerDistance && GameManager.Instance.equipmentManager.GetWeapon() != null;
                            c.Add(thing);
                        }
                        else
                        {
                            //  isPlayerDetected = false;
                            //  isInDanger = false;
                        }
                    }
                }
                else
                {
                    // isPlayerDetected = false;
                    //  isInDanger = false;
                }
            }
            thingsIsee = c;
        }

        public  List<Character> Isaw() { return thingsIsee; }

    }
}
