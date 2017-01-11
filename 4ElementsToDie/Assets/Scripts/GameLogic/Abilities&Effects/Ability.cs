using UnityEngine;

public class Ability : ScriptableObject
{

    [Header("General")]
    public string abilityName;
    public string description;

    [Header("Effect")]
    public Effect effect;

}

