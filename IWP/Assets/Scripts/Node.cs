using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    protected List<Transfer> _transfers;
    [SerializeField] protected List<Product> _products;
    private GameManager _manager;

    private void Start() {
        _manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            TestFunction();
        }


        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Node selectedNode = hit.transform.gameObject.GetComponent<Node>();
                if (selectedNode == this) {
                    if (_manager.State == GameManager.GameState.Running) {
                        Debug.Log("ASDAD");
                    }
                }
            }
        }
    }

    public virtual void TestFunction() {
        Debug.Log("I am a node");
    }
}
