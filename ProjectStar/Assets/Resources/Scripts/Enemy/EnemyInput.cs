using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : MonoBehaviour
{
    [Header("INSERT A COMBAT CALCULATOR MANAGER SO :")]
    public CombatCalculatorManager_SO CombatCalculatorManager;


    [Header("ENEMY MOVE VARIABLES - INSTANCE :")]
    public EnemyMove_SO EnemyMoveVariables;
    [Header("ENEMY COMBAT VARIABLES - INSTANCE :")]
    public EnemyCombat_SO EnemyCombatVariables;
    [Header("ENEMY STATS VARIABLES - INSTANCE :")]
    public EnemyStats_SO EnemyStatsVariables;
    [Header("ENEMY TURN VARIABLES - INSTANCE :")]
    public EnemyTurn_SO EnemyTurnVariables;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyTurnVariables.isTurnActive)
        {
            if (EnemyMoveVariables._isMoveMode)
            {
                if (!EnemyMoveVariables.isMoving)
                {

                }
                else
                {
                    GetComponent<EnemyMove>().Move();
                }

                if (!EnemyMoveVariables.isTilesFound)
                {

                }
            }
        }
    }

    public void ShowProbability()
    {
        Debug.Log(CombatCalculatorManager.DisplayShotChance());
    }

    
}
