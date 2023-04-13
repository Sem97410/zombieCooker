using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oven : MonoBehaviour
{
    [SerializeField]
    private RecipeManager _recipeManager;
    public static List<Food> ingredients = new List<Food>();
    private Button _hamburgerButton;
    private Canvas _canvas;
    private mainCharacter _mainCharacter;
    private CameraController _cameraControllerCharacter;


    private float ActualMovementSpeed;
    private float ActualMouseSensitivity;

    public void Start()
    {
        _canvas = GetComponentInChildren<Canvas>();
        _canvas.enabled = false;
        _mainCharacter = FindObjectOfType<mainCharacter>();
        _cameraControllerCharacter = _mainCharacter.GetComponentInChildren<CameraController>();



    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_canvas.enabled == false)
            {
             ActualMovementSpeed = _mainCharacter.MovementSpeed;
             ActualMouseSensitivity = _cameraControllerCharacter.MouseSensitivity;

            }
            Debug.Log("Dedans");

            if (Input.GetKey(KeyCode.E))
            {
                if (_canvas.enabled == false)
                {
                    _canvas.enabled = true;
                    _mainCharacter.MovementSpeed = 0;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;

                    _cameraControllerCharacter.MouseSensitivity = 0;
                    return;
                }
                else if (_canvas.enabled == true)
                {

                    _canvas.enabled = false;
                    _mainCharacter.MovementSpeed = ActualMovementSpeed;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.None;
                    _cameraControllerCharacter.MouseSensitivity = ActualMouseSensitivity;

                }

            }




        }
    }

    public void Hamburger()
    {

        if (_recipeManager.RecipeValid(ingredients) == null) return;


        if (_recipeManager.RecipeValid(ingredients).Name == "Hamburger")
        {
            Debug.Log("HAMBURGER");
           //Oven.ingredients.Remove(Food.);
        }
    }

}
