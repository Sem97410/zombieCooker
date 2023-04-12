using UnityEngine;

public class gameManager : MonoBehaviour
{

    private Transform _FXs = null;
    [SerializeField]
    private static gameManager _gameManager = null;



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
}
