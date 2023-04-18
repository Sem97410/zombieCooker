using UnityEngine;
using UnityEngine.InputSystem;

public class OvenButton : MonoBehaviour
{
    [SerializeField]
    private Oven _oven;

    [SerializeField] private mainCharacter _player;
    [SerializeField] private gameManager _gameManager;


    private void Start()
    {
        //_oven = GetComponentInParent<Oven>();
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
            _oven.MakeThePlate(_oven.Hamburger, _oven.SpawnHamburgerPosition);
            _gameManager.WinCondition();
        }
        if (_oven.recipeIp == 5005)
        {
            _oven.MakeThePlate(_oven.Salade, _oven.SpawnSaladePosition);
            _gameManager.WinCondition();

        }
        if (_oven.recipeIp == 10)
        {
            _oven.MakeThePlate(_oven.SoupeViande, _oven.SpawnSoupeViandePosition);
            _gameManager.WinCondition();

        }

        else
        {
            for (var i = 0; i < _oven.FoodOven.Count; i++)
            {
                Rigidbody rb = _oven.FoodOven[i].GetComponent<Rigidbody>();
                rb.AddForce(300 * transform.up);

            }
            


        }

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log(collision);
    //        if (RecipeIp == 30)
    //        {
    //            MakeThePlate(_hamburger, _spawnHamburgerPosition);
    //        }
    //        if (RecipeIp == 5005)
    //        {
    //            MakeThePlate(_salade, _spawnSaladePosition);
    //        }
    //        if (RecipeIp == 10)
    //        {
    //            MakeThePlate(_soupeViande, _spawnSoupeViandePosition);
    //        }
    //    }
    //}

}
