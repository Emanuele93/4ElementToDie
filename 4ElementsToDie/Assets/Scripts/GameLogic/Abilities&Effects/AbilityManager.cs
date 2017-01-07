using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AbilityManager
{

    // Static abilities are activated once, when the relative condition is true; see CharacterManager
    public static void CheckStaticAbilitiesActivation(CharacterManager abilityPossessor)
    {
        if (abilityPossessor.Abilities != null)
        {
            foreach (StaticAbility a in abilityPossessor.Abilities.OfType<StaticAbility>())
            {
                bool shouldBeActive = false;
                bool isAlreadyActive = false;

                if (abilityPossessor.ActiveEffects != null)
                {
                    isAlreadyActive = abilityPossessor.ActiveEffects.Contains(a.effect);
                }

                switch (a.trigger)
                {
                    case StaticTriggerType.Passive:
                        shouldBeActive = true;
                        break;

                    case StaticTriggerType.AboveStatValue:
                        shouldBeActive = abilityPossessor.Stats[(int)a.statToCheck].FinalStat >= a.statTreshold;
                        break;

                    case StaticTriggerType.BelowStatValue:
                        shouldBeActive = abilityPossessor.Stats[(int)a.statToCheck].FinalStat <= a.statTreshold;
                        break;

                    case StaticTriggerType.AboveCurVitPercentage:
                        shouldBeActive = (100 * (1 - (abilityPossessor.Damage / abilityPossessor.Stats[(int)StatType.VIT].FinalStat))) >= a.currentVitalityTreshold;
                        break;

                    case StaticTriggerType.BelowCurVitPercentage:
                        shouldBeActive = (100 * (1 - (abilityPossessor.Damage / abilityPossessor.Stats[(int)StatType.VIT].FinalStat))) <= a.currentVitalityTreshold;
                        break;
                }

                if (shouldBeActive && !isAlreadyActive)
                {
                    abilityPossessor.AddActiveEffect(a.effect);
                }
                else if (!shouldBeActive && isAlreadyActive)
                {
                    abilityPossessor.RemoveActiveEffect(a.effect);
                }
            }
        }
    }

    // Event-triggered abilities can be activated multiple times, when the relative event happens; see GameplayManager.ExecuteAttack()
    public static void CheckTriggeredAbilitiesActivation(TriggeredTriggerType trigger, CharacterManager abilityPossessor, CharacterManager target)
    {
        if (abilityPossessor.Abilities != null)
        {
            foreach (TriggeredAbility a in abilityPossessor.Abilities.OfType<TriggeredAbility>())
            {
                if (a.trigger == trigger)
                {
                    bool shouldBeActive = false;

                    switch (trigger)
                    {
                        case TriggeredTriggerType.OnInflictedAttack:
                        case TriggeredTriggerType.OnReceivedAttack:
                        case TriggeredTriggerType.OnKill:
                        case TriggeredTriggerType.OnDeath:
                            shouldBeActive = Random.Range(0, 100) <= a.probability;
                            break;
                    }

                    if (shouldBeActive)
                    {
                        if (a.target == TargetType.Self)
                        {
                            abilityPossessor.StartCoroutine(abilityPossessor.TriggeredEffect(a));
                        }
                        else
                        {
                            target.StartCoroutine(target.TriggeredEffect(a));
                        }
                    }
                }
            }
        }
    }

}
