using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class mainCharacter : LivingObject
{
    [SerializeField]    
    private float _movementSpeed;

    private List<Weapon> _weapon;

    [SerializeField]
    private float _runSpeedMultiplication;      //cette variable est multiplié avec la vitesse pour calculer la vitesse de course

    private PickUp _itemInteracable;



    private float _maxHunger;
    private float _cuurentHunger;


    private bool _isRunning;
    private bool _canInteract;
    private bool _havePistol;

    public bool CanInteract { get => _canInteract; set => _canInteract = value; }
    public PickUp ItemInteracable { get => _itemInteracable; set => _itemInteracable = value; }
    public bool HavePistol { get => _havePistol; set => _havePistol = value; }
    public List<Weapon> Weapons { get => _weapon; set => _weapon = value; }

    private void Start()
    {
        //lock le cursor pour la caméra
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Weapons = new List<Weapon>();

    }
    private void Update()
    {
        Move();
        Run();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * _movementSpeed * Time.deltaTime; ;
        float verticalInput = Input.GetAxis("Vertical") * _movementSpeed * Time.deltaTime; ;
        transform.Translate(horizontalInput, 0, verticalInput);
        
    }

    private void Run()
    {
        if (Input.GetButton("Run"))
        {
            if (_isRunning == false)
            {
                _movementSpeed = _movementSpeed * _runSpeedMultiplication;
                     _isRunning = true;
            }
           
            
        }
        else
        {
            if (_isRunning == true)
            {
            _isRunning = false;

            _movementSpeed = _movementSpeed / _runSpeedMultiplication;
        }

            }
    }

    public void OnInteract(InputValue value)
    {
        if (_canInteract && ItemInteracable != null)
        {
            ItemInteracable.PickUpItem();
            CanInteract = false;
            ItemInteracable = null;
        }
        else return;
    }

    public void OnFire(InputValue value)
    {
        if (HavePistol)
        {
            
        }
    }


}
