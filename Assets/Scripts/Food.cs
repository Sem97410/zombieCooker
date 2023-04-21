using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : PickUp
{
    [SerializeField] private float _satiety;

    public float Satiety { get => _satiety; set => _satiety = value; }
    public int Id { get => _id; set => _id = value; }

    [SerializeField]
    private int _id;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
    public override void PickUpItem()
    {
        base.PickUpItem();
        ZombieEvents.onTriggerItemExit();

    }
}
