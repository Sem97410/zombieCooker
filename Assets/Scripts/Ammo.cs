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
        
        if (MainCharacter.HavePistol == true)
        {
            if (MainCharacter.GetItemSelected() is Pistol)
            {
                Debug.Log("Reload");
                MainCharacter.WeaponAnimator.SetTrigger("Reload");
                Pistol pistol = MainCharacter.GetItemSelected().GetComponent<Pistol>();
                pistol.CurrentAmmo += 10;
                pistol.CurrentAmmo = Mathf.Clamp(pistol.CurrentAmmo, 0, pistol.MaxAmmo);
                ZombieEvents.onAmmoChanged(pistol.CurrentAmmo, pistol.MaxAmmo);
                Destroy(gameObject);
            }
            
        }
        else
        {
            Debug.Log("Take the pistol");
        }


    }
    private void AddAmmo(Pistol pistol, int value)
    {
            pistol.CurrentAmmo += value;
            pistol.CurrentAmmo = Mathf.Clamp(pistol.CurrentAmmo, 0, pistol.MaxAmmo);
    }
}
