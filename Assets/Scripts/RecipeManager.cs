using Packages.Rider.Editor.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    Dictionary<int, Recipe> _recipes = new Dictionary<int, Recipe>();





    private void Start()
    {
        Recipe Hamburger = new Recipe();
        Hamburger.Name = "Hamburger";
        Hamburger.Id = 30;
        Hamburger.NumberOfIngredients = 3;
        _recipes.Add(Hamburger.Id, Hamburger);
        Debug.Log(_recipes[30]);
        
    }
    public  Recipe RecipeValid(List<Food> ingredients)
    {
        Recipe tmp = null;
        int result = 1;
        foreach (Food ingredient in ingredients)
        {
            result = result * ingredient.Id;        
        }
        if (_recipes.ContainsKey(result))
        {
            tmp = _recipes[result];
        }
        else
        {
            return null;
        }

        return tmp;
    }
}
