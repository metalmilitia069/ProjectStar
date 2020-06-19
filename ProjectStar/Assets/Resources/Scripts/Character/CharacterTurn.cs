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
        //if (this.characterTurnVariables.isTurnActive)
        //{
        //    if (this.GetComponent<CharacterMove>().characterMoveVariables._isMoveMode)
        //    {
        //        if (Input.GetKeyDown(KeyCode.Tab))// && !this.characterTurnVariables.isDone)
        //        {
        //            //this.characterTurnVariables.isTurnActive = false;
        //            //if (teamId == 1)
        //            //{
        //                GetComponent<CharacterInput>().TurnManager.SwitchCharacter(this.GetComponent<CharacterTurn>(), null);
        //            //}
        //            Debug.Log("CU");
        //            //characterMoveVariables.isTilesFound = false;
        //            this.characterTurnVariables.isDone = true;
        //        }
        //    }
        //}
    }
}
