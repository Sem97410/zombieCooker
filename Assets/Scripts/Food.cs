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
        if (MainCharacter.PickUps.Count >= MainCharacter.MaxSpaceInInventory)
        {
            Debug.Log("Inventaire plein");
            return;
        }
        else
        {
            ZombieEvents.onTriggerItemExit();
            SetGameObject(this.gameObject);
            MainCharacter.PickUps.Add((PickUp)this);
            MainCharacter.ChooseItem(MainCharacter.PickUps.Count - 1);
            MainCharacter.GetItemSelected().GetComponent<Rigidbody>().isKinematic = true;
            MainCharacter.GetItemSelected().GetComponent<SphereCollider>().enabled = false;
            this.GetComponentInChildren<BoxCollider>().enabled = false;
            UiManager.UpdateSpriteOfInventory(MainCharacter);
            BoxCollider _box = GetComponentInChildren<BoxCollider>();
            CapsuleCollider _capsule = MainCharacter.GetComponent<CapsuleCollider>();
            Physics.IgnoreCollision(_box, _capsule);

        }

    }
}
