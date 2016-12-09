using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Character : ScriptableObject
{

    [Header("Element")]
    public ElementType element;

    [Header("AttackType")]
    public AttackType defaultAttackType;

    [Header("Stats")]
    [Range(0, 5)]
    public double[] baseStats = new double[System.Enum.GetValues(typeof(StatType)).Length];
    [Range(0, 2)]
    public double[] growingRatios = new double[System.Enum.GetValues(typeof(StatType)).Length];

    [Header("Items")]
    public Equipment[] equipments;
    public Item[] inventory;

    [Header("Abilities")]
    public List<Ability> abilities;
}