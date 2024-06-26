using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour, INode {

    protected List<Transfer> _transfersOutbound = new();
    protected List<Transfer> _transfersIncoming = new();
    protected int _outboundTransferIterator = 0;
    protected Inventory _inventory = new();
    protected Timer _itemDispatchTimer;
    public Inventory Inventory { get { return _inventory; } }

    [SerializeField]
    private bool _available = true;
    public bool Available { get { return _available; } private set { } }

    [SerializeField]
    private GameObject _ui;

    //The model that can be scaled
    [SerializeField]
    private Transform _model;

    private void OnEnable() {
        SelectionManager.OnNodeSelected += SetUIInactive;
        BuildingManager.OnBuildingUIOpened += SetUIInactive;
    }

    private void OnDisable() {
        SelectionManager.OnNodeSelected -= SetUIInactive;
        BuildingManager.OnBuildingUIOpened -= SetUIInactive;
    }

    public virtual void Awake() {
        _itemDispatchTimer = new(GlobalConstants.BURST_SEND_DELAY, SendItem);
        _itemDispatchTimer.Repeat(true);
        _itemDispatchTimer.Start();

        _model.localScale = new Vector3(GlobalConstants.NODE_SCALE, GlobalConstants.NODE_SCALE, GlobalConstants.NODE_SCALE);
    }

    private void Start() {
        _ui.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public virtual void Update() {
        _itemDispatchTimer.Update();
    }


    public void SetUI(bool active) {
        if (GameManager.State == GameManager.GameState.Running)
            _ui.SetActive(active);
    }

    public void SetUIInactive(GameObject gameObject) {
        if (gameObject == this.gameObject) {
            SetUI(true);
        }
        else {
            SetUI(false);
        }
    }

    public bool GetUIState()
    {
        return _ui.activeSelf;
    }

    public void AddIncomingTransfer(Transfer transfer) {
        _transfersIncoming.Add(transfer);
    }

    public virtual void AddOutboundTransfer(Transfer transfer) {
        _transfersOutbound.Add(transfer);
    }

    public void AddToInventory(Product product) {
        _inventory.AddItem(product);
        //Debug.Log(_inventory.GetQuantityOfProduct(product));
    }

    private void SendItem() {
        //Debug.Log($"{_transfersOutbound.Count}");
        if (_transfersOutbound.Count == 0)
            return;

        Transfer transfer = _transfersOutbound[_outboundTransferIterator];
        ProductType productType = transfer.TransferredProductType;

        if (_inventory.GetQuantityOfProduct(productType) > 0) {
            Product product = Instantiate(_inventory.TakeAnItem(productType), transform.position, Quaternion.identity, transfer.transform) ;

            transfer.AddProductToDeliver(product);
            //Debug.Log($"Item is sent");
        }

        //Debug.Log(_transfersOutbound.Count);
        //Debug.Log(_outboundTransferIterator);

        _outboundTransferIterator++;
        if (_outboundTransferIterator >= _transfersOutbound.Count)
            _outboundTransferIterator = 0;
    }
}
