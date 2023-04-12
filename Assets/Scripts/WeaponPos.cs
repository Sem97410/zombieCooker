using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPos : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    private void Update()
    {
        transform.localEulerAngles = _camera.localEulerAngles;
    }
}
