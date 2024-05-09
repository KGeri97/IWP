using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }
    [SerializeField] private LayerMask _selectableLayers;

    public static event Action<GameObject> OnObjectSelected;

    private void Start() {
        if (!Instance) {
            Instance = this;
        }
        else {
            Debug.LogError("There is already a SelectionManager Instacne!");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, _selectableLayers)) {
                Debug.Log("Hit");
                OnObjectSelected?.Invoke(hit.transform.gameObject);
            }
        }
    }
}
