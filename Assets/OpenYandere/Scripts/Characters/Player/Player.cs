using UnityEngine;
using System;
using System.Collections.Generic;
using OpenYandere.Managers;

namespace OpenYandere.Characters.Player
{
    public class Player : Character
    {
        
        [Range(-100, 100)] public int Reputation;
        public int attackDamage = 5;

        private LayerMask DefaultMask;
        private EquipmentManager EM;
        private void Awake()
        {
            base.Awake();
            EM = EquipmentManager.Instance;
            DefaultMask = LayerMask.GetMask("Default");
        }
        void Update()
        {
            
            if (EM.GetWeapon() != null && Input.GetMouseButtonDown(0)) //0 = left mouse button
            {
                Debug.Log("Attack");
                Attack();
            }
            // Exemplo: Desequipar o item atualmente equipado como arma ao pressionar a tecla Q
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (EM.GetWeapon() == null && InventorySystem.Instance.GetItems().Count > 0)
                {
                    EM.Equip(InventorySystem.Instance.GetItems()[0]);
                }
                else
                {
                    EM.Unequip(EquipmentManager.Instance.GetWeapon());
                }
            }


            }
        void Attack()
        {
            EM.GetWeapon().Use(this);
          
        }
    }

}