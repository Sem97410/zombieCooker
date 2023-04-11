using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _fireRate;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private int _currentAmmo;
    [SerializeField] private float _range;

    public int CurrentAmmo { get => _currentAmmo; set => _currentAmmo = value; }
    public int MaxAmmo { get => _maxAmmo; set => _maxAmmo = value; }
}
