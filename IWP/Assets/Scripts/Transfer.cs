using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfer : MonoBehaviour
{
    [SerializeField] private Node _startNode;
    public Node StartNode { get { return _startNode; } set { _startNode = value; } }
    [SerializeField] private Node _endNode;
    [SerializeField] private Product _transferredProduct;
    private float _transferTime;
    private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _raycastLayermask;

    private float _elapsedTime = 0;

    private void Awake() {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnEnable() {
        SelectionManager.OnNodeSelected += SetEndNode;
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
        else {
            _transferredProduct.transform.position = Vector3.Lerp(_startNode.transform.position, _endNode.transform.position, _elapsedTime / _transferTime);
            //_lineRenderer.colorGradient = gradient;
            _elapsedTime += Time.deltaTime;
        }
    }

    private void SetEndNode(GameObject go) {
        if (go != _startNode.gameObject) {
            _endNode = go.GetComponent<Node>();
            _startNode.AddOutboundTransfer(this);
            _endNode.AddOutboundTransfer(this);
            _lineRenderer.SetPosition(1, _endNode.transform.position);
            _transferTime = Vector3.Distance(_startNode.transform.position, _endNode.transform.position);

            _transferredProduct = Instantiate(_startNode.Products[0].gameObject, _startNode.transform.position, Quaternion.identity).GetComponent<Product>();

            SelectionManager.OnNodeSelected -= SetEndNode;
        }
    }

}
