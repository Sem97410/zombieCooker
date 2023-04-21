using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip _eatFood;
    [SerializeField] AudioClip _shoot;
    [SerializeField] AudioClip _endGameSound;
    [SerializeField] AudioClip _reloadGunSound;
    [SerializeField] AudioClip _zombieHitSound;

    public AudioClip EatFood { get => _eatFood; set => _eatFood = value; }
    public AudioClip Shoot { get => _shoot; set => _shoot = value; }
    public AudioClip EndGameSound { get => _endGameSound; set => _endGameSound = value; }
    public AudioClip ReloadGunSound { get => _reloadGunSound; set => _reloadGunSound = value; }
    public AudioClip ZombieHitSound { get => _zombieHitSound; set => _zombieHitSound = value; }

    private void OnEnable()
    {
        ZombieEvents.onFoodEaten += EatFoodPlay;
        ZombieEvents.onShoot += ShootPlay;

        ZombieEvents.onReload += ReloadPlay;
        ZombieEvents.onZombieHit += ZombieHitPlay;


    }

    private void OnDisable()
    {
        ZombieEvents.onFoodEaten -= EatFoodPlay;
        ZombieEvents.onShoot -= ShootPlay;

        ZombieEvents.onReload -= ReloadPlay;
        ZombieEvents.onZombieHit -= ZombieHitPlay;


    }
    public void EatFoodPlay(AudioSource audioSource)
    {
        audioSource.clip = EatFood;
        audioSource.Play();
    }

    public void ShootPlay(AudioSource audioSource)
    {
        audioSource.clip = Shoot;
        float randomPitch = Random.Range(0.8f, 1.2f);
        audioSource.pitch = randomPitch;
        audioSource.Play();
    }

    public void EndGamePlay(bool value, AudioSource audioSource)
    {
        audioSource.clip = EndGameSound;
        audioSource.Play();
    }

    public void ReloadPlay(AudioSource audioSource)
    {
        audioSource.clip = ReloadGunSound;
        audioSource.Play();
    }

    public void ZombieHitPlay(AudioSource audioSource)
    {
        audioSource.clip = ZombieHitSound;
        audioSource.Play();
    }
}
