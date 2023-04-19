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
        if (MainCharacter.PickUps.Count >= 6)
        {
            Debug.Log("Inventaire plein");
            return;
        }
        else
        {
            MainCharacter.HaveKnife = true;
            Debug.Log("A pris le couteau");
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
               GetComponent<Knife>().Attack(MainCharacter, collider.GetComponent<IDamageable>());
            }
        }
    }
}
