using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    [SerializeField]
    private Transfer _transferPrefab;
    [SerializeField] 
    private Transform _transferParent;
    private Node _node;

    private void Awake() {
        SetEventCamera();
    }

    private void Start() {
        _node = transform.parent.GetComponent<Node>();
    }

    private void SetEventCamera() {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public void CloseUI() {
        gameObject.SetActive(false);
    }

    public void AddTransfer() {
        Transfer transfer = Instantiate(_transferPrefab, transform.parent.position, Quaternion.identity, _transferParent);
        transfer.StartNode = _node;
        //transfer.TransferredProduct = _node.Products[0];
        GameManager.State = GameManager.GameState.PlacingTransfer;
        CloseUI();
    }
}
