using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetIncomeColour : MonoBehaviour
{
    public float NetValue;
    public Text NetValueText;
    public Text NetIncomeText;

    // Update is called once per frame
    void Update()
    {

        if (NetValue == 0) { NetValueText.color = Color.white; }
        else
        {
            if (NetValue > 0)
            {
                NetValueText.color = Color.green;
                NetIncomeText.color = Color.green;
            }
            else
            {
                NetValueText.color = Color.red;
                NetIncomeText.color = Color.red;
            }
        };
        NetValueText.text = NetValue.ToString();
    }
}
