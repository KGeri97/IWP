using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public class AssemblyItem
{
    public ProductType ProductType;
    public Image Image;
    public TMP_Text Text;
    public int AmountNeeded;
    public bool HasMeetRequirements;
}

public class AssemblyNodeUI : BaseNodeUI
{
    [SerializeField] private List<AssemblyItem> _assemblyItemsNeeded = new List<AssemblyItem>();
    private AssemblyItem _assemblyItemMade;
    public int amountOfKnifesProduced;
    public bool canProduceKnives = false;
    
    private Inventory _inventory;

    void Start()
    {
        _inventory = node.Inventory;
        _inventory.OnInventoryChanged += UpdateAssemblyInformation;
        
        UpdateAssemblyInformation();
    }

    void OnDestroy()
    {
        _inventory.OnInventoryChanged -= UpdateAssemblyInformation;
    }

    private void UpdateAssemblyInformation()
    {
        foreach (var productType in _inventory.GetProductsInStock())
        {
            AssemblyItem assemblyItem = _assemblyItemsNeeded.FirstOrDefault(a => a.ProductType == productType);
            
            assemblyItem.Text.text = $"{_inventory.GetQuantityOfProduct(productType)}/{assemblyItem.AmountNeeded}";
            
            if (_inventory.GetQuantityOfProduct(productType) >= assemblyItem.AmountNeeded)
            {
                assemblyItem.Image.color = Color.white;
                assemblyItem.HasMeetRequirements = true;
            }
            else
            {
                assemblyItem.Image.color = Color.grey;
                assemblyItem.HasMeetRequirements = false;
            }
        }
        
        int requirementCheck = 0;
        
        foreach (var assemblyItem in _assemblyItemsNeeded)
        {
            if (!assemblyItem.HasMeetRequirements)
            {
                _assemblyItemMade.Image.color = Color.grey;
                _assemblyItemMade.Text.text = $"Not producing any knives";
                canProduceKnives = false;
                break;
            }
            
            requirementCheck++;
            
            if (requirementCheck >= _assemblyItemsNeeded.Count)
            {
                _assemblyItemMade.Image.color = Color.white;
                _assemblyItemMade.Text.text = $"Producing {amountOfKnifesProduced} amount of knives";
                canProduceKnives = true;
            }
        }
    }
}

// public TMP_Text assemblyInformationText;
// public Image assemblyProductIcon;
// public List<Image> assemblyAttachIcon = new List<Image>();
//     
// public void UpdateAssemblyText(string productType, string productionAmount)
// {
//     assemblyInformationText.text = $"The assembly is producing: {productType}." +
//                                    $"{productionAmount}s per month";
// }
//
// public void UpdateAssemblyImage(Image productIcon)
// {
//     assemblyProductIcon = productIcon;
// }
//
// public void UpdateAssemblyAttachIcon(Image nodeIcon, int attachNum)
// {
//     assemblyAttachIcon[attachNum] = nodeIcon;
// }