using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour, INode {

    [SerializeField] 
    public List<Product> Products;
    protected List<Transfer> _transfersOutbound = new();
    protected List<Transfer> _transfersIncoming = new();

    [SerializeField]
    private bool _available = true;
    public bool Available { get { return _available; } private set { } }

    [SerializeField]
    private GameObject _ui;

    private void Start() {
        _ui.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void OnEnable() {
        SelectionManager.OnNodeSelected += SetUIInactive;
    }

    private void OnDisable() {
        SelectionManager.OnNodeSelected -= SetUIInactive;
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

    public void AddIncomingTransfer(Transfer transfer) {
        _transfersIncoming.Add(transfer);
    }

    public void AddOutboundTransfer(Transfer transfer) {
        _transfersOutbound.Add(transfer);
    }
}
