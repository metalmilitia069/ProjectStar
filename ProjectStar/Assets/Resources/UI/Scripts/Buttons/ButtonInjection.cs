using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInjection : MonoBehaviour
{
    [Header("INSERT TURN MANAGER SO :")]
    public TurnManager_SO TurnManager;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
