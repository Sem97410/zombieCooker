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



    [Header("GameObject des plats")]

    [SerializeField] private GameObject _hamburger;
    [SerializeField] private GameObject _salade;
    [SerializeField] private GameObject _soupeViande;



    [Header("Position de spawn des plats")]

    [SerializeField] private Transform _spawnHamburgerPosition;
    [SerializeField] private Transform _spawnSaladePosition;
    [SerializeField] private Transform _spawnSoupeViandePosition;

    public int recipeIp { get => RecipeIp; set => RecipeIp = value; }
    public GameObject Hamburger { get => _hamburger; set => _hamburger = value; }
    public GameObject Salade { get => _salade; set => _salade = value; }
    public GameObject SoupeViande { get => _soupeViande; set => _soupeViande = value; }
    public Transform SpawnHamburgerPosition { get => _spawnHamburgerPosition; set => _spawnHamburgerPosition = value; }
    public Transform SpawnSaladePosition { get => _spawnSaladePosition; set => _spawnSaladePosition = value; }
    public Transform SpawnSoupeViandePosition { get => _spawnSoupeViandePosition; set => _spawnSoupeViandePosition = value; }
    public List<GameObject> FoodOven { get => _foodOven; set => _foodOven = value; }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FoodMesh"))
        {
            RecipeIp = 1;
            _food = other.GetComponentInParent<Food>();
            _foodOven.Add(other.transform.parent.gameObject);
            _foodIdOven.Add(_food);
            _food.enabled = false;
            for (var i = 0; i < _foodIdOven.Count; i++)
            {
                RecipeIp *= _foodIdOven[i].Id;

            }


 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FoodMesh"))
        {
            _food = other.GetComponentInParent<Food>();
            _foodOven.Remove(other.transform.parent.gameObject);
            _foodIdOven.Remove(_food);

            RecipeIp /= _food.Id;
            if (_foodIdOven.Count < 0)
            {
                RecipeIp = 1;
            }
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
