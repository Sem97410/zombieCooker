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
        if (MainCharacter.GetItemSelected() is Pistol)
        {
            Pistol pistol = MainCharacter.GetItemSelected().GetComponent<Pistol>();
            if (pistol.CurrentAmmo == pistol.MaxAmmo) return;
            MainCharacter.WeaponAnimator.SetTrigger("Reload");
            ZombieEvents.onReload(MainCharacter.PlayerAudioSource);
            ZombieEvents.onTriggerItemExit();
            pistol.CurrentAmmo += 10;
            pistol.CurrentAmmo = Mathf.Clamp(pistol.CurrentAmmo, 0, pistol.MaxAmmo);
            ZombieEvents.onAmmoChanged(pistol.CurrentAmmo, pistol.MaxAmmo);
            Destroy(gameObject);
        }
    }
    private void AddAmmo(Pistol pistol, int value)
    {
            pistol.CurrentAmmo += value;
            pistol.CurrentAmmo = Mathf.Clamp(pistol.CurrentAmmo, 0, pistol.MaxAmmo);
    }
}
