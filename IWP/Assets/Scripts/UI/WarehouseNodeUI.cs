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

    private void Update() {
        base.Update();
        //UpdateWarehouseText(transform.parent.GetComponent<Node>().Products[0].name, transform.parent.GetComponent<Node>().Inventory.Count.ToString());
    }

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
