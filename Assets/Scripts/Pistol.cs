using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Pistol : Weapon, IShooting
{
    public Transform _firePoint;

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
        MainCharacter.HavePistol = true;
        Debug.Log("A pris le pistol");
    }
    public void Shoot(IDamageable tireur, IDamageable cible)
    {
        cible.TakeDamage(Damage, tireur);
    }

    private void Reload()
    {

    }

    
    



}
