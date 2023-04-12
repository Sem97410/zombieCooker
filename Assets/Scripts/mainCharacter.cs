using System.Collections;
using UnityEngine;

public class mainCharacter : LivingObject
{
    [SerializeField]
    private float _movementSpeed;

    [SerializeField]
    private float _runSpeedMultiplication;      //cette variable est multiplié avec la vitesse pour calculer la vitesse de course


    private float _maxHunger;

    [SerializeField]
    private float _currentHunger;

    [SerializeField]
    private float _hungerDecrease;   //la vitesse a laquelle on perd de la faim

    [SerializeField]
    private float _hungerDecreaseRun; // la perte de faim quand on court

    private bool _isRunning;


    private void Start()
    {
        //lock le cursor pour la caméra
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        StartCoroutine("DecreaseHunger");

    }
    private void Update()
    {
        Move();
        
    }

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



    IEnumerator  DecreaseHunger()
    {
        while (_currentHunger > 0)
        {

         yield return new WaitForSeconds(_hungerDecrease);
        _currentHunger -= 1;
        }
    }



}
