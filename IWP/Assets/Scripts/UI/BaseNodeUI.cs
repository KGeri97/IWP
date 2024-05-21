using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNodeUI : MonoBehaviour
{
    [SerializeField] private Transfer _transferPrefab;
    [SerializeField] private Transform _transferParent;
    private Node _node;
    
    private Camera _camera;
    private Vector3 _initialPosition;
    private float _initialOrthographicSize;
    private float _minOrthographicSize = 3.0f, _maxOrthographicSize = 7.0f;
    
    void Awake()
    {
        SetEventCamera();
    }

    void Start()
    {
        _node = transform.parent.GetComponent<Node>();
        
        _initialPosition = transform.position;
        _initialOrthographicSize = _camera.orthographicSize;
    }

    void Update()
    {
        ScaleUIWithCamera();
    }

    private void SetEventCamera()
    {
        _camera = Camera.main;
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void ScaleUIWithCamera()
    {
        if (_camera != null && _initialOrthographicSize != 0) 
        {
            float scaleFactor = _camera.orthographicSize / _initialOrthographicSize;
            transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

            float t = (_camera.orthographicSize - _minOrthographicSize) / (_maxOrthographicSize - _minOrthographicSize);
            float newY = Mathf.Lerp(3f, 7f, t);
            transform.position = new Vector3(_initialPosition.x, newY, _initialPosition.z);
        }
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public void AddTransfer()
    {
        Transfer transfer = Instantiate(_transferPrefab, transform.parent.position, Quaternion.identity, _transferParent);
        transfer.StartNode = _node;
        transfer.TransferredProduct = _node.Products[0];
        GameManager.State = GameManager.GameState.PlacingTransfer;
        CloseUI();
    }
}
