using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUnitsTurnButton : MonoBehaviour
{
    [Header("INSERT TURN MANAGER SO :")]
    public TurnManager_SO TurnManager;
    [Header("INSERT UI MANAGER SO :")]
    public UIManager_SO uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager.endUnitsTurnButton = this;
    }

    public void EndUnitsTurn()
    {
        TurnManager.EndUnitsTurn(GetActiveCharacter());
        //uiManager.spawnedCrossSignUI.gameObject.SetActive(false);
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
