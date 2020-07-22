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
    [Header("INSERT A UI MANAGER SO :")]
    public UIManager_SO uiManager;

    [Header("ENEMY SAVED DATA SO - COPY FROM ENEMY SAVED DATA")]
    public SavedEnemies_SO enemySavedData;
    [Header("ENEMY DETECTION AI VARIABLES - INSTANCE :")]
    public EnemyDetectionAI_SO enemyDetectionAIVariables;
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

    public IEnumerator WaitForDetection(Coroutine co)
    {        
        yield return co;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyTurnVariables.isTurnActive)
        {
            if (EnemyTurnVariables.actionPoints <= 0)
            {
                TurnManager.RemoveFromTurn(null, this.GetComponent<EnemyTurn>());
                return;
            }

            if (!enemyDetectionAIVariables.isAlertMode)
            {
                GetComponent<EnemyDetectionAI>().AIDetectHostiles(true);
                StartCoroutine(WaitForDetection(StartCoroutine(GetComponent<EnemyDetectionAI>().WaitForDetection())));
                return;
            }

            //AI DECISION 

            if (EnemyCombatVariables.checkOverwatch)
            {
                EnemyCombatVariables.checkOverwatch = false;

                if(GetComponent<EnemyDetectionAI>().AIChooseOverWatchState())
                {
                    return;
                }                
            }

            Debug.Log("not overwathing");

            //END OF AI DECISION

            if (EnemyMoveVariables._isMoveMode)
            {
                if (EnemyMoveVariables.isMoving)
                {
                    Debug.Log(this.name + " Is moving");
                    GetComponent<EnemyMove>().Move();
                    enemyPathAIVariables.isScanRoutineDone = false;
                    return;
                }

                if (!enemyPathAIVariables.isScanRoutineDone)
                {                    
                    ChangeMode();
                    return;
                }

                if (!EnemyMoveVariables.isTilesFound)
                {                    
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
                    EnemyMoveVariables._weaponRange = GetComponent<EnemyCombat>().weapon.GetComponent<WeaponInput>().weaponBasicVariables.weaponRange; //CHANGE ONCE THE WEAPON BELT SYSTEM IS DONE!!!!
                    GridManager.CalculateAttackPathForTheAI(this.gameObject);
                    GetComponent<EnemyCombat>().ScanRoutine();
                    enemyPathAIVariables.isScanRoutineDone = true;
                    ChangeMode();
                    return;                    
                }
            }
        }
        else
        {
            if (EnemyCombatVariables.isOverWatching)
            {
                GetComponent<EnemyCombat>().OverWatch();
            }
        }
    }

    public void ShowProbability()
    {
        Debug.Log(CombatCalculatorManager.DisplayShotChance());
    }

    //private void OnMouseOver()
    //{
    //    if (!EnemyCombatVariables.isMarkedEnemy)
    //    {
    //        return;
    //    }



    //}

    public void ChangeMode()
    {
        if (EnemyMoveVariables._isMoveMode)
        {
            EnemyMoveVariables._isCombatMode = true;
            EnemyMoveVariables._isMoveMode = false;
            EnemyMoveVariables.isAttackRangeFound = false;
            foreach (var item in GridManager.tileList_SO.GetList())
            {
                item.basicTileVariables.isMoveMode = false;
            }

        }
        else if (EnemyMoveVariables._isCombatMode)
        {
            EnemyMoveVariables._isCombatMode = false;
            EnemyMoveVariables._isMoveMode = true;
            EnemyMoveVariables.isTilesFound = false;
            foreach (var item in GridManager.tileList_SO.GetList())
            {
                item.basicTileVariables.isMoveMode = true;
            }
        }

        GridManager.ClearSelectableTiles();
    }


}
