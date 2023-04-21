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
    public void OnAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Zombie"))
            {
                Knife knife = GetComponent<Knife>();
                knife.Attack(MainCharacter, collider.GetComponent<IDamageable>());
                MainCharacter.FxImpact(MainCharacter.BloodFx,knife.transform.position);
            }
        }
    }
}
