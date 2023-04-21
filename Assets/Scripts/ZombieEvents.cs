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

    public delegate void OnRecipeDone(int recipeDone, int recipeNeed);
    public static OnRecipeDone onRecipeDone;

    public delegate void OnFoodEaten(AudioSource audioSource);
    public static OnFoodEaten onFoodEaten;

    public delegate void OnShoot(AudioSource audioSource);
    public static OnShoot onShoot;

    public delegate void OnPlayerDeath(bool value);
    public static OnPlayerDeath onPlayerDeath;

    public delegate void OnTriggerButtonEnter();
    public static OnTriggerButtonEnter onTriggerButtonEnter;


    public delegate void OnReload(AudioSource audioSource);
    public static OnReload onReload;

    public delegate void OnPickUpAmmo(AudioSource audioSource);
    public static OnPickUpAmmo onPickUpAmmo;

    public delegate void OnZombieHit(AudioSource audioSource);
    public static OnZombieHit onZombieHit;
}
