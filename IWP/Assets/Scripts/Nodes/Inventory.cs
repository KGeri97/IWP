using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event Action OnInventoryChanged;
    
    public Inventory() {
        _stock = new();
    }

    private Dictionary<ProductType, Queue<Product>> _stock;

    public List<ProductType> GetProductsInStock() {
        List<ProductType> items = new();

        foreach (KeyValuePair<ProductType, Queue<Product>> pair in _stock) {
            items.Add(pair.Key);
        }
        return items;
    }
    
    public int GetQuantityOfProduct(Product product) {
        if (!_stock.ContainsKey(product.Type))
            return 0;

        return _stock[product.Type].Count;
    }
    public int GetQuantityOfProduct(ProductType productType) {
        if (!_stock.ContainsKey(productType))
            return 0;

        return _stock[productType].Count;
    }

    public void AddItem(Product product) {
        if (!_stock.ContainsKey(product.Type)) {
            //Debug.Log("The product key does not exist");
            _stock.Add(product.Type, new());
        }

        _stock[product.Type].Enqueue(product);
        OnInventoryChanged?.Invoke();
    }

    public Product TakeAnItem(Product product) {
        if (!_stock.ContainsKey(product.Type))
            return null;

        if (_stock[product.Type].Count == 0)
            return null;

        Product itemTaken = _stock[product.Type].Dequeue();

        RemoveKeyIfEmpty(product);
        OnInventoryChanged?.Invoke();
        
        return itemTaken;
    }
    public Product TakeAnItem(ProductType productType) {
        if (!_stock.ContainsKey(productType))
            return null;

        if (_stock[productType].Count == 0)
            return null;

        Product itemTaken = _stock[productType].Dequeue();

        RemoveKeyIfEmpty(productType);
        OnInventoryChanged?.Invoke();
        
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
        OnInventoryChanged?.Invoke();

        return takenItems;
    }

    public List<Product> TakeItems(ProductType productType, int numberOfItems) {
        if (!_stock.ContainsKey(productType))
            return null;

        List<Product> takenItems = new();

        for (int i = 0; i < numberOfItems; i++) {
            if (_stock[productType].Count == 0)
                return takenItems;

            takenItems.Add(_stock[productType].Dequeue());
        }

        RemoveKeyIfEmpty(productType);
        OnInventoryChanged?.Invoke();

        return takenItems;
    }

    private void RemoveKeyIfEmpty(Product product) {
        if (_stock[product.Type].Count == 0) {
            _stock.Remove(product.Type);
            OnInventoryChanged?.Invoke();
        }

    }private void RemoveKeyIfEmpty(ProductType productType) {
        if (_stock[productType].Count == 0) {
            _stock.Remove(productType);
            OnInventoryChanged?.Invoke();
        }
    }
}
