using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assembler : Node {
    [SerializeField]
    private Recipe _recipe;

    [SerializeField]
    private Quality _baseProductionQuality;
    private bool _isProducing = false;
    public bool IsProducing { get { return _isProducing; } private set { } }

    private Timer _productionTimer;
    private List<Transfer> _transfers = new();
    private int _transferIterator;

    public override void Awake() {
        base.Awake();
        _productionTimer = new(_recipe.ProductionTime, ItemProduced);
        _productionTimer.Repeat(false);
        StartProduction();
    }

    public override void Update() {
        base.Update();
        _productionTimer.Update();
    }

    [ContextMenu("Start production")]
    public void StartProduction() {
        if (!IngredientCheck(_recipe)) {
            Debug.LogError("There is not enough ingredients in the inventory to produce.");
            return;
        }

        _isProducing = true;
        RemoveIngredients(_recipe);
        _productionTimer.Start();
    }

    [ContextMenu("Stop production")]
    public void StopProduction() {
        _isProducing = false;
        _productionTimer.Reset();
    }

    public void ToggleProductionAssembler() {
        if (IsProducing)
            StopProduction();
        else
            StartProduction();
    }

    private void ItemProduced() {
        for (int i = 0; i < _recipe.EndProductAmount; i++) {
            AddToInventory(_recipe.EndProduct);
        }

        //Debug.Log($"{_recipe.EndProductAmount} {_recipe.EndProduct.Type} was created");
        //Debug.Log($"Currently in inventory {_inventory.GetQuantityOfProduct(_recipe.EndProduct)}");

        if (IngredientCheck(_recipe)) {
            RemoveIngredients(_recipe);
            _productionTimer.Reset();
            _productionTimer.Start();
        }
    }

    /// <summary>
    /// Returns true if there are enough ingredients to craft another item
    /// </summary>
    /// <param name="recipe"></param>
    /// <returns></returns>
    private bool IngredientCheck(Recipe recipe) {
        Debug.Log(Inventory.GetQuantityOfProduct(recipe.Ingredient1) >= recipe.Amount1 &&
            Inventory.GetQuantityOfProduct(recipe.Ingredient2) >= recipe.Amount2);
        return 
            Inventory.GetQuantityOfProduct(recipe.Ingredient1) >= recipe.Amount1 &&
            Inventory.GetQuantityOfProduct(recipe.Ingredient2) >= recipe.Amount2;
    }

    private void RemoveIngredients(Recipe recipe) {
        _inventory.TakeItems(recipe.Ingredient1, recipe.Amount1);
        _inventory.TakeItems(recipe.Ingredient2, recipe.Amount2);
    }
}
