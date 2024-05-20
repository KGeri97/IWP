using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RefineryNodeUI : BaseNodeUI
{
    public TMP_Text refineryInfomationText;
    public Image refineryProductIcon;

    public void UpdateRefineryText(string productType)
    {
        refineryInfomationText.text = $"The refinery is producing: {productType}";
    }

    public void UpdateRefineryImage(Image productIcon)
    {
        refineryProductIcon = productIcon;
    }
}
