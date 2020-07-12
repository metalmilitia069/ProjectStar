using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadButton : MonoBehaviour
{
    [Header("INSERT TURN MANAGER SO :")]
    public TurnManager_SO TurnManager;
    [Header("INSERT UI MANAGER SO :")]
    public UIManager_SO uiManager;

    // Start is called before the first frame update
    private void Awake()
    {
        uiManager.reloadButton = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadWeapon()
    {
        CharacterInput characterInput = GetActiveCharacter();
        int[] weaponArr = characterInput.GetComponent<CharacterCombat>().GetCurrentWeapon().weaponBasicVariables.ReloadWeapon();

        if (weaponArr != null)
        {
            uiManager.weaponDisplayPanel.bulletsDisplayImage.sprite = uiManager.DisplayBullets(weaponArr[0], weaponArr[1]);

            characterInput.characterTurnVariables.actionPoints--;

            if (characterInput.characterTurnVariables.actionPoints < 1)
            {
                if (characterInput.characterMoveVariables._isCombatMode)
                {
                    characterInput.ChangeMode();
                }

                TurnManager.RemoveFromTurn(characterInput.GetComponent<CharacterTurn>(), null);                
            }
        }



    }

    public CharacterInput GetActiveCharacter()
    {
        foreach (var groupableEntity in TurnManager.listOfAllCharacters.GetList())
        {
            if (groupableEntity.GetComponent<CharacterInput>().characterTurnVariables.isTurnActive)
            {
                return groupableEntity.GetComponent<CharacterInput>();
            }
        }

        return null;
    }
}
