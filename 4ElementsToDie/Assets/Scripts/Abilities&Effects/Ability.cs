using UnityEngine;

//public enum BuffType { Positive, Negative };
public enum TriggerType { Passive, OnInflictedAttack, OnReceivedAttack, OnKill, OnDeath, OnEffectBuff, OnEffectDebuff, AboveStatPercentage, BelowStatPercentage };  //others?
public enum TargetType { Self, Enemy }

[CreateAssetMenu]
public class Ability : ScriptableObject {

    [Header("General")]
    public string abilityName;
    [Range(1, 3)]
    public int level;
    public string description;

    [Header("Trigger")]
    public TriggerType trigger;

    [Header("Target")]
    public TargetType target;

    // NOTE: ignored by all except AboveStatValue, BelowStatValue;
    [Header("Stat Treshold")]
    public StatType stat;
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
