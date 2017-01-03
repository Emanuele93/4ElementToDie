using UnityEngine;

public class Ability : ScriptableObject
{

    [Header("General")]
    public string abilityName;
    [Range(1, 3)]
    public int level;
    public string description;

    [Header("Effect")]
    public Effect effect;

}

