using UnityEngine;

public abstract class Item : ScriptableObject {

    [Header("General")]
    public string itemName;
    public string itemID;
    public string description;
    public int price;

}
