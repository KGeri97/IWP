using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfer : MonoBehaviour
{
    [SerializeField] private Node _startNode;
    public Node StartNode { get { return _startNode; } set { _startNode = value; } }
    [SerializeField]
    private Node _endNode;
    public Node EndNode { get { return _endNode; } set { _endNode = value; } }
    private ProductType _transferredProductType;
    public ProductType TransferredProductType { get { return _transferredProductType; } set { _transferredProductType = value; } }
    [SerializeField]
    private float _transferTime;
    private LineRenderer _lineRenderer;
    [SerializeField]
    private LayerMask _raycastLayermask;
    public class ProductProgress {
        public ProductProgress(Product product) {
            Product = product;
            Progress = 0;
        }

        public Product Product { get; }
        public float Progress;

    }

    private List<ProductProgress> _productsInTransfer = new();

    private void Awake() {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = GlobalConstants.NODE_SCALE * 0.75f;
        _lineRenderer.endWidth = GlobalConstants.NODE_SCALE * 0.75f;
    }

    private void OnEnable() {
        SelectionManager.OnNodeSelected += SetEndNode;
        SelectionManager.OnNothingSelected += CancelTransferCreation;
    }

    private void OnDisable() {
        SelectionManager.OnNothingSelected -= CancelTransferCreation;
    }

    private void Start() {
        _lineRenderer.SetPosition(0, _startNode.transform.position);
    }

    private void Update() {
        if (!_endNode) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, _raycastLayermask)) {
                _lineRenderer.SetPosition(1, hit.point + Vector3.up * _startNode.transform.position.y);
            }
        }

        if (_productsInTransfer.Count > 0) {
            MoveProducts();
        }
    }

    private void SetEndNode(GameObject go) {
        if (go != _startNode.gameObject) {
            _endNode = go.GetComponent<Node>();
            _startNode.AddOutboundTransfer(this);
            _endNode.AddIncomingTransfer(this);
            _lineRenderer.SetPosition(1, _endNode.transform.position);
            _transferTime = Vector3.Distance(_startNode.transform.position, _endNode.transform.position);

            SelectionManager.OnNodeSelected -= SetEndNode;
            SelectionManager.OnNothingSelected -= CancelTransferCreation;

            GameManager.State = GameManager.GameState.Running;
        }
    }

    private void CancelTransferCreation() {
        if (!_endNode) {
            GameManager.State = GameManager.GameState.Running;
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void MoveProducts() {
        List<ProductProgress> productsToRemove = new();
        foreach (ProductProgress package in _productsInTransfer) {
            if (package.Progress / _transferTime >= 1) {
                _endNode.AddToInventory(package.Product);
                productsToRemove.Add(package);
            }

            package.Product.transform.position = Vector3.Lerp(_startNode.transform.position, _endNode.transform.position, package.Progress / _transferTime);
            package.Progress += Time.deltaTime;
        }

        foreach (ProductProgress package in productsToRemove) {
            _productsInTransfer.Remove(package);
        }
    }

    public void AddProductToDeliver(Product product) {
        _productsInTransfer.Add(new(product));
    }
}
