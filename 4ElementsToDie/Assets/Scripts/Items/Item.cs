using UnityEngine;

public abstract class Item : ScriptableObject {

    [Header("General")]
    public Sprite sprite;
    public string itemID;
    public string itemName;
    public string description;
    public int price;
    [Range(0, 100)]
    public double dropRate;

}
