using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : PickUp
{
   private void AddAmmo(Pistol pistol, int value)
    {
        pistol.CurrentAmmo += value;
        pistol.CurrentAmmo = Mathf.Clamp(pistol.CurrentAmmo, 0, pistol.MaxAmmo);
    }
}
