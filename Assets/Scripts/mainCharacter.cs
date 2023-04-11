using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCharacter : LivingObject
{
    [SerializeField]    
    private float _movementSpeed;
    [SerializeField]
    private float _sensitivity; //sensibilité de la caméra
    private float _rotationCamera; // stocke la rotation de la caméra
    private float _maxHunger;
    private float _cuurentHunger;



    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float translation = Input.GetAxis("Vertical") * _movementSpeed * Time.deltaTime;
        float straffe = Input.GetAxis("Horizontal") * _movementSpeed * Time.deltaTime;
        transform.Translate(straffe, 0, translation);

        // Rotation de la caméra
        float mouseX = Input.GetAxis("Mouse X") * _sensitivity;
        _rotationCamera += mouseX;
        transform.localRotation = Quaternion.Euler(0, _rotationCamera, 0);
    }
}
