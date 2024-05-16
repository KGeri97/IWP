using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManufacturingLine {

    public ManufacturingLine(Product product, float productionTimePerUnit, int batchSize, Transfer transfer) {
        _productionTimer = new(productionTimePerUnit, PackageItems);
        _productionTimer.Repeat(true);
        _burstSendTimer = new(_burstSendDelay, InstantiateProducts);

        _product = product;
        _productionTimePerUnit = productionTimePerUnit;
        _batchSize = batchSize;
        _transfer = transfer;
    }

    private Product _product;
    private Timer _productionTimer;
    private Timer _burstSendTimer;
    private float _burstSendDelay = GlobalConstants.BURST_SEND_DELAY;
    private float _productionTimePerUnit;
    private int _batchSize;
    private Transfer _transfer;
    private int _waitingToBeDispatched;

    public void StartProduction() {
        Debug.Log("Production Started");
        _productionTimer.Start();
    }

    public void StopProduction() {
        _productionTimer.Reset();
    }

    public void Update() {
        _productionTimer.Update();
        _burstSendTimer.Update();
        //Debug.Log("Line updated");
    }

    private void InstantiateProducts() {
        Debug.Log("Product Instantiated");
        Product product = GameObject.Instantiate(_product, _transfer.transform.position, Quaternion.identity, _transfer.transform);
        _transfer.AddProductToDeliver(product);

        _waitingToBeDispatched--;

        if (_waitingToBeDispatched > 0) {
            _burstSendTimer.Reset();
            _burstSendTimer.Start();
        }
    }

    private void PackageItems() {
        Debug.Log("ItemPackaged");
        _waitingToBeDispatched += _batchSize;
        if (!_burstSendTimer.IsRunning) {
            _burstSendTimer.Reset();
            _burstSendTimer.Start();
        }
    }
}
