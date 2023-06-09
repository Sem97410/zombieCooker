using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : PickUp
{
    [SerializeField] private int _damage;
    [SerializeField] private float _fireRate;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private int _currentAmmo;
    [SerializeField] private float _range;

    public int CurrentAmmo { get => _currentAmmo; set => _currentAmmo = value; }
    public int MaxAmmo { get => _maxAmmo; set => _maxAmmo = value; }
    public int Damage { get => _damage; set => _damage = value; }
    public float FireRate { get => _fireRate; set => _fireRate = value; }
    public float Range { get => _range; set => _range = value; }


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
    protected override void Start()
    {
        base.Start();
        CurrentAmmo = MaxAmmo;

    }

    public override void PickUpItem()
    {
        base.PickUpItem();
    }
}
