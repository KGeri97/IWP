using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuilidingManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _buildings = new List<GameObject>();
    [SerializeField] private List<GameObject> _spawnLocations = new List<GameObject>();
    private List<GameObject> _ownedBuildings = new List<GameObject>();
        
    public GameObject buildingPanelUI;
    public Image buildingButtonImage;

    private GameObject _currentSpawnLocationChosen;
    
    private bool _isInBuildingMode = false;

    void Start()
    {
        ToggleBuildingPanelUI(false);
        ToggleSpawnLocations(false);
        buildingButtonImage.color = Color.gray;
    }
    
    void Update()
    {
        if (_isInBuildingMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectSpawnLocation();
            }
        }
    }

    private void SelectSpawnLocation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            SphereCollider sphereCollider = hit.collider.GetComponent<SphereCollider>();
            if (sphereCollider != null && _spawnLocations.Contains(hit.collider.gameObject))
            {
                _currentSpawnLocationChosen = hit.collider.gameObject;
                ToggleBuildingPanelUI(true);
            }
        }
    }

    public void BuyBuilding(string type)
    {
        GameObject buildingToBuy = _buildings.FirstOrDefault(b => b.name == type);
        
        if (buildingToBuy != null)
        {
            Instantiate(buildingToBuy, _currentSpawnLocationChosen.transform.position, Quaternion.identity);
            _ownedBuildings.Add(buildingToBuy);
            _spawnLocations.Remove(_currentSpawnLocationChosen);
            _currentSpawnLocationChosen.SetActive(false);
            ToggleBuildingPanelUI(false);
        }
    }

    private void ToggleSpawnLocations(bool show)
    {
        foreach (var spawnLocation in _spawnLocations)
        {
            spawnLocation.SetActive(show);
        }
    }

    private void ToggleBuildingPanelUI(bool show)
    {
        buildingPanelUI.SetActive(show);
    }

    public void ToggleBuildingMode()
    {
        _isInBuildingMode = !_isInBuildingMode;
        ToggleSpawnLocations(_isInBuildingMode);

        if (_isInBuildingMode)
        {
            buildingButtonImage.color = Color.white;
        }
        else
        {
            buildingButtonImage.color = Color.gray;
        }
    }
}
