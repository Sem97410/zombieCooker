using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
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
        MainCharacter.HavePistol = true;
        MainCharacter.Weapons.Add(this);
        Destroy(gameObject);
    }
    private void Shoot()
    {

    }

    private void Reload()
    {

    }



}
