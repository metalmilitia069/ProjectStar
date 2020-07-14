using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSetup : MonoBehaviour
{
    [Header("INSERT CHARACTER SETUP VARIABLES :")]
    public CharacterSetup_SO characterSetupVariables;

    [TextArea(1, 10)]
    public string Description = "SETUP CHARACTER BASIC CONFIGURATION";

    
    public string characterName = default;    
    public string callSign = default;
    [Range(1,30)]
    public int characterLevel;
    public Gender characterGender = Gender.underfined;
    public CharacterClass characterClass = CharacterClass.undefined;
    public GameObject characterGeometryPoint;
    public Sprite characterAfiliationSigil;



    private void Awake()
    {
        
        GetCharacterAffiliationSigil();

        GetCharacterGeometry();
        GetCharacterClass();

        GetCharacterName();
        GetCharacterLevel();

        GetCharacterCallSign();

    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    GetCharacterGeometry();
    //    GetCharacterClass();

    //    GetCharacterName();
    //    GetCharacterLevel();
    //}

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

        characterSetupVariables.characterGeometryReference = Instantiate(characterSetupVariables.SetCharacterGeometry(), characterGeometryPoint.transform); 

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
        if (characterSetupVariables.characterName == string.Empty)
        {
            if (characterName.Length > 20)
            {
                characterName = characterName.Substring(0, 19);
            }
            characterSetupVariables.characterName = characterName;            
        }
        else
        {
            characterName = characterSetupVariables.characterName;
        }
    }

    public void GetCharacterLevel()
    {
        if (characterSetupVariables.characterLevel == 1)
        {
            characterSetupVariables.characterLevel = characterLevel;
        }
        else
        {
            characterLevel = characterSetupVariables.characterLevel;
        }
    }

    public void GetCharacterAffiliationSigil()
    {
        characterSetupVariables.characterAfiliationSigilReference = characterAfiliationSigil;
    }

    public void GetCharacterCallSign()
    {
        if (characterSetupVariables.callSign == string.Empty)
        {
            if (callSign.Length > 18)
            {
                callSign = callSign.Substring(0, 17);            
            }
            callSign = "'" + callSign + "'";
            characterSetupVariables.callSign = callSign;
        }
        else
        {
            callSign = characterSetupVariables.callSign;
        }
    }


}
