using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInjection : MonoBehaviour
{
    [Header("INSERT PLAYER CHARACTERS LIST HERE:")]
    public CharacterList_SO listOfAllCharacters_SO;

    private void OnEnable()
    {

        GetComponent<CharacterMove>().characterMoveVariables = ScriptableObject.CreateInstance<CharacterMove_SO>();
        GetComponent<CharacterMove>().characterMoveVariables.name = "InstanceCharacterMove";

        GetComponent<CharacterStats>().characterStatsVariables = ScriptableObject.CreateInstance<CharacterStats_SO>();
        GetComponent<CharacterStats>().characterStatsVariables.name = "InstanceCharacterStats";


        GetComponent<CharacterInput>().characterMoveVariables = GetComponent<CharacterMove>().characterMoveVariables;
        GetComponent<CharacterInput>().characterStatsVariables = GetComponent<CharacterStats>().characterStatsVariables;


        listOfAllCharacters_SO.AddCharacter(this.GetComponent<CharacterInput>());


    }

    private void OnDisable()
    {
        //listOfAllCharacters_SO.RemoveTile(this.GetComponent<AdvancedTile>());
        listOfAllCharacters_SO.RemoveCharacter(this.GetComponent<CharacterInput>());
    }



    
}
