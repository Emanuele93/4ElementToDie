using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using POLIMIGameCollective;

public class AbilityManager : Singleton<AbilityManager>
{

    public void CheckAbilityActivation(TriggerType trigger, CharacterManager character, CharacterManager target = null)
    {
        for (int i = 0; i < character.Abilities.Count; i++)
        {
            if (character.Abilities[i].trigger == trigger)
            {
                bool isActive = false;
                switch (trigger)
                {
                    case TriggerType.Passive:
                        // automatically managed on Ability gain
                        break;
                    case TriggerType.OnInflictedAttack:
                    case TriggerType.OnReceivedAttack:
                    case TriggerType.OnKill:
                    case TriggerType.OnDeath:
                    case TriggerType.OnEffectBuff:
                    case TriggerType.OnEffectDebuff:
                        if (Random.Range(0, 100) <= character.Abilities[i].probability)
                        {
                            isActive = true;
                        }
                        break;
                    case TriggerType.AboveStatPercentage:
                        if (character.Stats[(int)character.Abilities[i].stat].FinalStat >= character.Abilities[i].treshold
                            && Random.Range(0, 100) <= character.Abilities[i].probability)
                        {
                            isActive = true;
                        }
                        break;
                    case TriggerType.BelowStatPercentage:
                        if (character.Stats[(int)character.Abilities[i].stat].FinalStat <= character.Abilities[i].treshold
                            && Random.Range(0, 100) <= character.Abilities[i].probability)
                        {
                            isActive = true;
                        }
                        break;
                }
                if (isActive)
                {
                    switch (character.Abilities[i].target)
                    {
                        case TargetType.Self:
                            character.AddActiveEffect(character.Abilities[i].effect);
                            break;
                        case TargetType.Enemy:
                            target.AddActiveEffect(character.Abilities[i].effect);
                            break;
                    }
                }
            }
        }
    }
}
