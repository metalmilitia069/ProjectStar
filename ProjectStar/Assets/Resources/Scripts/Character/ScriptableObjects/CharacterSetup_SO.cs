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

    private void OnDisable()
    {
        characterGender = Gender.underfined; //DELETE THIS WHEN IMPLEMENTING THE CHARACTER POOL!!!!!!!!!!!!!!!
        characterName = default; //DELETE THIS WHEN IMPLEMENTING THE CHARACTER POOL!!!!!!!!!!!!!!!
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

    public void SetCharacterName()
    {

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


