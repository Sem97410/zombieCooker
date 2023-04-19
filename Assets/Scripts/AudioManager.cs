using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip _eatFood;

    public AudioClip EatFood { get => _eatFood; set => _eatFood = value; }

    private void OnEnable()
    {
        ZombieEvents.onFoodEaten += EatFoodPlay;
    }

    private void OnDisable()
    {
        ZombieEvents.onFoodEaten -= EatFoodPlay;
    }
    public void EatFoodPlay(AudioSource audioSource)
    {
        audioSource.clip = EatFood;
        audioSource.Play();
    }
}
