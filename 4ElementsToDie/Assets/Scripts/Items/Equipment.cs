using UnityEngine;
using System.Collections.Generic;

public abstract class Equipment : Item {

    public enum Rarity { Common, Uncommon, Rare, Epic, Legendary }
    [Header("General")]
    public Rarity rarity;

    [Header("Element")]
    public ElementType element;

    [Header("Buffs")]
    [Range(-10, 10)]
    public double[] statBuffs = new double[System.Enum.GetValues(typeof(StatType)).Length];

    [Header("Abilities")]
    public List<Ability> abilities;

}
