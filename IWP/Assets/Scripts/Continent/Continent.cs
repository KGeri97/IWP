using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

/// <summary>
/// NOTE: using names to allow players to buy building, need to change when it gets bigger, using tags or enums
/// </summary>

public class Continent : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnLocation = new List<Transform>();
    [SerializeField] private List<GameObject> _buildings = new List<GameObject>();
    private List<GameObject> _ownedBuildings = new List<GameObject>();
    [SerializeField] private GameObject _panel;
    private SpriteRenderer _spriteRenderer;
    private LayerMask _uiLayer = 5;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void CloseUI()
    {
        _panel.SetActive(false);
    }

    public void BuyBuilding(string buildingName)
    {
        GameObject buildingToBuy = _buildings.FirstOrDefault(b => b.name == buildingName);

        if (buildingToBuy != null)
        {
            int random = Random.Range(0, _spawnLocation.Count);
        
            //Debug.Log($"buying {buildingName}");
            _ownedBuildings.Add(Instantiate(buildingToBuy, _spawnLocation[random]));
            _spawnLocation.RemoveAt(random);
        }
        
        CloseUI();
    }

    void OnMouseOver()
    {
        _spriteRenderer.color = Color.green;

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
        {
            _panel.SetActive(!_panel.activeSelf);
        }
    }

    void OnMouseExit()
    {
        _spriteRenderer.color = Color.white;
    }
    
    private bool IsPointerOverUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
