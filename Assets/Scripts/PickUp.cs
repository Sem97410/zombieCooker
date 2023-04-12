using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private mainCharacter _mainCharacter;
    private bool _isPickable;

    public bool IsPickable { get => _isPickable; set => _isPickable = value; }
    public mainCharacter MainCharacter { get => _mainCharacter; set => _mainCharacter = value; }

    private void Start()
    {
        _mainCharacter = GameObject.Find("Player").GetComponent<mainCharacter>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPickable = true;
            other.gameObject.GetComponent<mainCharacter>().CanInteract = true;
            other.gameObject.GetComponent<mainCharacter>().ItemInteracable = this;
            Debug.Log("Peut prendre");
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPickable = false;
            Debug.Log("Peut plus prendre");
        }
    }

    public virtual void PickUpItem()
    {

    }

}
