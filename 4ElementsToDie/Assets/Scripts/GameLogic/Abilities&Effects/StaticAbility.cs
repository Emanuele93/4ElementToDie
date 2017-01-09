using UnityEngine;

public enum StaticTriggerType { Passive, AboveStatValue, BelowStatValue, AboveCurVitPercentage, BelowCurVitPercentage };  //others?

[CreateAssetMenu]
public class StaticAbility : Ability
{

    [Header("Trigger")]
    public StaticTriggerType trigger;

    [Header("Treshold")]
    public StatType statToCheck;
    [Range(0, 10)]
    public int statTreshold;
    [Range(0, 100)]
    public int currentVitalityTreshold;

}
