using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCharacter : LivingObject
{
    [SerializeField]    
    private float _movementSpeed;

    [SerializeField]
    private float _runSpeedMultiplication;      //cette variable est multipli� avec la vitesse pour calculer la vitesse de course
   

    private float _maxHunger;
    private float _cuurentHunger;


    private bool _isRunning;


    private void Start()
    {
        //lock le cursor pour la cam�ra
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        

    }
    private void Update()
    {
        Move();
        Run();
    }

    //Crash apr�s l'utilisation de la touche maj si  _runSpeedMultiplication == 0
    //TODO : Rajouter un test pour eviter que �a plante
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


    
}
