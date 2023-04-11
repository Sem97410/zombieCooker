using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : PickUp
{
    [SerializeField] private float _satiety;

    public float Satiety { get => _satiety; set => _satiety = value; }
}
