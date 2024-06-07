using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BaseNodeUI : MonoBehaviour
{
    [SerializeField] private Transfer _transferPrefab;
    [SerializeField] private Transform _transferParent;
    [SerializeField] private TMP_Dropdown _productTypeDropdown;
    [SerializeField] private Image _switch;
    
    public Node node;
    public Inventory inventory;
    
    private Camera _camera;

    private bool isOperating = false;
    
    void Awake()
    {
        SetEventCamera();
    }

    void Start()
    {
        node = transform.parent.GetComponent<Node>();
        inventory = node.Inventory;
        inventory.OnInventoryChanged += UpdateProductTypeDropdown;
        
        _switch.color = Color.red;
        
        UpdateProductTypeDropdown();
    }

    void OnDestroy()
    {
        inventory.OnInventoryChanged -= UpdateProductTypeDropdown;
    }

    private void SetEventCamera()
    {
        _camera = Camera.main;
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public void CloseUI()
    {
        print($"hitting button");
        gameObject.SetActive(false);
    }

    public void AddTransfer()
    {
        print($"hit button two");
        ProductType selectedProductType = (ProductType) _productTypeDropdown.value;
        
        Transfer transfer = Instantiate(_transferPrefab, transform.parent.position, Quaternion.identity, _transferParent);
        transfer.StartNode = node;
        //transfer.TransferredProduct = _node.Products[0];
        GameManager.State = GameManager.GameState.PlacingTransfer;

        isOperating = true;
        _switch.color = Color.green;
        
        CloseUI();
    }

    protected void UpdateProductTypeDropdown()
    {
        _productTypeDropdown.options.Clear();
        List<ProductType> productTypes = inventory.GetProductsInStock();
        
        foreach (ProductType productType in productTypes)
        {
            _productTypeDropdown.options.Add(new TMP_Dropdown.OptionData(productType.ToString()));
        }
        _productTypeDropdown.RefreshShownValue();
    }

    public void ToggleTransfer()
    {
        if (isOperating)
        {
            isOperating = false;
            _switch.color = Color.red;
        }
        else
        {
            isOperating = true;
            _switch.color = Color.green;
        }
    }
}
