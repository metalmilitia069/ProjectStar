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
        ResetLocalLists();        
    }

    public void ResetLocalLists()
    {
        playerTeamList = new List<GroupableEntities>();
        inactivePlayerTeamList = new List<GroupableEntities>();
        enemyTeamList = new List<GroupableEntities>();
        inactiveEnemyTeamList = new List<GroupableEntities>();
        
        
        roundQueueGameObject = new Queue<List<GroupableEntities>>();

        roundCounter = 0;
        isGameWon = false;
        isGameOver = false;
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
        if (isGameOver || isGameWon)
        {
            return;
        }
        if (inactivePlayerTeamList.Count > 0 && inactiveEnemyTeamList.Count > 0)
        {
            playerTeamList = new List<GroupableEntities>(inactivePlayerTeamList);
            inactivePlayerTeamList.Clear();

            foreach (var chara in playerTeamList)
            {                
                chara.GetComponent<CharacterTurn>().ResetActionPoints();
                chara.GetComponent<CharacterInput>().characterMoveVariables.isTilesFound = false;
            }
            
            playerTeamList.Sort((ch1, ch2) => ch1.GetComponent<CharacterTurn>().characterTurnVariables.teamId.CompareTo(ch2.GetComponent<CharacterTurn>().characterTurnVariables.teamId));
            
            playerTeamList[0].GetComponent<CharacterInput>().uiManager.weaponDisplayPanel.ToggleTween();
            playerTeamList[0].GetComponent<CharacterInput>().uiManager.playerIdentificationPanel.ToggleTween();


            enemyTeamList = new List<GroupableEntities>(inactiveEnemyTeamList);
            inactiveEnemyTeamList.Clear();

            foreach (var enem in enemyTeamList)
            {
                enem.GetComponent<EnemyTurn>().ResetActionPoints();
                enem.GetComponent<EnemyInput>().EnemyStatsVariables.ResetOverWatchable();
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
        
        characterTurn.GetComponent<CharacterInput>().characterMoveVariables._isMoveMode = true;
        characterTurn.characterTurnVariables.isTurnActive = true;
        
        

        characterTurn.GetComponent<CharacterInput>().uiManager.weaponDisplayPanel.SetWeaponToDisplay();
        characterTurn.GetComponent<CharacterInput>().uiManager.playerIdentificationPanel.SetupPlayerIdentificationPanel(characterTurn.GetComponent<CharacterInput>());
        

        roundCounter++;
    }

    public bool canContinue = false;

    public void PrepareToContinueRound()
    {        
        enemyTeamList[0].GetComponent<EnemyInput>().uiManager.turnPanel.CallTurnPanel(enemyTeamList[0]);
    }

    public void ContinueRound()
    {
            EnemyTurn enemyTurn = roundQueueGameObject.Dequeue()[0].GetComponent<EnemyTurn>();
            if (enemyTurn)
            {
                enemyTurn.EnemyTurnVariables.isTurnActive = true;
            }            

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

            character.GetComponent<CharacterInput>().UnMarkEnemy();
            character.GetComponent<CharacterInput>().uiManager.attackPanel.GetComponent<AttackPanelTween>().OutPosTween();
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
            playerTeamList[index].GetComponent<CharacterInput>().uiManager.playerIdentificationPanel.SetupPlayerIdentificationPanel(playerTeamList[index].GetComponent<CharacterInput>());
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
                character.GetComponent<CharacterInput>().UnMarkEnemy();

                character.GetComponent<CharacterInput>().uiManager.weaponDisplayPanel.ToggleTween();
                character.GetComponent<CharacterInput>().uiManager.playerIdentificationPanel.ToggleTween();
                character.GetComponent<CharacterInput>().uiManager.attackPanel.GetComponent<AttackPanelTween>().OutPosTween();
                character.GetComponent<CharacterInput>().uiManager.DisableButtons();

                
                character.GetComponent<CharacterInput>().characterMoveVariables.isTilesFound = false;
                
                character.GetComponent<CharacterInput>().characterMoveVariables._isMoveMode = true;

                character.GetComponent<CharacterTurn>().characterTurnVariables.isTurnActive = false;
                character.GetComponent<CharacterInput>().characterMoveVariables.isTilesFound = false;
                character.GetComponent<CharacterInput>().characterMoveVariables.isAttackRangeFound = false;
                character.GetComponent<CharacterInput>().characterMoveVariables._isMoveMode = true;
                character.GetComponent<CharacterInput>().characterMoveVariables._isCombatMode = false;


                playerTeamList.Remove(character);

                CheckEndStageCondition();
                PrepareToContinueRound();                
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
            if (enemy.EnemyTurnVariables.isTurnActive)
            {
                int index = enemyTeamList.IndexOf(enemy);
                if (enemyTeamList.Count > 1)
                {
                    index++;
                    if (index >= enemyTeamList.Count - 1)
                    {
                        index = 0;
                    }

                    enemyTeamList[index].GetComponent<EnemyInput>().EnemyTurnVariables.isTurnActive = true;                    
                }
            }
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

            listOfAllCharacters.GetList()[0].GetComponent<CharacterInput>().uiManager.endLevelBoard.PreparePlayerInfoCards();
            listOfAllCharacters.GetList()[0].GetComponent<CharacterInput>().uiManager.DisableButtons();
            listOfAllCharacters.GetList()[0].GetComponent<CharacterInput>().uiManager.DisablePanels();
            listOfAllEnemies.GetList()[0].GetComponent<EnemyInput>().uiManager.turnPanel.TweenPanelOut();
        }
        else if (enemyTeamList.Count < 1 && inactiveEnemyTeamList.Count < 1)
        {
            isGameWon = true;
            Debug.Log("GAME WON!!!!");
            listOfAllEnemies.GetList()[0].GetComponent<EnemyInput>().uiManager.endLevelBoard.PreparePlayerInfoCards();
            listOfAllEnemies.GetList()[0].GetComponent<EnemyInput>().uiManager.DisableButtons();
            listOfAllEnemies.GetList()[0].GetComponent<EnemyInput>().uiManager.DisablePanels();
            listOfAllEnemies.GetList()[0].GetComponent<EnemyInput>().uiManager.turnPanel.TweenPanelOut();
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

    public void EndPlayerTurn()
    {
        foreach (var activeCharacter in playerTeamList)
        {
            activeCharacter.GetComponent<CharacterInput>().characterTurnVariables.actionPoints = 0;
            activeCharacter.GetComponent<CharacterInput>().characterTurnVariables.isTurnActive = false;
            inactivePlayerTeamList.Add(activeCharacter);
            activeCharacter.GetComponent<CharacterInput>().characterMoveVariables.isTilesFound = false;
            
            activeCharacter.GetComponent<CharacterInput>().characterMoveVariables._isMoveMode = true;
            activeCharacter.GetComponent<CharacterInput>().UnMarkEnemy();

        }
        playerTeamList.Clear();

        inactivePlayerTeamList[0].GetComponent<CharacterInput>().uiManager.weaponDisplayPanel.ToggleTween();

        PrepareToContinueRound();        
    }

    public void EndUnitsTurn(CharacterInput characterInput)
    {
        
        RemoveFromTurn(characterInput.GetComponent<CharacterTurn>(), null);
        characterInput.characterTurnVariables.actionPoints = 0;
    }
}
