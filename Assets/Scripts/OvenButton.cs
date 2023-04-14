using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenButton : Oven
{
    public Oven _oven;


    private void Start()
    {
        _oven = GetComponentInParent<Oven>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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


}
