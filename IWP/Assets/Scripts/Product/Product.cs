using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour {

    public Quality ProductQuality { get; private set; }

    [SerializeField]
    private ProductType _type;
    public ProductType Type { get { return _type; } }

    [SerializeField]
    private Sprite _icon;
    public Sprite Icon { get { return _icon;  } }

    [SerializeField]
    private int _price;
    public int Price { get { return _price; } }

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
    Default,
    Money,
    KnifeBlade,
    KnifeHandle,
    Knife
}
