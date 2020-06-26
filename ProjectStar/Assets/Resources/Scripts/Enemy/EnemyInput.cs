using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : MonoBehaviour
{
    [Header("INSERT A GRIDMANAGER SO :")]
    public GridManager_SO GridManager;
    [Header("INSERT A COMBAT CALCULATOR MANAGER SO :")]
    public CombatCalculatorManager_SO CombatCalculatorManager;    
    [Header("INSERT A TURN MANAGER SO :")]
    public TurnManager_SO TurnManager;


    [Header("ENEMY PATH AI VARIABLES - INSTANCE :")]
    public EnemyPathAI_SO enemyPathAIVariables;
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
                if (EnemyMoveVariables.isMoving)
                {
                    GetComponent<EnemyMove>().Move();
                }

                if (!EnemyMoveVariables.isTilesFound)
                {
                    if (EnemyTurnVariables.actionPoints <= 0)
                    {
                        TurnManager.RemoveFromTurn(null, this.GetComponent<EnemyTurn>());
                        return;
                    }
                    GridManager.inputEnemy = this;
                    GetComponent<EnemyPathAI>().FindNearestTarget();
                    GetComponent<EnemyPathAI>().CalculatePath();
                    GridManager.CalculateAvailablePathForTheAI(this.gameObject); // THE ONLY FUNCTION OF THIS METHOD IS TO USE BFS ALGORITHM TO SHOW AI MOVEMENT OPTIONS. NOT RELEVANT TO ITS MOVEMENT
                }
            }
            if (EnemyMoveVariables._isCombatMode)
            {
                if (!EnemyMoveVariables.isAttackRangeFound)
                {
                    ////>>>>>>>REDO THIS WITH WEAPON BELT!!!!!!!!!!!
                    //characterMoveVariables._weaponRange = GetComponent<CharacterCombat>().weapon.GetComponent<WeaponInput>().weaponBasicVariables.weaponRange; //weaponInstanceBelt[_currentWeaponIndex].GetComponent<WeaponBaseClass>().weaponRange + attackRangeModifier;
                    //if (characterTurnVariables.actionPoints <= 0)
                    //{
                    //    TurnManager.RemoveFromTurn(this.GetComponent<CharacterTurn>(), null);
                    //    return;
                    //}
                    //GridManager.CalculateAttackPath(this.gameObject);
                    //GetComponent<CharacterCombat>().ScanForEnemies();
                }
            }
        }
    }

    public void ShowProbability()
    {
        Debug.Log(CombatCalculatorManager.DisplayShotChance());
    }

    
}
