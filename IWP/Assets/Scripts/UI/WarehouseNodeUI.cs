using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WarehouseNodeUI : BaseNodeUI
{
    public TMP_Text warehouseInformationText;
    public Image warehouseProductIcon;

    public void UpdateWarehouseText(string productType, string productAmount)
    {
        warehouseInformationText.text = $"The warehouse is holding: {productType}." +
                    $"{productAmount}/30";
    }

    public void UpdateWarehouseImage(Image productIcon)
    {
        warehouseProductIcon = productIcon;
    }
}
