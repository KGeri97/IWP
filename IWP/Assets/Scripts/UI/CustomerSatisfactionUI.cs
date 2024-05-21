using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// NOTE: need to have the customersatisfaction number calculated from the product quality and delivery time
/// </summary>

public class CustomerSatisfactionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private List<Image> _satisfactionStarImages = new List<Image>();
    [SerializeField] private List<Image> _productQualityStarImages = new List<Image>();
    [SerializeField] private List<Image> _deliveryTimeStarImages = new List<Image>();
    [SerializeField] private Sprite _fullStar, _emptyStar;
    public float CustomerSatisfaction, ProductQuality, DeliveryTime;
    private int _amountOfSatisfactionStarsDisplayed, _amountOfProductQualityStarsDisplayed, _amountOfDeliveryTimeStarsDisplayed;
    public GameObject hiddenStats;
    
    void Start()
    {
        UpdateSatisfactionDisplay();
        UpdateProductQualityDisplay();
        UpdateDeliveryTimeDisplay();
        
        hiddenStats.SetActive(false);
    }

    void Update()
    {
        if ((int)CustomerSatisfaction != _amountOfSatisfactionStarsDisplayed)
        {
            UpdateSatisfactionDisplay();
        }

        if (hiddenStats.activeSelf)
        {
            if ((int)ProductQuality != _amountOfProductQualityStarsDisplayed)
            {
                UpdateProductQualityDisplay();
            }

            if ((int)DeliveryTime != _amountOfDeliveryTimeStarsDisplayed)
            {
                UpdateDeliveryTimeDisplay();
            }
        }
    }

    private void UpdateSatisfactionDisplay()
    {
        _amountOfSatisfactionStarsDisplayed = (int)CustomerSatisfaction;

        for (int i = 0; i < _satisfactionStarImages.Count; i++)
        {
            if (i < _amountOfSatisfactionStarsDisplayed)
            {
                _satisfactionStarImages[i].sprite = _fullStar;
            }
            else
            {
                _satisfactionStarImages[i].sprite = _emptyStar;
            }
        }
    }

    private void UpdateProductQualityDisplay()
    {
        _amountOfProductQualityStarsDisplayed = (int)ProductQuality;
        
        for (int i = 0; i < _productQualityStarImages.Count; i++)
        {
            if (i < _amountOfProductQualityStarsDisplayed)
            {
                _productQualityStarImages[i].sprite = _fullStar;
            }
            else
            {
                _productQualityStarImages[i].sprite = _emptyStar;
            }
        }
    }

    private void UpdateDeliveryTimeDisplay()
    {
        _amountOfDeliveryTimeStarsDisplayed = (int)DeliveryTime;

        for (int i = 0; i < _deliveryTimeStarImages.Count; i++)
        {
            if (i < _amountOfDeliveryTimeStarsDisplayed)
            {
                _deliveryTimeStarImages[i].sprite = _fullStar;
            }
            else
            {
                _deliveryTimeStarImages[i].sprite = _emptyStar;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hiddenStats.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hiddenStats.SetActive(false);
    }
}
