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
            IsPickable = true;
            ZombieEvents.onTriggerItemEnter();
            other.gameObject.GetComponent<mainCharacter>().CanInteract = true;
            other.gameObject.GetComponent<mainCharacter>().ItemInteractable = this;
            Debug.Log("Peut prendre");
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPickable = false;
            ZombieEvents.onTriggerItemExit();
            other.gameObject.GetComponent<mainCharacter>().CanInteract = false;
            other.gameObject.GetComponent<mainCharacter>().ItemInteractable = null;

            Debug.Log("Peut plus prendre");
        }
    }

    public virtual void PickUpItem()
    {
    }

    public void SetGameObject(GameObject go)
    {
        this._go = go;
    }

    public GameObject GetGameObject()
    {
        return _go;
    }

    

    

}
