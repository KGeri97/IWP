using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Obsolete
public class ProductFactory {

    public ProductFactory(Product product, float productionTimePerUnit, Quality quality, int batchSize, Transfer transfer) {
        _productionTimer = new(productionTimePerUnit, PackageItems);
        _productionTimer.Repeat(true);
        _burstSendTimer = new(_burstSendDelay, InstantiateProducts);

        _product = product;
        _productionTimePerUnit = productionTimePerUnit;
        _batchSize = batchSize;
        _quality = quality;
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
    private Quality _quality;
    private int _batchesProduced = 0;
    private bool _isProducing;
    public bool IsProducing { get { return _isProducing; } private set { } }

    public void StartProduction() {
        _isProducing = true;
        _productionTimer.Start();
    }

    public void StopProduction() {
        _isProducing = false;
        _productionTimer.Reset();
    }

    public void Update() {
        _productionTimer.Update();
        _burstSendTimer.Update();
        //Debug.Log("Line updated");
    }

    private void InstantiateProducts() {
        Product product = GameObject.Instantiate(_product, _transfer.transform.position, Quaternion.identity, _transfer.transform);
        
        _product.SetProductProperties(CalculateProductQuality());
        _transfer.AddProductToDeliver(product);

        _waitingToBeDispatched--;

        if (_waitingToBeDispatched > 0) {
            _burstSendTimer.Reset();
            _burstSendTimer.Start();
        }
    }

    private void PackageItems() {
        //Debug.Log("ItemPackaged");
        _waitingToBeDispatched += _batchSize;
        _batchesProduced++;
        if (!_burstSendTimer.IsRunning) {
            _burstSendTimer.Reset();
            _burstSendTimer.Start();
        }
    }

    private Quality CalculateProductQuality() {
        Quality result = Quality.Perfect;
        int lineDepreciation = Mathf.FloorToInt(_batchesProduced / GlobalConstants.LINE_QUALITY_DEPRECIATION_RATE);
        result -= lineDepreciation;
        //Debug.Log($"Line depreciation is {lineDepreciation}");

        //int chance = Random.Range(1, GlobalConstants.BATCH_QUALITY_DROP_CHANCE);
        //if (chance == 1) {
        //    result -= 1;
        //}

        int chance = Random.Range(0, GlobalConstants.PRODUCT_QUALITY_DROP_CHANCE);
        if (chance == 0) {
            result -= 1;
            //Debug.Log("Product quality dropped");
        }

        //Debug.Log($"Product end quality is {result}");

        return result;
    }
}
