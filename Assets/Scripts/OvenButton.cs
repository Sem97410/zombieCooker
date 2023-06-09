using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

public class OvenButton : MonoBehaviour
{
    [SerializeField]
    private Oven _oven;

    [SerializeField] private mainCharacter _player;

    private Animation _animation;


    private void Start()
    {
        _animation = GetComponent<Animation>();
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<mainCharacter>();
            _player.ButtonAction.performed += ValidRecipes;
            ZombieEvents.onTriggerShowGrabIconEnter();


        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            _player.ButtonAction.performed -= ValidRecipes;
            ZombieEvents.onTriggerHideGrabIconExit();



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
        gameManager.Instance().WinCondition();
        ZombieEvents.onRecipeDone(gameManager.Instance().NumberOfPlate, gameManager.Instance().NumberOfPlateNeed);
     
        _animation.Play("ButtonPush");
    }



}
