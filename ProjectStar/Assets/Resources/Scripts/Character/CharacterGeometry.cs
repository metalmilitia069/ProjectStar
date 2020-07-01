using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGeometry : MonoBehaviour
{
    [Header("CHARACTER GEOMETRY VARIABLES - INSTANCE :")]
    public CharacterGeometry_SO CharacterGeometryVariables;

    [Header("LOCAL VARIABLES :")]
    public GameObject meleeWeaponHolster;
    public GameObject gunWeaponHolster;
    public GameObject rifleWeaponHolster;
    public GameObject minigunWeaponHolster;
    public GameObject sniperWeaponHolster;

    public GameObject handWeaponGripPoint;
    // Start is called before the first frame update
    void Start()
    {
        SetupBeltSockets();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupBeltSockets()
    {
        CharacterGeometryVariables.meleeWeaponHolster = meleeWeaponHolster;
        CharacterGeometryVariables.gunWeaponHolster = gunWeaponHolster;
        CharacterGeometryVariables.rifleWeaponHolster = rifleWeaponHolster;
        CharacterGeometryVariables.minigunWeaponHolster = minigunWeaponHolster;
        CharacterGeometryVariables.sniperWeaponHolster = sniperWeaponHolster;

        CharacterGeometryVariables.handWeaponGripPoint = handWeaponGripPoint;
}
}
