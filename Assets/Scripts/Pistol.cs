using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Pistol : Weapon, IShooting
{
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private Fx _muzzleFx;

    public Transform MuzzlePoint { get => _muzzlePoint; set => _muzzlePoint = value; }
    public Fx MuzzleFx { get => _muzzleFx; set => _muzzleFx = value; }

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
            ZombieEvents.onTriggerItemExit();
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
