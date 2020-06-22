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

        
    //public List<IPlayerTeam> playerTeam;// = new List<IPlayerTeam>();
    public List<GroupableEntities> playerTeamList;
    public List<GroupableEntities> inactivePlayerTeamList;

    //public List<GroupableEntities> playerTurnList;

    public List<GroupableEntities> enemyTeamList;
    public List<GroupableEntities> inactiveEnemyTeamList;


    public Queue<List<GroupableEntities>> roundQueueGameObject;

    //AddEntityToTeam()

    //public bool isTurnStarted = true;
    //public bool isPlayerTurn = true; //TODO: change to false after testing
    //public bool isEnemyTurn = false;
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

        playerTeamList = listOfAllCharacters.GetList();
        playerTeamList.Sort((ch1, ch2) => ch1.GetComponent<CharacterTurn>().teamId.CompareTo(ch2.GetComponent<CharacterTurn>().teamId));
        
        enemyTeamList = listOfAllEnemies.GetList();        

        RoundSetup();
    }

    public void RoundSetup()
    {
        if (inactivePlayerTeamList.Count > 0 && inactiveEnemyTeamList.Count > 0)
        {
            playerTeamList = inactivePlayerTeamList;
            foreach (var chara in playerTeamList)
            {
                chara.GetComponent<CharacterTurn>().ResetActionPoints();
            }
            
            playerTeamList.Sort((ch1, ch2) => ch1.GetComponent<CharacterTurn>().characterTurnVariables.teamId.CompareTo(ch2.GetComponent<CharacterTurn>().characterTurnVariables.teamId));

            enemyTeamList = inactiveEnemyTeamList;
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
        roundQueueGameObject.Dequeue()[0].GetComponent<CharacterTurn>().characterTurnVariables.isTurnActive = true;
        
        roundCounter++;
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

    public void SelectCharacterOnClick(CharacterTurn characterTurn)
    {
        bool conditionOne = false;
        bool conditionTwo = false;
        CharacterTurn characterTempToBeActivated = default;
        CharacterTurn charTempToBeDeactivated = default;
        Debug.Log("MOZO");

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
                charTempToBeDeactivated.GetComponent<CharacterInput>().characterMoveVariables._isMoveMode = true;


                characterTempToBeActivated.characterTurnVariables.isTurnActive = true;
            }
        }




        characterTurn.GetComponent<CharacterMove>().enabled = true;
    }

    public void SwitchCharacter(CharacterTurn character, EnemyTurn enemy)
    {
        if (character != null)
        {
            int index = playerTeamList.IndexOf(character);

            playerTeamList[index].GetComponent<CharacterTurn>().characterTurnVariables.isTurnActive = false;
            playerTeamList[index].GetComponent<CharacterInput>().characterMoveVariables.isTilesFound = false;
            playerTeamList[index].GetComponent<CharacterInput>().characterMoveVariables._isMoveMode = true;
            
            if (index >= playerTeamList.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            
            playerTeamList[index].GetComponent<CharacterTurn>().characterTurnVariables.isTurnActive = true;            

        }
        else if (enemy != null)
        {
            int index = enemyTeamList.IndexOf(enemy);

            enemyTeamList[index].GetComponent<EnemyTurn>().EnemyTurnVariables.isTurnActive = false;

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
                playerTeamList.Remove(character);

                ContinueRound();
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
            
        }
        else if (enemyTeamList.Count < 1 && inactiveEnemyTeamList.Count < 1)
        {
            isGameWon = true;
            Debug.Log("GAME WON!!!!");
        }
    }




    //public bool isListStarted = false;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    if (playerTeamList.Count > 0)
    //    {
    //        CharacterStats chara = (CharacterStats)playerTeamList[0];
    //        chara.isTurnActive = true;

    //        CameraTargetManager.instance.isLocked = true;
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (isTurnStarted)
    //    {
    //        playerTurnList = new List<CharacterStats>(playerTeamList);

    //        isTurnStarted = false;
    //    }

    //    if (isPlayerTurn)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Tab))
    //        {
    //            SwitchCharacter();
    //            CameraTargetManager.instance.isLocked = true;
    //        }

    //        if (Input.GetKeyDown(KeyCode.Z))
    //        {
    //            isPlayerTurn = false;
    //            Debug.Log("Players Turn is Over!!!");
    //            foreach (var character in playerTurnList)
    //            {
    //                character.isTurnActive = false;
    //                GridManager.instance.ClearSelectableTiles();
    //            }
    //            playerTurnList.Clear();
    //            isEnemyTurn = true;
    //        }
    //    }
    //}

    //public void PlayerCharacterActionDepleted(CharacterStats characterStats)
    //{
    //    if (playerTurnList.Count == 1)
    //    {
    //        isPlayerTurn = false;
    //        Debug.Log("Players Turn is Over!!!");
    //        characterStats.isTurnActive = false;
    //        playerTurnList.Remove(characterStats);
    //        isEnemyTurn = true;


    //        //CameraTargetManager.instance.isLocked = false;
    //        return;
    //    }

    //    SwitchCharacter();
    //    playerTurnList.Remove(characterStats);
    //}

    //public void SwitchCharacter()
    //{
    //    if (playerTurnList.Count > 0)
    //    {

    //        foreach (var player in playerTurnList)
    //        {
    //            //if (player is CharacterStats)
    //            //{


    //            CharacterStats p = (CharacterStats)player;
    //            if (p.isTurnActive)
    //            {
    //                int index = playerTurnList.IndexOf(p);
    //                p.isTurnActive = false;
    //                p._isMoveMode = true;
    //                p.isTilesFound = false;
    //                index++;
    //                if (index >= playerTurnList.Count)
    //                {
    //                    index = 0;
    //                }
    //                p = (CharacterStats)playerTurnList[index];
    //                CameraTargetManager.instance.followTransform = p.transform;
    //                p.isTurnActive = true;
    //                break;
    //            }
    //            //}
    //        }
    //    }
    //}

    //public void EndTurn()
    //{

    //}
}
