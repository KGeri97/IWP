using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FactoryNodeUI : BaseNodeUI
{
    public List<Sprite> productIcons = new List<Sprite>();
    public TMP_Text factoryInfomationText;
    public Image factoryProductIcon;

    private ProductType _productType;
    private Factory _factory;

    void Start()
    {
        if (transform.parent.GetComponent<Factory>() == null)
        {
            Debug.LogError($"No factory script attached to {transform.parent.name}");
        }

        _factory = transform.parent.GetComponent<Factory>();
        _productType = _factory.GetFactoryProductType();
        
        UpdateFactoryImage(_productType);
        UpdateFactoryText();
        UpdateProductTypeDropdown();
    }

    private void UpdateFactoryImage(ProductType productType)
    {
        Sprite sprite = productIcons.FirstOrDefault(a => a.name == productType.ToString());

        if (sprite == null)
        {
            Debug.LogError($"The {sprite} is not valid. Entered in the wrong product type. Only KnifeBlade and KnifeHandle is valid.");
            return; 
        }
        
        factoryProductIcon.sprite = sprite;
    }
    
    private void UpdateFactoryText()
    {
        factoryInfomationText.text = $"Producing: {TextCorrection(_productType)}";
    }
    
    private void UpdateProductTypeDropdown()
    {
        productTypeDropdown.options.Clear();

        productTypeDropdown.options.Add(new TMP_Dropdown.OptionData(_productType.ToString()));
        productTypeDropdown.options.Add(new TMP_Dropdown.OptionData($"Money"));
        
        productTypeDropdown.RefreshShownValue();
    }

    public override void ToggleTransfer() {
        base.ToggleTransfer();
        _factory.ToggleProductionFactory();
    }
}
