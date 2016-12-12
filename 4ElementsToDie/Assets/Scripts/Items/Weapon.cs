using UnityEngine;

[CreateAssetMenu (menuName = "Item > Equipment > Weapon")]
public class Weapon : Equipment {

    [Header("AttackType")]
    public AttackType attackType;
}
