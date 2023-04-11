using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCharacter : LivingObject
{
    [SerializeField]    
    private float _movementSpeed;

   

    private float _maxHunger;
    private float _cuurentHunger;


    private void Start()
    {
        //lock le cursor pour la caméra
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

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



    
}
