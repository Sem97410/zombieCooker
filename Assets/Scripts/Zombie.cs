using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : LivingObject
{
    private float _speed;

    private int _damage;

    private Transform _target;

    public Transform Target { get => _target; set => _target = value; }

    public void SetTarget(Transform target)
    {
        Target = target;
    }
}
