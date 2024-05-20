using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour {

    public Quality ProductQuality { get; private set; }
    
    public void SetProductProperties(Quality quality) {
        ProductQuality = quality;
    }
}

public enum Quality {
    Horrible,
    Bad,
    Normal,
    Good,
    Perfect
}
