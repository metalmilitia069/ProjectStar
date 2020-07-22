using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGlobalController : MonoBehaviour
{
    [Header("INSERT A TURN MANAGER SO :")]
    public TurnManager_SO TurnManager;    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CallSwitchCharacter();
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            CallOverWatch();
        }
    }

    private void CallSwitchCharacter()
    {
        if (TurnManager.playerTeamList.Count > 1)
        {
            foreach (var character in TurnManager.playerTeamList)
            {
                if (character.GetComponent<CharacterTurn>().characterTurnVariables.isTurnActive)
                {
                    TurnManager.SwitchCharacter(character.GetComponent<CharacterTurn>(), null);

                    //character.GetComponent<CharacterInput>().uiManager.weaponDisplayPanel.SetWeaponToDisplay();

                    character.GetComponent<CharacterInput>().MainCameraControllerVariables.LockCamera(character.transform);
                    break;
                }
            }
        }
    }

    public void CallOverWatch()
    {
        foreach (var character in TurnManager.playerTeamList)
        {
            if (character.GetComponent<CharacterTurn>().characterTurnVariables.isTurnActive)
            {                
                character.GetComponent<CharacterCombat>().PrepareOverWatch();
                break;
            }
        }
    }
}
