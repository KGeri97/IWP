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
    [SerializeField] private Image _switch;
    
    public Node node;
    public TMP_Dropdown productTypeDropdown;
    private Camera _camera;

    private bool isOperating = false;
    
    void Awake()
    {
        SetEventCamera();
    }

    void Start()
    {
        node = transform.parent.GetComponent<Node>();
        
        _switch.color = Color.red;
    }

    private void SetEventCamera()
    {
        _camera = Camera.main;
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
    
    
    public void AddTransfer()
    {
        Transfer transfer = Instantiate(_transferPrefab, transform.parent.position, Quaternion.identity, _transferParent);
        transfer.StartNode = node;
        //transfer.TransferredProduct = _node.Products[0];
        GameManager.State = GameManager.GameState.PlacingTransfer;

        isOperating = true;
        _switch.color = Color.green;
        
        CloseUI();
    }

    public void AddTransferWithProductType()
    {
        ProductType selectedProductType = ProductType.Default;
        if (productTypeDropdown.options != null)
        {
            selectedProductType = (ProductType) productTypeDropdown.value;
        }
        
        Transfer transfer = Instantiate(_transferPrefab, transform.parent.position, Quaternion.identity, _transferParent);
        transfer.StartNode = node;
        transfer.TransferredProductType = selectedProductType;
        //transfer.TransferredProduct = _node.Products[0];
        GameManager.State = GameManager.GameState.PlacingTransfer;

        isOperating = true;
        _switch.color = Color.green;
        
        CloseUI();
    }

    //This should also derive from either the assembly or factory as it has isProducing
    public virtual void ToggleTransfer()
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

    public string TextCorrection(ProductType productType)
    {
        if (productType.ToString() == $"KnifeBlade")
        {
            return $"Knife Blades";
        }
        if (productType.ToString() == $"KnifeHandle")
        {
            return $"Knife Handles";
        }

        return $"Nothing";
    }
}
