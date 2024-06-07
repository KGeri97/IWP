using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public class WarehouseItem
{
    public ProductType Type;
    public Sprite Icon;
}

public class WarehouseNodeUI : BaseNodeUI
{
    [SerializeField] private List<WarehouseItem> WarehouseItems = new List<WarehouseItem>();
    [SerializeField] private List<Sprite> _sprites = new List<Sprite>();
    [SerializeField] private Transform contentTransform;
    [SerializeField] private GameObject item;

    private List<GameObject> _currentItems = new List<GameObject>();

    private void Start()
    {
        inventory = node.Inventory;
        inventory.OnInventoryChanged += UpdateInventory;
        inventory.OnInventoryChanged += UpdateProductTypeDropdown; 
        
        UpdateInventory();
        UpdateProductTypeDropdown();
    }

    private void OnDestroy()
    {
        inventory.OnInventoryChanged -= UpdateInventory;
        inventory.OnInventoryChanged -= UpdateProductTypeDropdown; 

    }

    private void UpdateInventory()
    {
        List<ProductType> productTypes = inventory.GetProductsInStock();

        for (int i = _currentItems.Count - 1; i >= 0; i--)
        {
            InventoryIconController iconController = _currentItems[i].GetComponent<InventoryIconController>();
            if (!productTypes.Contains(iconController.GetProductType()))
            {
                Destroy(_currentItems[i]);
                _currentItems.RemoveAt(i);
            }
        }
        
        foreach (var productType in productTypes)
        {
            Sprite sprite = _sprites.FirstOrDefault(a => a.name == productType.ToString());

            bool itemExists = _currentItems.Any(icon => icon.GetComponent<InventoryIconController>().GetProductType() == productType);

            if (!itemExists)
            {
                GameObject gameObjectIcon = Instantiate(item, contentTransform);
                gameObjectIcon.GetComponent<InventoryIconController>().SetInventoryIcon(node, productType, sprite);
                _currentItems.Add(gameObjectIcon);
            }
        }
    }
}