using UnityEngine;

[CreateAssetMenu]
public class Effect : ScriptableObject {

    [Header("General")]
    public string effectName;
    [Range (1,3)]
    public int level;

    /*
    [Header("Duration")]
    [Range(0, 10)]
    public double duration;  // in seconds
    [Range(1, 10)]
    public int no_ticks;    // how many times the buff/debuff repeats before expiring
    */

    [Header("Effect bonuses")]
    [Range(-5, 5)]
    public double[] statBuffs = new double[System.Enum.GetValues(typeof(StatType)).Length];

}
