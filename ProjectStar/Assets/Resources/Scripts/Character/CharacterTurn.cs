using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTurn : MonoBehaviour
{
    [Header("CHARACTER TURN VARIABLES - INSTANCE :")]
    public CharacterTurn_SO characterTurnVariables;

    [Header("TURN SETUP")]
    [SerializeField]
    public int teamId;


    // Start is called before the first frame update
    void Start()
    {
        characterTurnVariables.teamId = teamId;

        if (teamId == 1)
        {
            GetComponent<CharacterInput>().TurnManager.SortActionOrder();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
