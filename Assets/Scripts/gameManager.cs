using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    private Transform _FXs ;
    [SerializeField]
    private static gameManager _gameManager;
    private int _numberOfPlate;

    //public static List<Food> ingredients = new List<Food>();

    private void Awake()
    {
        _gameManager = this;
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

    public void WinCondition()
    {
        _numberOfPlate++;
        if (_numberOfPlate == 3)
        {
            Debug.Log("Victoire");
        }
    }

}
