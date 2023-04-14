using UnityEngine;

public class OvenButton : MonoBehaviour
{
    [SerializeField]
    private Oven _oven;


    private void Start()
    {
        _oven = GetComponentInParent<Oven>();
    }


    public void Update()
    {
        Debug.Log(_oven.recipeIp);
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other);
            if (_oven.recipeIp == 30)
            {
                _oven.MakeThePlate(_oven.Hamburger, _oven.SpawnHamburgerPosition);
            }
            if (_oven.recipeIp == 5005)
            {
                _oven.MakeThePlate(_oven.Salade, _oven.SpawnSaladePosition);
            }
            if (_oven.recipeIp == 10)
            {
                _oven.MakeThePlate(_oven.SoupeViande, _oven.SpawnSoupeViandePosition);
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
