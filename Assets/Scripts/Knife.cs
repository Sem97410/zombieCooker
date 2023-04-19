using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon, IShooting
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    public override void PickUpItem()
    {
        base.PickUpItem();
        if (MainCharacter.PickUps.Count >= MainCharacter.MaxSpaceInInventory)
        {
            Debug.Log("Inventaire plein");
            return;
        }
        else
        {
            ZombieEvents.onTriggerItemExit();
            MainCharacter.HaveKnife = true;
        }
        
    }

    public void Attack(IDamageable attaquant, IDamageable cible)
    {
        cible.TakeDamage(Damage, attaquant);
    }
}
