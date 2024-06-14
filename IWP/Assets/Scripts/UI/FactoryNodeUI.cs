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
    [SerializeField]
    private Factory _factory;

    private void Start() {

        _factory = transform.parent.GetComponent<Factory>();
    }

    public void UpdateFactoryImage(ProductType productType)
    {
        Sprite sprite = productIcons.FirstOrDefault(a => a.name == productType.ToString());

        if (sprite == null)
        {
            Debug.LogError($"The {sprite} is not valid. Entered in the wrong product type. Only KnifeBlade and KnifeHandle is valid.");
            return;
        }
        
        factoryProductIcon.sprite = sprite;
        
        UpdateFactoryText(productType.ToString());
    }
    
    private void UpdateFactoryText(string productType)
    {
        string textCorrection = "Nothing";

        if (productType == $"KnifeBlade")
        {
            textCorrection = $"Knife Blades";
        }
        else if (productType == $"KnifeHandle")
        {
            textCorrection = $"Knife Handles";
        }
        
        factoryInfomationText.text = $"Producing: {textCorrection}";
    }

    public override void ToggleTransfer() {
        base.ToggleTransfer();
        _factory.ToggleProductionFactory();
    }
}
