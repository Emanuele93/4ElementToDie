using UnityEngine;

public enum TriggeredTriggerType { OnInflictedAttack, OnReceivedAttack, OnKill, OnDeath };  //others?
public enum TargetType { Self, Enemy }

[CreateAssetMenu]
public class TriggeredAbility : Ability
{

    [Header("Trigger")]
    public TriggeredTriggerType trigger;

    [Header("Target")]
    public TargetType target;

    [Header("Probability (%)")]
    [Range(1, 100)]
    public int probability;

    [Header("Duration (seconds)")]
    [Range(1, 60)]
    public int duration;
    public int noTicks;
    public int secondsBetweenTicks;

}
