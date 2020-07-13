using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSetup", menuName = "ScriptableVariables/Type: CharacterSetup")]
public class CharacterSetup_SO : ScriptableObject
{
    public string characterName = default;
    [Range(1, 30)]
    public int characterLevel = 1;
    public Gender characterGender = Gender.underfined;
    public CharacterClass characterClass = CharacterClass.undefined;

    //LEAVE JUST ONE GEOMETRY LATER, WHEN CREATING POOLS AND SPAWNMANAGER!!!!!!!!!!
    public GameObject characterGeometryMale;
    public GameObject characterGeometryFemale;

    //LEAVE JUST ONE CLASS SIGIL SPRITE LATER, WHEN CREATING POOLS AND SPAWNMANAGER!!!!!!!!!!
    public Sprite assaultSigil;
    public Sprite sniperSigil;
    public Sprite heavySigil;
    public Sprite supportSigil;
    public Sprite heroSigil;

    [Header("CHARACTER GEOMETRY REFERENCE :")]
    public GameObject characterGeometryReference;

    [Header("CHARACTER AFFILIATION SIGIL REFERENCE :")]
    public Sprite characterAfiliationSigilReference;
    [Header("CHARACTER AFFILIATION SIGIL REFERENCE :")]
    public Sprite characterClassSigilReference;


    private void OnDisable()
    {
        characterGender = Gender.underfined; //DELETE THIS WHEN IMPLEMENTING THE CHARACTER POOL!!!!!!!!!!!!!!!
        characterClass = CharacterClass.undefined; //DELETE THIS WHEN IMPLEMENTING THE CHARACTER POOL!!!!!!!!!!!!!!!
        characterName = default; //DELETE THIS WHEN IMPLEMENTING THE CHARACTER POOL!!!!!!!!!!!!!!!
        characterLevel = 1; //DELETE THIS WHEN IMPLEMENTING THE CHARACTER POOL!!!!!!!!!!!!!!!

        characterGeometryReference = default;
    }

    public void ResetVariables()
    {
        characterGender = Gender.underfined; 
        characterClass = CharacterClass.undefined;
        characterName = default;
        characterLevel = 1; 
    }

    public GameObject SetCharacterGeometry()
    {
        switch (characterGender)
        {
            case Gender.male:
                return characterGeometryMale;                
            case Gender.female:
                return characterGeometryFemale;                
            case Gender.underfined:
                Debug.Log("Gender Undefined");
                break;
        }

        return characterGeometryMale;
    }
}

public enum Gender
{
    male,
    female,
    underfined,
}

public enum CharacterClass
{
    support,
    sniper,
    assault,
    heavy,
    hero,
    undefined,
}


