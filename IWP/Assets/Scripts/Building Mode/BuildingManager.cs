using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _buildings = new List<GameObject>();
    [SerializeField] private List<GameObject> _spawnLocations = new List<GameObject>();
    private List<GameObject> _ownedBuildings = new List<GameObject>();

    public Material selectedMaterial, previousMaterial;
        
    public GameObject buildingPanelUI;
    public Image buildingButtonImage;

    private GameObject _currentSpawnLocationChosen;
    
    private bool _isInBuildingMode = false;
    
    public static event Action<GameObject> OnBuildingUIOpened;

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
    
    private void OnEnable() 
    {
        SelectionManager.OnNodeSelected += HandleTogglingBuildingPanelUI;
    }

    private void OnDisable()
    {
        SelectionManager.OnNodeSelected -= HandleTogglingBuildingPanelUI;
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
                if (_currentSpawnLocationChosen != null)
                {
                    _currentSpawnLocationChosen.GetComponent<MeshRenderer>().material = previousMaterial;
                }
                
                _currentSpawnLocationChosen = hit.collider.gameObject;
                _currentSpawnLocationChosen.GetComponent<MeshRenderer>().material = selectedMaterial;
                ToggleBuildingPanelUI(true);
                OnBuildingUIOpened?.Invoke(buildingPanelUI);
            }
        }
    }

    public void BuyBuilding(string type)
    {
        GameObject buildingToBuy = _buildings.FirstOrDefault(b => b.name == type);
        
        if (buildingToBuy != null)
        {
            Vector3 editedSpawnLocation = new Vector3(_currentSpawnLocationChosen.transform.position.x,
                _currentSpawnLocationChosen.transform.position.y + .5f,
                _currentSpawnLocationChosen.transform.position.z);
            
            Instantiate(buildingToBuy, editedSpawnLocation, Quaternion.identity);
            _ownedBuildings.Add(buildingToBuy);
            _spawnLocations.Remove(_currentSpawnLocationChosen);
            _currentSpawnLocationChosen.SetActive(false);
            ToggleBuildingPanelUI(false);
            _currentSpawnLocationChosen = null;
        }
    }

    private void ToggleSpawnLocations(bool show)
    {
        foreach (var spawnLocation in _spawnLocations)
        {
            spawnLocation.SetActive(show);
        }
    }

    private void HandleTogglingBuildingPanelUI(GameObject obj)
    {
        ToggleBuildingPanelUI(obj.GetComponent<Node>().GetUIState());
    }

    private void ToggleBuildingPanelUI(bool show)
    {
        buildingPanelUI.SetActive(show);
    }

    public void ToggleBuildingMode()
    {
        _isInBuildingMode = !_isInBuildingMode;
        ToggleSpawnLocations(_isInBuildingMode);
        
        if (buildingPanelUI)
            ToggleBuildingPanelUI(false);

        if (_isInBuildingMode)
        {
            buildingButtonImage.color = Color.white;
        }
        else
        {
            buildingButtonImage.color = Color.gray;

            if (_currentSpawnLocationChosen != null)
                _currentSpawnLocationChosen.GetComponent<MeshRenderer>().material = previousMaterial;
            
            _currentSpawnLocationChosen = null;
        }
    }

    public void CloseUI()
    {
        ToggleBuildingPanelUI(false);
    }
}
