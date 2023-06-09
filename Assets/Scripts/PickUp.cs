using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    private mainCharacter _mainCharacter;
    private bool _isPickable;
    private GameObject _go;

    [SerializeField] private Sprite _sprite;
    [SerializeField] private UiManager uiManager;


    public bool IsPickable { get => _isPickable; set => _isPickable = value; }
    public mainCharacter MainCharacter { get => _mainCharacter; set => _mainCharacter = value; }
    public UiManager UiManager { get => uiManager; set => uiManager = value; }
    public Sprite Sprite { get => _sprite; set => _sprite = value; }

    protected virtual void Start()
    {
        _mainCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<mainCharacter>();
        UiManager = GameObject.FindGameObjectWithTag("UiManager").GetComponent<UiManager>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IfPlayerCanPickUpItem(other, true);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IfPlayerCanPickUpItem(other, false);
        }
    }

    public void IfPlayerCanPickUpItem(Collider other, bool valueIsPickable)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPickable = valueIsPickable;
            if (IsPickable)
            {
                ZombieEvents.onTriggerItemEnter();
                other.gameObject.GetComponent<mainCharacter>().CanInteract = valueIsPickable;
                other.gameObject.GetComponent<mainCharacter>().ItemInteractable = this;
            }
            else
            {
                ZombieEvents.onTriggerItemExit();
                other.gameObject.GetComponent<mainCharacter>().CanInteract = valueIsPickable;
                other.gameObject.GetComponent<mainCharacter>().ItemInteractable = this;
            }
        }
    }

    public virtual void PickUpItem()
    {
        if (MainCharacter.PickUps.Count >= MainCharacter.MaxSpaceInInventory)
        {
            Debug.Log("Inventaire plein");
            return;
        }
        else
        {
            SetGameObject(this.gameObject);
            MainCharacter.PickUps.Add(this);
            MainCharacter.EnleverItemEquipe(MainCharacter.GetItemSelected().GetGameObject());
            MainCharacter.ChoixIndex = MainCharacter.PickUps.Count - 1;
            MainCharacter.GetItemSelected().GetComponent<Rigidbody>().isKinematic = true;
            IgnoreCollisionWithPlayer();
            MainCharacter.AfficherItemEquipe(MainCharacter.GetItemSelected().GetGameObject());
            MainCharacter.GetItemSelected().GetComponent<SphereCollider>().enabled = false;
            UiManager.UpdateSpriteOfInventory(MainCharacter);
        }
    }

    public void SetGameObject(GameObject go)
    {
        this._go = go;
    }

    public GameObject GetGameObject()
    {
        return _go;
    }

    public void IgnoreCollisionWithPlayer()
    {
        BoxCollider meshCollider = MainCharacter.GetItemSelected().GetComponentInChildren<BoxCollider>();
        CapsuleCollider capsule = MainCharacter.GetComponent<CapsuleCollider>();
        Physics.IgnoreCollision(meshCollider, capsule);
    }

    

    

}
