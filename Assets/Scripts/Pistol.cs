using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Pistol : Weapon, IShooting
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
            MainCharacter.HavePistol = true;
            ZombieEvents.onAmmoChanged(CurrentAmmo,MaxAmmo);
            Debug.Log("A pris le pistol");

        }
    }

    public void Attack(IDamageable attaquant, IDamageable cible)
    {
        cible.TakeDamage(Damage, attaquant);
    }

    private void Reload()
    {

    }

    
    



}
