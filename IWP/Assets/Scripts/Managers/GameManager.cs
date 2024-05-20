using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public enum GameState {
        Running,
        PlacingTransfer
    }

    public static GameState State = GameState.Running;

    private void Awake() {
        if (!Instance) {
            Instance = this;
        }
        else {
            Debug.LogError("GameManager already has an instance");
        }
    }
}
