using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour, INode {

    [SerializeField] 
    public List<Product> Products;
    protected List<Transfer> _transfersOutbound = new();
    protected List<Transfer> _transfersIncoming = new();
    protected List<ManufacturingLine> _manufacturingLines = new();
    protected List<Product> _inventory = new();
    public List<Product> Inventory { get { return _inventory; }}

    [SerializeField]
    private bool _available = true;
    public bool Available { get { return _available; } private set { } }

    [SerializeField]
    private GameObject _ui;

    private void OnEnable() {
        SelectionManager.OnNodeSelected += SetUIInactive;
    }

    private void OnDisable() {
        SelectionManager.OnNodeSelected -= SetUIInactive;
    }

    private void Start() {
        _ui.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void Update() {
        UpdateManufacturingLines();
    }

    public virtual void SetUI(bool active) {
        if (GameManager.State == GameManager.GameState.Running)
            _ui.SetActive(active);
    }

    public virtual void SetUIInactive(GameObject gameObject) {
        if (gameObject == this.gameObject) {
            SetUI(true);
        }
        else {
            SetUI(false);
        }
    }

    public virtual void AddIncomingTransfer(Transfer transfer) {
        _transfersIncoming.Add(transfer);
    }

    public virtual void AddOutboundTransfer(Transfer transfer) {
        _transfersOutbound.Add(transfer);
    }

    public virtual void UpdateManufacturingLines() {
        if (_manufacturingLines.Count == 0)
            return;

        foreach (ManufacturingLine manufacturingline in _manufacturingLines) {
            manufacturingline.Update();
        }
    }

    public virtual void AddToInventory(Product product) {
        _inventory.Add(product);
        Debug.Log(_inventory.Count);
    }

    public void AddManufacturingLine(ManufacturingLine manufacturingLine) {
        _manufacturingLines.Add(manufacturingLine);
    }

    public virtual void ToggleManufacturingLine() {
        if (_manufacturingLines.Count == 0)
            return;

        ManufacturingLine ml = _manufacturingLines[0];

        if (ml.IsProducing)
            ml.StopProduction();
        else
            ml.StartProduction();
    }
}
