using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : PickUp
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
        Debug.Log("+10 Ammo");
        Destroy(gameObject);
        
    }
    private void AddAmmo(Pistol pistol, int value)
    {
            pistol.CurrentAmmo += value;
            pistol.CurrentAmmo = Mathf.Clamp(pistol.CurrentAmmo, 0, pistol.MaxAmmo);
    }
}
