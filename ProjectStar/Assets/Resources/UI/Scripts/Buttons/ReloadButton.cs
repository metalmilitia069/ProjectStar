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
    void Start()
    {
        uiManager.reloadButton = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadWeapon()
    {
        int[] weaponArr = GetActiveCharacter().GetComponent<CharacterCombat>().GetCurrentWeapon().weaponBasicVariables.ReloadWeapon();

        uiManager.weaponDisplayPanel.bulletsDisplayImage.sprite = uiManager.DisplayBullets(weaponArr[0], weaponArr[1]);
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
