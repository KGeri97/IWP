using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThresholdBar : MonoBehaviour
{
    public Slider ThresholdSlider;
    public float CurrentThreshold;
    public float PlayerRevenue;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerRevenue = GameObject.FindGameObjectWithTag("Player").GetComponent<Revenue>().Revenue;
        //ThresholdSlider = GetComponent<Slider>();
        ThresholdSlider.maxValue = CurrentThreshold;
        ThresholdSlider.value = PlayerRevenue;
    }

    // Update is called once per frame
    void Update()
    {
        ThresholdSlider.value = PlayerRevenue;

    }

   // public void SetRevenueThreshold(int Revenue)
   // {
   //     ThresholdSlider.value = Revenue;
   // }
}

