using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    [Header("INSERT CHARACTER EQUIPMENT VARIABLES :")]
    public CharacterEquipment_SO characterEquipmentVariables;


    [Header("LOCAL VARIABLES :")]
    [TextArea(1, 3)]
    public string description;
    public List<WeaponInput> weaponBelt = new List<WeaponInput>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetWeapons()
    {
        switch (GetComponent<CharacterInput>().characterSetupVariables.characterClass)
        {
            case CharacterClass.support:
                weaponBelt.Add(characterEquipmentVariables.rifleWeaponPrefab);
                weaponBelt.Add(characterEquipmentVariables.gunWeaponPrefab);
                description = CharacterClass.support + " Class Weapon Belt";
                break;
            case CharacterClass.sniper:
                weaponBelt.Add(characterEquipmentVariables.sniperWeaponPrefab);
                weaponBelt.Add(characterEquipmentVariables.gunWeaponPrefab);
                description = CharacterClass.sniper + " Class Weapon Belt";
                break;
            case CharacterClass.assault:
                weaponBelt.Add(characterEquipmentVariables.rifleWeaponPrefab);
                weaponBelt.Add(characterEquipmentVariables.meleeWeaponPrefab);
                description = CharacterClass.assault + " Class Weapon Belt";
                break;
            case CharacterClass.heavy:
                weaponBelt.Add(characterEquipmentVariables.minigunWeaponPrefab);
                weaponBelt.Add(characterEquipmentVariables.gunWeaponPrefab);
                description = CharacterClass.heavy + " Class Weapon Belt";
                break;
            case CharacterClass.hero:
                weaponBelt.Add(characterEquipmentVariables.rifleWeaponPrefab);
                weaponBelt.Add(characterEquipmentVariables.meleeWeaponPrefab);
                weaponBelt.Add(characterEquipmentVariables.gunWeaponPrefab);
                description = CharacterClass.support + " Class Weapon Belt";
                break;
            case CharacterClass.undefined:
                weaponBelt.Add(characterEquipmentVariables.rifleWeaponPrefab);
                description = CharacterClass.undefined + " Class Weapon Belt";
                break;
            default:
                break;
        }

        characterEquipmentVariables.weaponBelt = weaponBelt;
    }

    public void LoadWeapons()
    {
        //GetComponent<CharacterInput>().characterSetupVariables
    }
}
