using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState {
        Running,
        Selecting
    }

    public GameState State = GameState.Running;
}
