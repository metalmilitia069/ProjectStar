using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTurn : GroupableEntities
{
    [Header("CHARACTER TURN VARIABLES - INSTANCE :")]
    public CharacterTurn_SO characterTurnVariables;

    [Header("TURN SETUP")]
    [SerializeField]
    public int teamId;

    public override void ResetActionPoints()
    {
        characterTurnVariables.actionPoints = GetComponent<CharacterInput>().characterStatsVariables.maxActionPoints;
    }


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
