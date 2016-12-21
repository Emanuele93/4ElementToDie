using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Character : ScriptableObject
{

    [Header("Sprite & Animations")]
    public Sprite sprite;
    public RuntimeAnimatorController idleAnimation;
    public RuntimeAnimatorController moveLeftRightAnimation;
    public RuntimeAnimatorController moveUpDownAnimation;
    
    [Header("General")]
    public string characterName;
    public ElementType element;
    public AttackType defaultAttackType;

    [Header("Stats")]
    [Range(0, 5)]
    public double[] baseStats = new double[System.Enum.GetValues(typeof(StatType)).Length];
    [Range(0, 2)]
    public double[] growingRatios = new double[System.Enum.GetValues(typeof(StatType)).Length];

    [Header("Items")]
    public Weapon weapon;
    public Armor armor;
    public Accessory accessory;
    public Garment garment;
    public Item[] inventory;

    [Header("Abilities")]
    public List<Ability> abilities;
}