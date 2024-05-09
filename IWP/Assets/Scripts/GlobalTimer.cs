using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    float multipled;
    public int multiplier;
    int minutes;
    int seconds;

    void Start() {
        multiplier = 1;
    }

   public void xMultiplier(int value)
    {
        multiplier = value;
       
    }
    
    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime*multiplier;
        
        minutes = Mathf.FloorToInt(elapsedTime / 60);
        seconds = Mathf.FloorToInt(elapsedTime % 60);
        
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
