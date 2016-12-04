using UnityEngine;
using System.Collections;

//Global enums, accessible from everywhere
public enum ElementType { Fire, Earth, Water, Air };
public enum StatType { ATT, DEF, VIT, SPD, ATTSpd, RES, LCK, ATTRng, FirePOW, EarthPOW, WaterPOW, AirPOW };
public enum EquipType { Weapon, Armor, Accessory, Garment }
public enum AttackType { Slashing, Thrusting, Area, Ranged }

public static class GameLogicManager {
    
    [Header("Basic Characters")]
    public static Character[] playableCharacters;

    //Singleton, non static version
    #region Singleton
    /*
    public static GameLogicManager Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    */
    #endregion

    private static double CalculateElementalFactor (CharacterManager attacker, CharacterManager defender)
    {
        double a = 0.0;
        double d = 0.0;
        double n = 0.0;

        for (int i = 0; i < System.Enum.GetValues(typeof(ElementType)).Length; i++)
        {
            a += attacker.Stats[(int)(StatType.FirePOW + i)].FinalStat;
            d += defender.Stats[(int)(StatType.FirePOW + i)].FinalStat;
            for (int j = 0; j < System.Enum.GetValues(typeof(ElementType)).Length; j++)
            {
                n +=
                    Constants.ELEMENTS_TABLE[i, j] *
                    attacker.Stats[ (int)(StatType.FirePOW) + i ].FinalStat *
                    defender.Stats[ (int)(StatType.FirePOW) + j ].FinalStat;
            }
        }
        return n / (a * d);
    }
    
    public static double CalculateDamage(CharacterManager attacker, CharacterManager defender)
    {
        double effectiveAttack = attacker.Stats[(int)StatType.ATT].FinalStat;
        double effectiveDefense = defender.Stats[(int)StatType.DEF].FinalStat;
        double elementalFactor = CalculateElementalFactor(attacker, defender);
        return elementalFactor * (effectiveAttack / effectiveDefense);
    }
}
