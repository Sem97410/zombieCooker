using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _mouseSensitivity;
    private float _cameraVerticalRotation;


    private Transform camTransform;



    public Transform Target { get => _target; set => _target = value; }
    public float MouseSensitivity { get => _mouseSensitivity; set => _mouseSensitivity = value; }
    public float CameraVerticalRotation { get => _cameraVerticalRotation; set => _cameraVerticalRotation = value; }

    private void Update()
    {
        MoveCamera();   
    }

    private void MoveCamera()
    {

        // Collect Mouse Input

        float inputX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        // Rotate the Camera around its local X axis

        _cameraVerticalRotation -= inputY;
        _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * _cameraVerticalRotation;


        // Rotate the Player Object and the Camera around its Y axis

        _target.Rotate(Vector3.up * inputX);
    }




}
