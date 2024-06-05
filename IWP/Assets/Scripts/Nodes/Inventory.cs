using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public Inventory() {
        _stock = new();
    }

    private Dictionary<ProductType, Queue<Product>> _stock;
    
    public int GetQuantityOfProduct(Product product) {
        if (!_stock.ContainsKey(product.Type))
            return 0;

        return _stock[product.Type].Count;
    }

    public void AddItem(Product product) {
        if (!_stock.ContainsKey(product.Type)) {
            Debug.Log("The product key does not exist");
            _stock.Add(product.Type, new());
        }

        _stock[product.Type].Enqueue(product);
    }

    public Product TakeAnItem(Product product) {
        if (!_stock.ContainsKey(product.Type))
            return null;

        if (_stock[product.Type].Count == 0)
            return null;

        Product itemTaken = _stock[product.Type].Dequeue();

        RemoveKeyIfEmpty(product);

        return itemTaken;
    }

    public List<Product> TakeItems(Product product, int numberOfItems) {
        if (!_stock.ContainsKey(product.Type))
            return null;

        List<Product> takenItems = new();

        for (int i = 0; i < numberOfItems; i++) {
            if (_stock[product.Type].Count == 0)
                return takenItems;

            takenItems.Add(_stock[product.Type].Dequeue());
        }

        RemoveKeyIfEmpty(product);

        return takenItems;
    }

    private void RemoveKeyIfEmpty(Product product) {
        if (_stock[product.Type].Count == 0) {
            _stock.Remove(product.Type);
        }
    }
}
