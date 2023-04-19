using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieEvents 
{
    public delegate void OnHungerChanged(float value);
    public static OnHungerChanged onHungerChanged;

    public delegate void OnLifeChanged(float value);
    public static OnLifeChanged onLifeChanged;

    public delegate void OnAmmoChanged(int currentAmmo, int maxAmmo);
    public static OnAmmoChanged onAmmoChanged;

    public delegate void OnItemChanged(mainCharacter player);
    public static OnItemChanged onItemChanged;

    public delegate void OnTriggerItemEnter();
    public static OnTriggerItemEnter onTriggerItemEnter;

    public delegate void OnTriggerItemExit();
    public static OnTriggerItemExit onTriggerItemExit;

    public delegate void OnZombieSpawnedDied();
    public static OnZombieSpawnedDied onZombieSpawnedDied;

    public delegate void OnRecipeDone(int recipeDone);
    public static OnRecipeDone onRecipeDone;

}
