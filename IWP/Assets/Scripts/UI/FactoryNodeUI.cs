using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FactoryNodeUI : BaseNodeUI
{
    public TMP_Text factoryInfomationText;
    public Image factoryProductIcon;

    public void UpdateRefineryText(string productType)
    {
        factoryInfomationText.text = $"The refinery is producing: {productType}";
    }

    public void UpdateRefineryImage(Image productIcon)
    {
        factoryProductIcon = productIcon;
    }
}
