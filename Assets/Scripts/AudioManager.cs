using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip _eatFood;
    [SerializeField] AudioClip _shoot;

    public AudioClip EatFood { get => _eatFood; set => _eatFood = value; }
    public AudioClip Shoot { get => _shoot; set => _shoot = value; }

    private void OnEnable()
    {
        ZombieEvents.onFoodEaten += EatFoodPlay;
        ZombieEvents.onShoot += ShootPlay;
    }

    private void OnDisable()
    {
        ZombieEvents.onFoodEaten -= EatFoodPlay;
        ZombieEvents.onShoot -= ShootPlay;

    }
    public void EatFoodPlay(AudioSource audioSource)
    {
        audioSource.clip = EatFood;
        audioSource.Play();
    }

    public void ShootPlay(AudioSource audioSouce)
    {
        audioSouce.clip = Shoot;
        audioSouce.Play();
    }
}
