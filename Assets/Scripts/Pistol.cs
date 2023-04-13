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
        base.PickUpItem();
        if (MainCharacter.PickUps.Count >= 5)
        {
            Debug.Log("Inventaire plein");
            return;
        }
        else
        {
            MainCharacter.HavePistol = true;
            Debug.Log("A pris le pistol");

        }
    }
    private void Shoot()
    {

    }

    private void Reload()
    {

    }

    
    



}
