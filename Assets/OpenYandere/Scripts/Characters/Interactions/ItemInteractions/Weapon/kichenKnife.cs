using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item/Weapon")]

public class weaponItem : ItemBase
{
    [Header("Weapon Information")]
    [SerializeField] private int Damage;
    [SerializeField] private float Range;
    [SerializeField] private LayerMask DefaultMask;

    private void Awake()
    {
        DefaultMask = LayerMask.GetMask("Default");
    }
    public override void Use(Character target)
    {
        base.Use(target);
        Debug.DrawRay(target.transform.position, target.transform.forward * 100, Color.red, 10f);
        if (Physics.Raycast(target.transform.position, target.transform.forward * Range, out RaycastHit hit, Mathf.Infinity, DefaultMask))
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.TryGetComponent<IDamageable>(out var damageableEntity))
            {
                damageableEntity.TakeDamage(Damage);
            }
        }
    }
    public override void Equip(Character target)
    {
        base.Equip(target);
        Debug.Log(base.ItemName + " is equiped");
    }

    private void playAnimation()
    {

    }
}
