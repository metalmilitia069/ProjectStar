using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterGeometry", menuName = "ScriptableVariables/Type: CharacterGeometry")]
public class CharacterGeometry_SO : ScriptableObject
{
    public GameObject meleeWeaponHolster;
    public GameObject gunWeaponHolster;
    public GameObject rifleWeaponHolster;
    public GameObject minigunWeaponHolster;
    public GameObject sniperWeaponHolster;

    public GameObject handWeaponGripPoint;
    //Melee,
    //Gun,
    //Rifle,
    //MiniGun,
    //Sniper,
}
