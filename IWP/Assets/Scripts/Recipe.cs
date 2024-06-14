using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe")]
public class Recipe : ScriptableObject
{
    public ProductType Ingredient1;
    public int Amount1;
    public ProductType Ingredient2;
    public int Amount2;
    public Product EndProduct;
    public int EndProductAmount;

    public float ProductionTime;
}


