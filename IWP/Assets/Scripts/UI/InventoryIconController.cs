using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryIconController : MonoBehaviour
{
    public TMP_Text text;
    
    private Node _node;
    private ProductType _productType;
    private Image _image;

    private bool _isSetUp;
    
    public void SetInventoryIcon(Node node, ProductType productType, Sprite sprite)
    {
        _node = node;
        _productType = productType;
        _image.GetComponent<Image>();
        _image.sprite = sprite;

        _isSetUp = true;
    }

    public ProductType GetProductType()
    {
        return _productType;
    }

    private void Update()
    {
        if (_isSetUp)
        {
            UpdateAmountText();
        }
    }

    private void UpdateAmountText()
    {
        int quantity = _node.GetComponent<Inventory>().GetQuantityOfProduct(_productType);
        text.text = quantity.ToString();
    }
}
