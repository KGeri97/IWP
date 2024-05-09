using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public enum GameState {
        Running,
        Selecting
    }

    public GameState State = GameState.Running;

    public List<Node> SelectedNodes;

    private void Awake() {
        if (!Instance) {
            Instance = this;
        }
        else {
            Debug.LogError("GameManager already has an instance");
        }
    }

    private void OnEnable() {
        SelectionManager.OnObjectSelected += AddNode;
    }

    private void OnDisable() {
        SelectionManager.OnObjectSelected -= AddNode;
    }

    public void AddNode(GameObject gameObject) {
        if (State == GameState.Running) {
            SelectedNodes.Clear();
        }
        Debug.Log($"The selected object is {gameObject.name}");

        Node node = gameObject.GetComponent<Node>();
        if (!node)
            return;

        SelectedNodes.Add(node);
        Debug.Log("Added to the list");
    }
}
