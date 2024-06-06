using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomerManager : MonoBehaviour {
    public static CustomerManager Instance { get; private set; }

    [SerializeField]
    private float _purchaseFrequency;
    private Timer _purchaseTimer;

    public static Action ItemPurchased;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("There is already a CustomerManager instance");

        _purchaseTimer = new(_purchaseFrequency, ItemBought);
        _purchaseTimer.Repeat(true);
        _purchaseTimer.Start();
    }

    private void Update() {
        _purchaseTimer.Update();
    }

    private void ItemBought() {
        ItemPurchased?.Invoke();
    }
}
