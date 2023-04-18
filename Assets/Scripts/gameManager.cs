using JetBrains.Annotations;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    private Transform _FXs;
    private Transform _Foods;
    [SerializeField]
    private static gameManager _gameManager;
    private int _numberOfPlate;

    //public static List<Food> ingredients = new List<Food>();

    [SerializeField] private int _foodCount;
    [SerializeField] private int _maxFoodSpawn;

    [SerializeField] private int _zombieCount;
    [SerializeField] private int _maxZombieSpawn;

    [SerializeField] private ZombieSpawner[] _zombieSpawners;
    [SerializeField] private FoodSpawner[] _foodSpawners;

    public int MaxFoodSpawn { get => _maxFoodSpawn; set => _maxFoodSpawn = value; }
    public int FoodCount { get => _foodCount; set => _foodCount = value; }
    public int MaxZombieSpawn { get => _maxZombieSpawn; set => _maxZombieSpawn = value; }
    public int ZombieCount { get => _zombieCount; set => _zombieCount = value; }

    private void Awake()
    {
        _gameManager = this;
    }

    private void Start()
    {
        foreach (FoodSpawner foodSpawner in _foodSpawners)
        {
            foodSpawner.StartSpawn();
        }

        foreach (ZombieSpawner zombieSpawner in _zombieSpawners)
        {
            zombieSpawner.StartSpawn();
        }
    }
    public static gameManager Instance()
    {
        if (_gameManager == null)
        {
            Debug.LogError("No Level instance found.");
        }

        return _gameManager;
    }

    public static Fx AddFX(Fx model, Vector3 position, Quaternion rotation)
    {
        if (model)
        {
            //return AddFX(model, position, rotation);
            Fx fx = Instantiate(model, position, rotation);
            fx.transform.SetParent(Instance()._FXs);
            return fx;

        }
        return null;
    }

    public static Food SpawnFoodInSpawner(Food model, Vector3 position, Quaternion rotation)
    {
        if (model)
        {
            Food food = Instantiate(model, position, rotation);
            food.transform.SetParent(Instance()._Foods);
        }

        return null;
    }

    public static Zombie SpawnZombieInSpawner(Zombie model, Vector3 position, Quaternion rotation)
    {
        if (model)
        {
            Zombie zombie = Instantiate(model, position, rotation);
            zombie.transform.SetParent(Instance()._Foods);
        }

        return null;
    }

    public void WinCondition()
    {
        _numberOfPlate++;
        if (_numberOfPlate == 4)
        {
            Debug.Log("Victoire");
        }
    }

}
