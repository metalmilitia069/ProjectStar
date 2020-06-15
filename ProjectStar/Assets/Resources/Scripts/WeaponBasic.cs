using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeaponBasic : MonoBehaviour
{
    [Header("BASIC WEAPON VARIABLES - INSTANCE :")]
    public WeaponBasic_SO weaponBasicVariables;


    [Header("WEAPON INITIAL SETUP : ")]
    public WeaponClass weaponClass;
    // Start is called before the first frame update
    void Start()
    {
        weaponBasicVariables.weaponClass = weaponClass;
        weaponBasicVariables.WeaponBasicSetup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
