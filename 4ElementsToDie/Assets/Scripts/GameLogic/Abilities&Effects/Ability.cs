using UnityEngine;

//public enum BuffType { Positive, Negative };
public enum ConditionType { Passive, OnInflictedAttack , OnReceivedAttack, OnKill, OnDeath, OnEffectBuff, OnEffectDebuff, AboveStatPercentage, BelowStatPercentage};  //others?

[CreateAssetMenu]
public class Ability : ScriptableObject {

    [Header("General")]
    public string abilityName;
    [Range(1, 3)]
    public int level;

    [Header("Condition")]
    public ConditionType condition;
    
    // NOTE: ignored by all except AboveStatValue, BelowStatValue;
    [Header("Treshold (%)")]
    [Range(1, 100)]
    public int treshold;

    // NOTE: ignored if Passive;
    [Header("Probability (%)")]
    [Range(1, 100)]
    public int probability;

    [Header("Effect")]
    public Effect effect;

    //TODO: TARGETS

}
