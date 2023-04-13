using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : PickUp
{
    [SerializeField] private float _satiety;

    [SerializeField] private Oven _oven;

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
        if (MainCharacter.PickUps.Count >= 5)
        {
            Debug.Log("Inventaire plein");
            return;
        }
        else
        {
            SetGameObject(this.gameObject);
            MainCharacter.PickUps.Add((PickUp)this);
            MainCharacter.ChooseItem(MainCharacter.PickUps.Count - 1);
        }

    }
}
