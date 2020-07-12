using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurnManager", menuName = "ScriptableManagers/Type: TurnManager")]
public class TurnManager_SO : ScriptableObject
{
    [Header("LIST OF ALL CHARACTERS")]
    public CharacterList_SO listOfAllCharacters;
    [Header("LIST OF ALL ENEMIES")]
    public EnemyList_SO listOfAllEnemies;
    
    public List<GroupableEntities> playerTeamList;
    public List<GroupableEntities> inactivePlayerTeamList;
    
    public List<GroupableEntities> enemyTeamList;
    public List<GroupableEntities> inactiveEnemyTeamList;
    
    public Queue<List<GroupableEntities>> roundQueueGameObject;
        
    public int roundCounter = 0;
    public bool isGameOver = false;
    public bool isGameWon = false;

    private void OnEnable()
    {
        //listOfAllCharacters.GetList().Sort((ch1, ch2) => ch1.characterMoveVariables.teamId.CompareTo(ch2.characterMoveVariables.teamId));
        
    }

    public void ResetLocalLists()
    {
        playerTeamList = new List<GroupableEntities>();
        inactivePlayerTeamList = new List<GroupableEntities>();
        enemyTeamList = new List<GroupableEntities>();
        inactiveEnemyTeamList = new List<GroupableEntities>();
        
        
        roundQueueGameObject = new Queue<List<GroupableEntities>>();

        roundCounter = 0;        
    }    

    public void SortActionOrder()
    {
        ResetLocalLists();
                
        playerTeamList = new List<GroupableEntities>(listOfAllCharacters.GetList());
        playerTeamList.Sort((ch1, ch2) => ch1.GetComponent<CharacterTurn>().teamId.CompareTo(ch2.GetComponent<CharacterTurn>().teamId));
                
        enemyTeamList = new List<GroupableEntities>(listOfAllEnemies.GetList());

        RoundSetup();
    }

    public void RoundSetup()
    {
        if (inactivePlayerTeamList.Count > 0 && inactiveEnemyTeamList.Count > 0)
        {
            playerTeamList = new List<GroupableEntities>(inactivePlayerTeamList);
            inactivePlayerTeamList.Clear();

            foreach (var chara in playerTeamList)
            {
                chara.GetComponent<CharacterTurn>().ResetActionPoints();
            }
            
            playerTeamList.Sort((ch1, ch2) => ch1.GetComponent<CharacterTurn>().characterTurnVariables.teamId.CompareTo(ch2.GetComponent<CharacterTurn>().characterTurnVariables.teamId));
            //playerTeamList[0].GetComponent<CharacterInput>().uiManager.weaponDisplayPanel.SetWeaponToDisplay();
            playerTeamList[0].GetComponent<CharacterInput>().uiManager.weaponDisplayPanel.ToggleTween();

            enemyTeamList = new List<GroupableEntities>(inactiveEnemyTeamList);
            inactiveEnemyTeamList.Clear();

            foreach (var enem in enemyTeamList)
            {
                enem.GetComponent<EnemyTurn>().ResetActionPoints();
            }
        }

        roundQueueGameObject.Enqueue(playerTeamList);
        roundQueueGameObject.Enqueue(enemyTeamList);

        StartRound();
    }

    public void StartRound()
    {
        playerTeamList[0].GetComponent<CharacterInput>().uiManager.turnPanel.CallTurnPanel(playerTeamList[0]);

        playerTeamList[0].GetComponent<CharacterInput>().MainCameraControllerVariables.LockCamera(playerTeamList[0].transform);


        CharacterTurn characterTurn = roundQueueGameObject.Dequeue()[0].GetComponent<CharacterTurn>();
        //roundQueueGameObject.Dequeue()[0].GetComponent<CharacterTurn>().characterTurnVariables.isTurnActive = true;
        characterTurn.characterTurnVariables.isTurnActive = true;

        characterTurn.GetComponent<CharacterInput>().uiManager.weaponDisplayPanel.SetWeaponToDisplay();
        //characterTurn.GetComponent<CharacterInput>().uiManager.weaponDisplayPanel.ToggleTween();

        roundCounter++;
    }

    public bool canContinue = false;

    public void PrepareToContinueRound()
    {
        //canContinue = false;
        enemyTeamList[0].GetComponent<EnemyInput>().uiManager.turnPanel.CallTurnPanel(enemyTeamList[0]);


    }

    public void ContinueRound()
    {

        //if (canContinue)
        //{
            EnemyTurn enemyTurn = roundQueueGameObject.Dequeue()[0].GetComponent<EnemyTurn>();
            if (enemyTurn)
            {
                enemyTurn.EnemyTurnVariables.isTurnActive = true;
            }
            //canContinue = false;
        //}

        //ATTENTION!!! >>> IF MORE CLASSES, ADD MORE IFS-ELSES
    }       

    public void SwitchCharacter(CharacterTurn character, EnemyTurn enemy)
    {

        if (character != null)
        {
            if (character.GetComponent<CharacterMove>().characterMoveVariables.isMoving)
            {
                return;
            }

            int index = playerTeamList.IndexOf(character);

            playerTeamList[index].GetComponent<CharacterTurn>().characterTurnVariables.isTurnActive = false;
            playerTeamList[index].GetComponent<CharacterInput>().characterMoveVariables.isTilesFound = false;
            playerTeamList[index].GetComponent<CharacterInput>().characterMoveVariables.isAttackRangeFound = false;
            playerTeamList[index].GetComponent<CharacterInput>().characterMoveVariables._isMoveMode = true;
            playerTeamList[index].GetComponent<CharacterInput>().characterMoveVariables._isCombatMode = false;


            if (index >= playerTeamList.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            
            playerTeamList[index].GetComponent<CharacterTurn>().characterTurnVariables.isTurnActive = true;
            playerTeamList[index].GetComponent<CharacterInput>().uiManager.weaponDisplayPanel.SetWeaponToDisplay();
            //character.GetComponent<CharacterInput>().uiManager.weaponDisplayPanel.SetWeaponToDisplay();

        }
        else if (enemy != null)
        {
            int index = enemyTeamList.IndexOf(enemy);

            enemyTeamList[index].GetComponent<EnemyTurn>().EnemyTurnVariables.isTurnActive = false;
            enemyTeamList[index].GetComponent<EnemyInput>().EnemyMoveVariables.isTilesFound = false;
            enemyTeamList[index].GetComponent<EnemyInput>().EnemyMoveVariables.isAttackRangeFound = false;
            enemyTeamList[index].GetComponent<EnemyInput>().EnemyMoveVariables._isMoveMode = true;
            enemyTeamList[index].GetComponent<EnemyInput>().EnemyMoveVariables._isCombatMode = false;

            if (index >= enemyTeamList.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }

            enemyTeamList[index].GetComponent<EnemyTurn>().EnemyTurnVariables.isTurnActive = true;
        }
    }

    public void RemoveFromTurn(CharacterTurn character, EnemyTurn enemy)
    {
        if (character != null)
        {
            int index = playerTeamList.IndexOf(character);

            inactivePlayerTeamList.Add(playerTeamList[index]);

            if (playerTeamList.Count > 1)
            {
                SwitchCharacter(character, null);
                playerTeamList.Remove(character);
            }
            else
            {
                //endplayer turn
                character.characterTurnVariables.isTurnActive = false;

                character.GetComponent<CharacterInput>().uiManager.weaponDisplayPanel.ToggleTween();

                playerTeamList.Remove(character);

                PrepareToContinueRound();
                //ContinueRound();
            }
        }
        else if (enemy != null)
        {
            int index = enemyTeamList.IndexOf(enemy);

            inactiveEnemyTeamList.Add(enemyTeamList[index]);

            if (enemyTeamList.Count > 1)
            {
                SwitchCharacter(null, enemy);
                enemyTeamList.Remove(enemy);
            }
            else
            {
                //endenemy turn
                enemy.EnemyTurnVariables.isTurnActive = false;
                enemyTeamList.Remove(enemy);

                RoundSetup();
            }
        }
    }

    public void RemoveFromTeam(CharacterTurn character, EnemyTurn enemy)
    {
        if (character != null)
        {
            playerTeamList.Remove(character);
            inactivePlayerTeamList.Remove(character);
        }
        else if (enemy != null)
        {
            enemyTeamList.Remove(enemy);
            inactivePlayerTeamList.Remove(enemy);
        }

        CheckEndStageCondition();
    }

    public void CheckEndStageCondition()
    {
        if (playerTeamList.Count < 1 && inactivePlayerTeamList.Count < 1)
        {
            isGameOver = true;
            Debug.Log("GAME OVER, PAL!");
            Debug.Log("TODO: Implement END GAME Sequence and Stuff!!!!");
            Time.timeScale = 0;
        }
        else if (enemyTeamList.Count < 1 && inactiveEnemyTeamList.Count < 1)
        {
            isGameWon = true;
            Debug.Log("GAME WON!!!!");
            Debug.Log("TODO: Implement END GAME Sequence and Stuff!!!!");
            Time.timeScale = 0;
        }
    }

    public void SelectCharacterOnClick(CharacterTurn characterTurn)
    {
        bool conditionOne = false;
        bool conditionTwo = false;
        CharacterTurn characterTempToBeActivated = default;
        CharacterTurn charTempToBeDeactivated = default;

        foreach (var character in playerTeamList)
        {
            if (character == characterTurn.GetComponent<GroupableEntities>())
            {
                characterTempToBeActivated = character.GetComponent<CharacterTurn>();
                conditionOne = true;
            }

            if (character.GetComponent<CharacterTurn>().characterTurnVariables.isTurnActive)
            {
                charTempToBeDeactivated = character.GetComponent<CharacterTurn>();
                conditionTwo = true;
            }

            if (conditionOne && conditionTwo)
            {
                charTempToBeDeactivated.characterTurnVariables.isTurnActive = false;
                charTempToBeDeactivated.GetComponent<CharacterInput>().characterMoveVariables.isTilesFound = false;
                charTempToBeDeactivated.GetComponent<CharacterInput>().characterMoveVariables.isAttackRangeFound = false;
                charTempToBeDeactivated.GetComponent<CharacterInput>().characterMoveVariables._isMoveMode = true;


                characterTempToBeActivated.characterTurnVariables.isTurnActive = true;
            }
        }

        characterTurn.GetComponent<CharacterMove>().enabled = true;
    }    
}
