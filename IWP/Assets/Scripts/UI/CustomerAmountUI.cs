using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// NOTE: need to have customersatisfaction to represent the correct number.
/// Need to discuss how the multiplier works
/// Also not sure how to implement the (amount of people)/region 
/// </summary>

public class CustomerAmountUI : MonoBehaviour
{
    [SerializeField] private List<int> _multiplier = new List<int>();
    [SerializeField] private TMP_Text _customerAmountText;
    // public int test;
    public float CustomerSatisfaction, AmountOfCustomers;
    private Coroutine _customerChangeCoroutine;

    void Start()
    {
        _customerChangeCoroutine = StartCoroutine(UpdateCustomerAmount());
    }

    void Update()
    {
        UpdateCustomerAmountText();

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     OnCustomerSatisfactionChanged(test);
        // }
    }

    public void UpdateCustomerAmountText()
    {
        _customerAmountText.text = $"{(int)AmountOfCustomers}";
    }

    private IEnumerator UpdateCustomerAmount()
    {
        while (true)
        {
            if (CustomerSatisfaction <= 2)
            {
                AmountOfCustomers = Mathf.Max(1, AmountOfCustomers - Time.deltaTime * _multiplier[(int)CustomerSatisfaction]);
            }
            else if (CustomerSatisfaction >= 3)
            {
                AmountOfCustomers += Time.deltaTime * _multiplier[(int)CustomerSatisfaction];
            }

            yield return null;
        }
    }

    public void OnCustomerSatisfactionChanged(float newSatisfaction)
    {
        CustomerSatisfaction = newSatisfaction;

        if (_customerChangeCoroutine != null)
        {
            StopCoroutine(_customerChangeCoroutine);
        }

        _customerChangeCoroutine = StartCoroutine(UpdateCustomerAmount());
    }
}
