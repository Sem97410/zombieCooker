using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

public class OvenButton : MonoBehaviour
{
    [SerializeField]
    private Oven _oven;

    [SerializeField] private mainCharacter _player;
    [SerializeField] private gameManager _gameManager;

    private Animation _animation;


    private void Start()
    {
        _animation = GetComponent<Animation>();
    }


    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            _player.ButtonAction.performed += ValidRecipes;


        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            _player.ButtonAction.performed -= ValidRecipes;


        }
    }

    public void ValidRecipes(InputAction.CallbackContext ctx)
    {
        if (_oven.recipeIp == 30)
        {
          
            
            MakeRightIpPlate(_oven.Hamburger, _oven.SpawnHamburgerPosition);

        }
        if (_oven.recipeIp == 5005)
        {
            
            MakeRightIpPlate(_oven.Salade, _oven.SpawnSaladePosition);


        }
        if (_oven.recipeIp == 10)
        {
          
            MakeRightIpPlate(_oven.SoupeViande, _oven.SpawnSoupeViandePosition);


        }
        if (_oven.recipeIp == 2431)
        {
          
            MakeRightIpPlate(_oven.Poisson, _oven.SpawnPoissonPosition);
        }

        else
        {
            _oven.EjectIngredient();
            _animation.Play("ButtonPush");

        }

    }

    public void MakeRightIpPlate(GameObject recipe, Transform recipePosition)
    {
        _oven.MakeThePlate(recipe, recipePosition);
        _gameManager.WinCondition();
        ZombieEvents.onRecipeDone(_gameManager.NumberOfPlate, _gameManager.NumberOfPlateNeed);
     
        _animation.Play("ButtonPush");
    }



}
