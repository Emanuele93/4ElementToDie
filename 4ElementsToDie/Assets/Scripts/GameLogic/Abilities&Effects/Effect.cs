using UnityEngine;

[CreateAssetMenu]
public class Effect : ScriptableObject
{

    [Header("General")]
    public string effectName;
    [Range(1, 3)]
    public int level;

    [Header("Effect bonuses (%)")]
    [Range(-100, 300)]
    public double[] statBuffs = new double[System.Enum.GetValues(typeof(StatType)).Length];
    [Range(-10, 10)]
    public double damage;

}
