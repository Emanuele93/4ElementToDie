using UnityEngine;

public abstract class Item : ScriptableObject
{

    public enum Rarity { Common, Uncommon, Rare, Epic, Legendary }

    [Header("General")]
    public Sprite sprite;
    public string itemID;
    public string itemName;
    public Rarity rarity;
    public string description;
    public int price;
    [Range(0, 100)]
    public double dropRate;

}
