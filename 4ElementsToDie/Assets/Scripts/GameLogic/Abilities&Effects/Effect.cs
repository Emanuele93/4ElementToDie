using UnityEngine;

[CreateAssetMenu]
public class Effect : ScriptableObject
{

    [Header("General")]
    public string effectName;

    [Header("Effect bonuses (%)")]
    [Range(-100, 300)]
    public double[] statBuffs = new double[System.Enum.GetValues(typeof(StatType)).Length];
    [Range(-25, 25)]
    public double damage;

}
