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

    protected int RecipeIp = 1;

    



    [Header("GameObject des plats")]

    [SerializeField] protected GameObject _hamburger;
    [SerializeField] protected GameObject _salade;
    [SerializeField] protected GameObject _soupeViande;



    [Header("Position de spawn des plats")]

    [SerializeField] protected Transform _spawnHamburgerPosition;
    [SerializeField] protected Transform _spawnSaladePosition;
    [SerializeField] protected Transform _spawnSoupeViandePosition;






    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FoodMesh"))
        {
            RecipeIp = 1;
            _food = other.GetComponentInParent<Food>();
            _foodOven.Add(other.transform.parent.gameObject);
            _foodIdOven.Add(_food);
            for (var i = 0; i < _foodIdOven.Count; i++)
            {
                RecipeIp *= _foodIdOven[i].Id;

            }


            if (RecipeIp == 30)
            {
                MakeThePlate(_hamburger, _spawnHamburgerPosition);
            }
            if (RecipeIp == 5005)
            {
                MakeThePlate(_salade, _spawnSaladePosition);
            }
            if (RecipeIp == 10)
            {
                MakeThePlate(_soupeViande, _spawnSoupeViandePosition);
            }
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FoodMesh"))
        {
            _food = other.GetComponentInParent<Food>();
            _foodOven.Remove(other.gameObject);
            _foodIdOven.Remove(_food);

            RecipeIp /= _food.Id;
        }
    }


    public void MakeThePlate(GameObject plate, Transform spawnPosition)
    {
        for (var i = 0; i < _foodOven.Count; i++)
        {
            Destroy(_foodOven[i]);


        }
        _foodOven.Clear();
        _foodIdOven.Clear();
        Instantiate(plate, spawnPosition.position, transform.rotation);
        RecipeIp = 1;
    }



}
