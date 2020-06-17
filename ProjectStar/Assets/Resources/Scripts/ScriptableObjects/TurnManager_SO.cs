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
    public List<CharacterInput> playerTeamList;
    public List<CharacterInput> playerTurnList;

    public List<EnemyInput> enemyTeamList;

    


    public bool isTurnStarted = true;
    public bool isPlayerTurn = true; //TODO: change to false after testing
    public bool isEnemyTurn = false;

    private void OnEnable()
    {
        //listOfAllCharacters.GetList().Sort((ch1, ch2) => ch1.characterMoveVariables.teamId.CompareTo(ch2.characterMoveVariables.teamId));
        
    }

    public void SortActionOrder()
    {
        listOfAllCharacters.GetList().Sort((ch1, ch2) => ch1.characterTurnVariables.teamId.CompareTo(ch2.characterTurnVariables.teamId));
        playerTeamList = listOfAllCharacters.GetList();
        playerTeamList[0].characterTurnVariables.isTurnActive = true;
    }

    public void SwitchCharacter(CharacterInput character, EnemyInput enemy)
    {
        if (character != null)
        {
            int index = playerTeamList.IndexOf(character);
            
            playerTeamList[index].characterTurnVariables.isTurnActive = false;

            if (index >= playerTeamList.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }

            playerTeamList[index].characterTurnVariables.isTurnActive = true;
        }
        else
        {
            int index = enemyTeamList.IndexOf(enemy);

            enemyTeamList[index].EnemyTurnVariables.isTurnActive = false;

            if (index >= enemyTeamList.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }

            enemyTeamList[index].EnemyTurnVariables.isTurnActive = true;
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
