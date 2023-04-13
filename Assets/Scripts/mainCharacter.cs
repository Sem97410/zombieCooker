using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class mainCharacter : LivingObject
{
    [SerializeField]
    private float _movementSpeed;

    private List<PickUp> _pickUp;

    [SerializeField]
    private float _runSpeedMultiplication;      //cette variable est multipli� avec la vitesse pour calculer la vitesse de course

    private PickUp _itemInteractable;

    private int _choixIndex;
    [SerializeField] private Transform _itemPos;


    private float _maxHunger;

    [SerializeField]
    private float _currentHunger;

    
    private bool _canInteract;
    private bool _havePistol;
    private bool _haveKnife;
    [SerializeField]
    private float _hungerDecrease;   //la vitesse a laquelle on perd de la faim

    public bool CanInteract { get => _canInteract; set => _canInteract = value; }
    public PickUp ItemInteractable { get => _itemInteractable; set => _itemInteractable = value; }
    public bool HavePistol { get => _havePistol; set => _havePistol = value; }
    public List<PickUp> PickUps { get => _pickUp; set => _pickUp = value; }
    public int ChoixIndex { get => _choixIndex; set => _choixIndex = value; }
    public Transform ItemPos { get => _itemPos; set => _itemPos = value; }
    public float MovementSpeed { get => _movementSpeed; set => _movementSpeed = value; }
    public bool HaveKnife { get => _haveKnife; set => _haveKnife = value; }

    [SerializeField]
    private float _hungerDecreaseRun; // la perte de faim quand on court

    private bool _isRunning;

    [SerializeField]
    private Fx _walkFx;
    private void Start()
    {
        //lock le cursor pour la cam�ra
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PickUps = new List<PickUp>();
        _currentLife = 100;

        ChoixIndex = 0;

        StartCoroutine("DecreaseHunger");

    }
    private void Update()
    {
        Move();
        ShowItemSelected();
    }

    //Crash apr�s l'utilisation de la touche maj si  _runSpeedMultiplication == 0
    //TODO : Rajouter un test pour eviter que �a plante
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * _movementSpeed * Time.deltaTime; ;
        float verticalInput = Input.GetAxis("Vertical") * _movementSpeed * Time.deltaTime; ;
        transform.Translate(horizontalInput, 0, verticalInput);
    }

    public void Run()  //la m�thode est appel� gr�ce aux event de l'input player
    {
        if (Input.GetButton("Run"))
        {
            if (_isRunning == false)
            {
                _movementSpeed = _movementSpeed * _runSpeedMultiplication;
                _hungerDecrease = _hungerDecrease / _hungerDecreaseRun;          //augmente la perte de faim quand on court
                _isRunning = true;
                //gameManager.AddFX(_walkFx, this.transform.position, transform.rotation);

            }
        }
        else
        {
            if (_isRunning == true)
            {
                _isRunning = false;

                _movementSpeed = _movementSpeed / _runSpeedMultiplication;
                _hungerDecrease = _hungerDecrease * _hungerDecreaseRun;
            }

        }
    }


    IEnumerator DecreaseHunger()
    {
        while (_currentHunger > 0)
        {

            yield return new WaitForSeconds(_hungerDecrease);
            _currentHunger -= 1;
        }
        while (_currentHunger == 0)
        {
                yield return new WaitForSeconds(1f);
                _currentLife -= 1;
        }
    }

    public void Interact()
    {
        if (_canInteract && ItemInteractable != null)
        {
            ItemInteractable.PickUpItem();
            CanInteract = false;
            ItemInteractable = null;
        }
        else return;
    }

    public void ShowItemSelected()
    {
        if (PickUps == null || GetItemSelected()== null) return;

        if (Input.GetButton("Item1"))
        {
            ChooseItem(0);
        }
        if (Input.GetButton("Item2"))
        {
            ChooseItem(1);
        }
        if (Input.GetButton("Item3"))
        {
            ChooseItem(2);
        }
        if (Input.GetButton("Item4"))
        {
            ChooseItem(3);
        }
        if (Input.GetButton("Item5"))
        {
            ChooseItem(4);
        }
    }

    public PickUp GetItemSelected()
    {
        if (PickUps != null && PickUps.Count > 0 && ChoixIndex < PickUps.Count)
            return PickUps[ChoixIndex];
        else return null;
    }

    public void AfficherItemEquipe(GameObject go)
    {
        if (go == null) return;
        go.SetActive(true);
        go.transform.parent = this.ItemPos;
        go.transform.localPosition = Vector3.zero;
        go.transform.rotation = ItemPos.rotation * Quaternion.Euler(0, 90, 0);
    }
    public void EnleverItemEquipe(GameObject go)
    {
        if (go == null) return;
        go.SetActive(false);
    }

    public void ChooseItem(int choixindex)
    {
        if (PickUps.Count < ChoixIndex) return;
        else
        {
            EnleverItemEquipe(GetItemSelected().GetGameObject());
            ChoixIndex = choixindex;
            AfficherItemEquipe(GetItemSelected().GetGameObject());
        }
    }

    
    public void Shoot()
    {
        if (GetItemSelected() is Pistol)
        {
            Ray ray = Camera.main.ScreenPointToRay(GetItemSelected().gameObject.GetComponent<Pistol>()._firePoint.position);

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Zombie"))
                {
                    GetItemSelected().gameObject.GetComponent<Pistol>().Shoot(this, hit.collider.GetComponent<IDamageable>());
                }
            }
            
        }
    }

}
