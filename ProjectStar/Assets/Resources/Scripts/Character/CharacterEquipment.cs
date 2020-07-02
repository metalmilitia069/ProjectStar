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
        GetWeapons();
        //LoadWeaponsIntoCharacterGeometryBelt();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetWeapons()
    {
        CharacterGeometry_SO charGeo = GetComponent<CharacterInput>().characterSetupVariables.characterGeometryReference.GetComponent<CharacterGeometry>().CharacterGeometryVariables;

        weaponBelt.Clear();
        characterEquipmentVariables.weaponBelt.Clear();

        switch (GetComponent<CharacterInput>().characterSetupVariables.characterClass)
        {
            case CharacterClass.support:
                weaponBelt.Add(Instantiate(characterEquipmentVariables.rifleWeaponPrefab, charGeo.rifleWeaponHolster.transform));//characterEquipmentVariables.rifleWeaponPrefab);
                //Instantiate(weaponBelt[0], charGeo.rifleWeaponHolster.transform);

                weaponBelt.Add(Instantiate(characterEquipmentVariables.gunWeaponPrefab, charGeo.gunWeaponHolster.transform));
                //Instantiate(weaponBelt[1], charGeo.gunWeaponHolster.transform);

                description = CharacterClass.support + " Class Weapon Belt";
                break;
            case CharacterClass.sniper:
                weaponBelt.Add(Instantiate(characterEquipmentVariables.sniperWeaponPrefab, charGeo.sniperWeaponHolster.transform));

                weaponBelt.Add(Instantiate(characterEquipmentVariables.gunWeaponPrefab, charGeo.gunWeaponHolster.transform));

                description = CharacterClass.sniper + " Class Weapon Belt";
                break;
            case CharacterClass.assault:
                weaponBelt.Add(Instantiate(characterEquipmentVariables.rifleWeaponPrefab, charGeo.rifleWeaponHolster.transform));

                weaponBelt.Add(Instantiate(characterEquipmentVariables.meleeWeaponPrefab, charGeo.meleeWeaponHolster.transform));

                description = CharacterClass.assault + " Class Weapon Belt";
                break;
            case CharacterClass.heavy:
                weaponBelt.Add(Instantiate(characterEquipmentVariables.minigunWeaponPrefab, charGeo.minigunWeaponHolster.transform));
                
                weaponBelt.Add(Instantiate(characterEquipmentVariables.gunWeaponPrefab, charGeo.gunWeaponHolster.transform));

                description = CharacterClass.heavy + " Class Weapon Belt";
                break;
            case CharacterClass.hero:
                weaponBelt.Add(Instantiate(characterEquipmentVariables.rifleWeaponPrefab, charGeo.rifleWeaponHolster.transform));
                weaponBelt.Add(Instantiate(characterEquipmentVariables.meleeWeaponPrefab, charGeo.meleeWeaponHolster.transform));
                weaponBelt.Add(Instantiate(characterEquipmentVariables.gunWeaponPrefab, charGeo.gunWeaponHolster.transform));
                description = CharacterClass.support + " Class Weapon Belt";
                break;
            case CharacterClass.undefined:
                weaponBelt.Add(Instantiate(characterEquipmentVariables.rifleWeaponPrefab, charGeo.rifleWeaponHolster.transform));
                description = CharacterClass.undefined + " Class Weapon Belt";
                break;
            default:
                break;
        }

        characterEquipmentVariables.weaponBelt = weaponBelt;
    }

    public void LoadWeaponsIntoCharacterGeometryBelt()
    {
        CharacterGeometry_SO charGeo = GetComponent<CharacterInput>().characterSetupVariables.characterGeometryReference.GetComponent<CharacterGeometry>().CharacterGeometryVariables;

        foreach (var weapon in characterEquipmentVariables.weaponBelt)
        {
            switch (weapon.GetComponent<WeaponBasic>().weaponBasicVariables.weaponClass)
            {
                case WeaponClass.Melee:
                    Instantiate(weapon, charGeo.meleeWeaponHolster.transform);
                    break;
                case WeaponClass.Gun:
                    Instantiate(weapon, charGeo.gunWeaponHolster.transform);
                    break;
                case WeaponClass.Rifle:
                    Instantiate(weapon, charGeo.rifleWeaponHolster.transform);
                    break;
                case WeaponClass.MiniGun:
                    Instantiate(weapon, charGeo.minigunWeaponHolster.transform);
                    break;
                case WeaponClass.Sniper:
                    Instantiate(weapon, charGeo.sniperWeaponHolster.transform);
                    break;
                default:
                    break;
            }
        }
    }
}
