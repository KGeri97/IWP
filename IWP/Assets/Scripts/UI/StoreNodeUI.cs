using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StoreNodeUI : BaseNodeUI
{
    public TMP_Text shopInformationText;

    void Start()
    {
        //UpdateShopUIText($"0");
    }

    public void UpdateShopUIText(string profitAmount)
    {
        shopInformationText.text = $"Currently earning: {profitAmount}";
    }
}
