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
        SetGameObject(this.gameObject);
        MainCharacter.ChoixIndex = MainCharacter.PickUps.Count;
        MainCharacter.PickUps.Add((PickUp)this);
        Debug.Log(MainCharacter.PickUps);
    }
    private void Shoot()
    {

    }

    private void Reload()
    {

    }

    
    



}
