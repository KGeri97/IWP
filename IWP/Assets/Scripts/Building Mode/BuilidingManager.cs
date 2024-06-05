using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    Warehouse,
    Assembly,
    Factory,
    Shop
}

public class BuilidingManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _buildings = new List<GameObject>();
    [SerializeField] private List<GameObject> _spawnLocations = new List<GameObject>();
    private List<GameObject> _ownedBuildings = new List<GameObject>();
        
    public GameObject buildingPanelUI;
    public GameObject buildingButtonUI;

    private bool _isInBuildingMode;

    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
        if (_isInBuildingMode)
        {
            
        }
        else
        {
            
        }
    }

    public void ToggleBuildingMode()
    {
        _isInBuildingMode = !_isInBuildingMode;
    }
}
