using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oven : MonoBehaviour
{
    [SerializeField]
    private RecipeManager _recipeManager;
    public static List<Food> ingredients = new List<Food>();

    private List<GameObject> _foodOven = new List<GameObject>();
    private List<Food> _foodIdOven = new List<Food>();

    private Food _food;

    private int RecipeIp = 1;



    [SerializeField]
    private GameObject _hamburger;
    [SerializeField]
    private Transform _spawnPlatePosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            RecipeIp = 1;
            _food = other.GetComponent<Food>();
            _foodOven.Add(other.gameObject);
            _foodIdOven.Add(_food);
            for (var i = 0; i < _foodIdOven.Count; i++)
            {
                RecipeIp *= _foodIdOven[i].Id;

            }



            if (RecipeIp == 30)
            {
                for (var i = 0; i < _foodOven.Count; i++)
                {
                    Destroy(_foodOven[i]);


                }
                _foodOven.Clear();
                Debug.Log("coucou");
                Instantiate(_hamburger, _spawnPlatePosition.position, transform.rotation);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            _food = other.GetComponent<Food>();
            _foodOven.Remove(other.gameObject);
            _foodIdOven.Remove(_food);

            RecipeIp /= _food.Id;
          Debug.Log(RecipeIp);
        }
    }




}
