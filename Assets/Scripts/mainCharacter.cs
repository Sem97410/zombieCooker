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

    [SerializeField]
    private float _currentHunger;

    
    private bool _canInteract;
    private bool _havePistol;
    [SerializeField]
    private float _hungerDecrease;   //la vitesse a laquelle on perd de la faim

    public bool CanInteract { get => _canInteract; set => _canInteract = value; }
    public PickUp ItemInteracable { get => _itemInteracable; set => _itemInteracable = value; }
    public bool HavePistol { get => _havePistol; set => _havePistol = value; }
    public List<Weapon> Weapons { get => _weapon; set => _weapon = value; }
    [SerializeField]
    private float _hungerDecreaseRun; // la perte de faim quand on court

    private bool _isRunning;

    [SerializeField]
    private Fx _walkFx;
    private void Start()
    {
        //lock le cursor pour la caméra
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Weapons = new List<Weapon>();

        _currentLife = 100;



        StartCoroutine("DecreaseHunger");

    }
    private void Update()
    {
        Move();

    }

    //Crash après l'utilisation de la touche maj si  _runSpeedMultiplication == 0
    //TODO : Rajouter un test pour eviter que ça plante
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * _movementSpeed * Time.deltaTime; ;
        float verticalInput = Input.GetAxis("Vertical") * _movementSpeed * Time.deltaTime; ;
        transform.Translate(horizontalInput, 0, verticalInput);
    }

    public void Run()  //la méthode est appelé grâce aux event de l'input player
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

    public void OnInteract()
    {
        if (_canInteract && ItemInteracable != null)
        {
            ItemInteracable.PickUpItem();
            CanInteract = false;
            ItemInteracable = null;
        }
        else return;
    }

    public void OnFire()
    {
        if (HavePistol)
        {
            
        }
    }


}
