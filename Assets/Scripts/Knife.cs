using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
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
        MainCharacter.HaveKnife = true;
        Debug.Log("A pris le couteau");
    }
    private void Attack()
    {

    }
}
