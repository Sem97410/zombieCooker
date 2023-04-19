using JetBrains.Annotations;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] private int _numberOfPlateNeed;

    [SerializeField] private Transform _FXs = null;
    [SerializeField] private Transform _Foods;
    [SerializeField] private Transform _Enemies;
    [SerializeField] private Transform _Spawners;
    [SerializeField] private static gameManager _gameManager;
    [SerializeField] private Camera _deathCamera;
    private int _numberOfPlate;

    //public static List<Food> ingredients = new List<Food>();

    //[SerializeField] private int _foodCount;
    //[SerializeField] private int _maxFoodSpawn;

    //[SerializeField] private int _zombieCount;
    //[SerializeField] private int _maxZombieSpawn;

    [SerializeField] private Spawner[] _spawners;

    public int NumberOfPlate { get => _numberOfPlate; set => _numberOfPlate = value; }
    public int NumberOfPlateNeed { get => _numberOfPlateNeed; set => _numberOfPlateNeed = value; }
    public Spawner[] Spawners { get => _spawners; set => _spawners = value; }
    public Camera DeathCamera { get => _deathCamera; set => _deathCamera = value; }

    //public int MaxFoodSpawn { get => _maxFoodSpawn; set => _maxFoodSpawn = value; }
    //public int FoodCount { get => _foodCount; set => _foodCount = value; }
    //public int MaxZombieSpawn { get => _maxZombieSpawn; set => _maxZombieSpawn = value; }
    //public int ZombieCount { get => _zombieCount; set => _zombieCount = value; }

    private void Awake()
    {
        _gameManager = this;
    }

    private void OnEnable()
    {
        ZombieEvents.onPlayerDeath += SetMainCameraOnDeath;
    }
    private void OnDisable()
    {
        ZombieEvents.onPlayerDeath -= SetMainCameraOnDeath;
    }

    private void Start()
    {
        foreach (Spawner foodSpawner in Spawners)
        {
            foodSpawner.StartSpawn();
        }

        //foreach (ZombieSpawner zombieSpawner in Spawners)
        //{
        //    zombieSpawner.StartSpawn();
        //}
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

    public static T SpawnGoInSpawner<T>(T model, Vector3 position, Quaternion rotation) where T: MonoBehaviour
    {
        if (model)
        {
            T go = Instantiate(model, position, rotation);
            go.transform.SetParent(GetParent<T>());
        }

        return null;
    }

    public static Transform GetParent<T>() where T : MonoBehaviour
    {
        Transform parent = null;
        gameManager gM = Instance();
        if (typeof(T) == typeof(Fx))
        {
            parent = gM._FXs;
        }

        if (typeof(T) == typeof(Spawner))
        {
            parent = gM._Spawners;
        }

#if UNITY_EDITOR
        else
        {
            Debug.LogWarning("Can't find parent of type '" + typeof(T) + "', temporary use level as Parent");
            parent = gM.transform;
        }
#endif //UNITY_EDITOR
        return parent;
    }

    public void WinCondition()
    {
        _numberOfPlate++;
        if (_numberOfPlate == NumberOfPlateNeed)
        {
            Debug.Log("Victoire");
            Time.timeScale = 0;
        }
    }

    public void SetMainCameraOnDeath(bool value)
    {
        DeathCamera.gameObject.SetActive(true);
        DeathCamera = Camera.main;
    }

    

}
