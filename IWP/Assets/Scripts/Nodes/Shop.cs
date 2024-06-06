using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Node {

    [SerializeField]
    private Product _money;

    private void OnEnable() {
        CustomerManager.ItemPurchased += Checkout;
    }

    private void OnDisable() {
        CustomerManager.ItemPurchased -= Checkout;
    }

    private void Checkout() {
        int itemTypeCount = _inventory.GetProductsInStock().Count;

        if (itemTypeCount == 0)
            return;

        int randomItemIndex = Random.Range(0, itemTypeCount - 1);

        ProductType productType = _inventory.GetProductsInStock()[randomItemIndex];

        Product purchasedItem = Inventory.TakeAnItem(productType);

        for (int i = 0; i < purchasedItem.Price; i++)
            Inventory.AddItem(_money);
    }
}
