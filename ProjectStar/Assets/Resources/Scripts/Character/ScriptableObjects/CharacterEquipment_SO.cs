using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterEquipment", menuName = "ScriptableVariables/Type: CharacterEquipment")]
public class CharacterEquipment_SO : ScriptableObject
{




    //LEAVE JUST ONE GEOMETRY LATER, WHEN CREATING POOLS AND SPAWNMANAGER!!!!!!!!!!
    public WeaponInput meleeWeaponPrefab;
    public WeaponInput gunWeaponPrefab;
    public WeaponInput rifleWeaponPrefab;
    public WeaponInput sniperWeaponPrefab;
    public WeaponInput minigunWeaponPrefab;

    public List<WeaponInput> weaponBelt = new List<WeaponInput>();
    public Dictionary<WeaponClass, WeaponInput> dicWeaponBelt = new Dictionary<WeaponClass, WeaponInput>();
    

    private void OnDisable()
    {
        weaponBelt.Clear(); // REVIEW THIS WHEN CREATING THE POOLS!!!!!!!
        dicWeaponBelt.Clear(); // REVIEW THIS WHEN CREATING THE POOLS!!!!!!!
    }
}
