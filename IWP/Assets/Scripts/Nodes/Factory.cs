using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Node {
    [SerializeField]
    private Product _productCreated;
    [SerializeField]
    private int _batchSize;
    [SerializeField]
    private float _productionTime;
    [SerializeField]
    private Quality _baseProductionQuality;
    private bool _isProducing;
    public bool IsProducing { get { return _isProducing; } private set { } }

    private Timer _productionTimer;
    private List<Transfer> _transfers = new();
    private int _transferIterator;

    private void Awake() {
        _productionTimer = new(_productionTime, ItemProduced);
        _productionTimer.Repeat(true);
        StartProduction();
    }

    private void Update() {
        _productionTimer.Update();
    }

    public void StartProduction() {
        _isProducing = true;
        _productionTimer.Start();
    }

    public void StopProduction() {
        _isProducing = false;
        _productionTimer.Reset();
    }

    public virtual void ToggleProductFactory() {
        if (IsProducing)
            StopProduction();
        else
            StartProduction();
    }

    private void ItemProduced() {
        for (int i = 0; i < _batchSize; i++) {
            AddToInventory(_productCreated);
        }
        Debug.Log($"{_batchSize} {_productCreated.Type.ToString()} was created");
        Debug.Log($"Currently in inventory {_inventory.GetQuantityOfProduct(_productCreated)}");
    }
}
