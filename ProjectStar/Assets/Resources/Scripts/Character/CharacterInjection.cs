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

        GetComponent<CharacterCombat>().characterCombatVariables = ScriptableObject.CreateInstance<CharacterCombat_SO>();
        GetComponent<CharacterCombat>().characterCombatVariables.name = "InstanceCharacterCombat";

        GetComponent<CharacterStats>().characterStatsVariables = ScriptableObject.CreateInstance<CharacterStats_SO>();
        GetComponent<CharacterStats>().characterStatsVariables.name = "InstanceCharacterStats";

        GetComponent<CharacterTurn>().characterTurnVariables = ScriptableObject.CreateInstance<CharacterTurn_SO>();
        GetComponent<CharacterTurn>().characterTurnVariables.name = "InstanceCharacterTurn";

        GetComponent<CharacterInput>().characterSavedData = GetComponent<CharacterSavedData>().savedPlayerCharacters_SO; //COPY, NOT AN INSTANCE!!!!!
        GetComponent<CharacterInput>().characterSetupVariables = GetComponent<CharacterSetup>().characterSetupVariables; //NOT AN INSTANCE!!!!
        GetComponent<CharacterInput>().characterMoveVariables = GetComponent<CharacterMove>().characterMoveVariables;
        GetComponent<CharacterInput>().characterEquipmentVariables = GetComponent<CharacterEquipment>().characterEquipmentVariables; //NOT AN INSTANCE!!!!
        
        //GetComponent<CharacterInput>().characterGeometryVariables = GetComponent<CharacterInput>().characterSetupVariables.characterGeometryReference.GetComponent<CharacterGeometry>().CharacterGeometryVariables; //NOT AN INSTANCE!!!!  
        
        GetComponent<CharacterInput>().characterCombatVariables = GetComponent<CharacterCombat>().characterCombatVariables;
        GetComponent<CharacterInput>().characterStatsVariables = GetComponent<CharacterStats>().characterStatsVariables;
        GetComponent<CharacterInput>().characterTurnVariables = GetComponent<CharacterTurn>().characterTurnVariables;


        //
        //GetComponent<CharacterMove>().GridManager = ScriptableObject.CreateInstance<GridManager_SO>();  //
        //GetComponent<CharacterInput>().GridManager = GetComponent<CharacterMove>().GridManager;  //        
        //


        listOfAllCharacters_SO.AddCharacter(this.GetComponent<CharacterTurn>());


    }

    private void Start()
    {        
        GetComponent<CharacterInput>().characterGeometryVariables = GetComponent<CharacterSetup>().characterSetupVariables.characterGeometryReference.GetComponent<CharacterGeometry>().CharacterGeometryVariables; //NOT AN INSTANCE!!!!                                                                    

        //GetComponent<CharacterMove>().characterMoveVariables = ScriptableObject.CreateInstance<CharacterMove_SO>();
        //GetComponent<CharacterMove>().characterMoveVariables.name = "InstanceCharacterMove";

        //GetComponent<CharacterCombat>().characterCombatVariables = ScriptableObject.CreateInstance<CharacterCombat_SO>();
        //GetComponent<CharacterCombat>().characterCombatVariables.name = "InstanceCharacterCombat";

        //GetComponent<CharacterStats>().characterStatsVariables = ScriptableObject.CreateInstance<CharacterStats_SO>();
        //GetComponent<CharacterStats>().characterStatsVariables.name = "InstanceCharacterStats";

        //GetComponent<CharacterTurn>().characterTurnVariables = ScriptableObject.CreateInstance<CharacterTurn_SO>();
        //GetComponent<CharacterTurn>().characterTurnVariables.name = "InstanceCharacterTurn";

        //GetComponent<CharacterInput>().characterSetupVariables = GetComponent<CharacterSetup>().characterSetupVariables; //NOT AN INSTANCE!!!!
        //GetComponent<CharacterInput>().characterMoveVariables = GetComponent<CharacterMove>().characterMoveVariables;
        //GetComponent<CharacterInput>().characterEquipmentVariables = GetComponent<CharacterEquipment>().characterEquipmentVariables; //NOT AN INSTANCE!!!!
        //GetComponent<CharacterInput>().characterGeometryVariables = GetComponent<CharacterInput>().characterSetupVariables.characterGeometryReference.GetComponent<CharacterGeometry>().CharacterGeometryVariables; //NOT AN INSTANCE!!!!                                                                    
        //GetComponent<CharacterInput>().characterCombatVariables = GetComponent<CharacterCombat>().characterCombatVariables;
        //GetComponent<CharacterInput>().characterStatsVariables = GetComponent<CharacterStats>().characterStatsVariables;
        //GetComponent<CharacterInput>().characterTurnVariables = GetComponent<CharacterTurn>().characterTurnVariables;


        ////
        ////GetComponent<CharacterMove>().GridManager = ScriptableObject.CreateInstance<GridManager_SO>();  //
        ////GetComponent<CharacterInput>().GridManager = GetComponent<CharacterMove>().GridManager;  //        
        ////


        //listOfAllCharacters_SO.AddCharacter(this.GetComponent<CharacterTurn>());
    }

    private void OnDisable()
    {
        //listOfAllCharacters_SO.RemoveTile(this.GetComponent<AdvancedTile>());
        //Debug.Log("mozo");
        listOfAllCharacters_SO.RemoveCharacter(this.GetComponent<CharacterTurn>());

    }



    
}
