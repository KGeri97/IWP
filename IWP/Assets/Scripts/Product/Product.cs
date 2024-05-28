using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour {

    public Quality ProductQuality { get; private set; }

    public ProductType Type { get; private set; }

    private void Awake() {
        Type = ProductType.Money;
    }

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

public enum ProductType {
    Money,
    KnifeBlade,
    KnifeHandle,
    Knife
}
