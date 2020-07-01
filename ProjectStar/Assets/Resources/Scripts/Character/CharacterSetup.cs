using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSetup : MonoBehaviour
{
    [Header("CHARACTER SETUP VARIABLES - INSTANCE :")]
    public CharacterSetup_SO characterSetupVariables;

    [TextArea(1, 10)]
    public string Description = "SETUP CHARACTER BASIC CONFIGURATION";

    public string characterName = default;
    [Range(1,30)]
    public int characterLevel;
    public Gender characterGender = Gender.underfined;
    public CharacterClass characterClass = CharacterClass.undefined;
    public GameObject characterGeometryPoint;

    

    // Start is called before the first frame update
    void Start()
    {
        GetCharacterGeometry();

        GetCharacterName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetCharacterGeometry()
    {
        if (characterSetupVariables.characterGender == Gender.underfined)
        {
            characterSetupVariables.characterGender = characterGender;
        }
        else
        {
            characterGender = characterSetupVariables.characterGender;
        }

        Instantiate(characterSetupVariables.SetCharacterGeometry(), characterGeometryPoint.transform); 

    }

    public void GetCharacterClass()
    {
        if (characterSetupVariables.characterClass == CharacterClass.undefined)
        {
            characterSetupVariables.characterClass = characterClass;
        }
        else
        {
            characterClass = characterSetupVariables.characterClass;
        }
    }

    public void GetCharacterName()
    {
        if (characterSetupVariables.characterName == default)
        {
            characterSetupVariables.characterName = characterName;
        }
        else
        {
            characterName = characterSetupVariables.characterName;
        }
    }


}
