using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    protected List<Transfer> _transfers;
    [SerializeField] protected List<Product> _products;
    [SerializeField] private bool _available = true;
    private MeshRenderer _renderer;

    [SerializeField] Color _colorAvailable;
    [SerializeField] Color _colorUnavailable;

    [SerializeField] private GameObject _ui;

    private void Awake() {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void Start() {
        _ui.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void OnEnable() {
        SelectionManager.OnObjectSelected += SetUIInactive;
    }

    private void OnDisable() {
        SelectionManager.OnObjectSelected -= SetUIInactive;
    }

    private void Update() {
        if (_available)
            _renderer.material.color = _colorAvailable;
        else
            _renderer.material.color = _colorUnavailable;
    }

    public virtual void SetUI(bool active) {
        _ui.SetActive(active);
    }

    private void SetUIInactive(GameObject gameObject) {
        _ui.SetActive(false);
    }

    public virtual void TestFunction(){ 
    
    }
}
