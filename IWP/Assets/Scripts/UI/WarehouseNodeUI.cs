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

    private Inventory _inventory;
    private List<GameObject> _currentItems = new List<GameObject>();

    void Start()
    {
        _inventory = node.Inventory;
        _inventory.OnInventoryChanged += UpdateInventory;
        _inventory.OnInventoryChanged += UpdateProductTypeDropdown; 
        
        UpdateInventory();
        UpdateProductTypeDropdown();
    }

    void OnDestroy()
    {
        _inventory.OnInventoryChanged -= UpdateInventory;
        _inventory.OnInventoryChanged -= UpdateProductTypeDropdown; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateInventory();
            UpdateProductTypeDropdown();
        }
    }

    private void UpdateInventory()
    {
        List<ProductType> productTypes = _inventory.GetProductsInStock();
        Debug.Log("Updating Inventory. Products in stock: " + string.Join(", ", productTypes));

        for (int i = _currentItems.Count - 1; i >= 0; i--)
        {
            InventoryIconController iconController = _currentItems[i].GetComponent<InventoryIconController>();
            if (!productTypes.Contains(iconController.GetProductType()))
            {
                Destroy(_currentItems[i]);
                _currentItems.RemoveAt(i);
                Debug.Log("Destroyed item: " + iconController.GetProductType());
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
                Debug.Log("Spawned new item: " + productType);
            }
        }
    }
    
    private void UpdateProductTypeDropdown()
    {
        productTypeDropdown.options.Clear();
        List<ProductType> productTypes = _inventory.GetProductsInStock();
        
        foreach (ProductType productType in productTypes)
        {
            productTypeDropdown.options.Add(new TMP_Dropdown.OptionData(productType.ToString()));
        }
        productTypeDropdown.RefreshShownValue();
    }
}