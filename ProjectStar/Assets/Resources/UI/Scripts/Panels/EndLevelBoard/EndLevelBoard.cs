using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelBoard : MonoBehaviour
{
    [Header("INSERT TURN MANAGER SO :")]
    public TurnManager_SO TurnManager;
    [Header("INSERT UI MANAGER SO :")]
    public UIManager_SO uiManager;
    [Header("INSERT SAVED PLAYER CHARACTERS SO:")]
    public SavedPlayerCharacters_SO savedPlayerCharacters_SO;
    [Header("INSERT SAVED ENEMIES SO:")]
    public SavedEnemies_SO savedEnemies_SO;
    [Header("LIST OF ALL CHARACTERS")]
    public CharacterList_SO listOfAllCharacters;
    [Header("LIST OF ALL ENEMIES")]
    public EnemyList_SO listOfAllEnemies;


    [Header("SIGIL BOARD VERSIONS")]
    public Sprite heroClassSigil;
    public Sprite assaultClassSigil;
    public Sprite sniperClassSigil;
    public Sprite heavyClassSigil;
    public Sprite supportClassSigil;

    [Header("INFO CARD PREFAB")]
    public GameObject playerInfoCardPrefab;
    public GameObject enemyInfoCardPrefab;

    [Header("END LEVEL BOARD ELEMENTS")]
    public Text boardHeader;
    public GameObject playerListContent;
    public GameObject enemyListContent;


    private List<GroupableEntities> listOfAllCharactersCopy = new List<GroupableEntities>();
    private List<GroupableEntities> listOfAllEnemiesCopy = new List<GroupableEntities>();

    // Start is called before the first frame update
    private void Awake()
    {
        uiManager.endLevelBoard = this;        
    }

    private void Start()
    {
        listOfAllCharactersCopy = new List<GroupableEntities>(listOfAllCharacters.GetList());
        listOfAllEnemiesCopy = new List<GroupableEntities>(listOfAllEnemies.GetList());
    }

    public void PreparePlayerInfoCards()
    {
        foreach (var groupableEntity in listOfAllCharactersCopy)//listOfAllCharacters.GetList())
        {
            PlayerInfoCard playerInfoCard = Instantiate(playerInfoCardPrefab).GetComponent<PlayerInfoCard>();
            CharacterInput characterInput = groupableEntity.GetComponent<CharacterInput>();
            playerInfoCard.gameObject.SetActive(true);

            switch (characterInput.characterSetupVariables.characterClass)
            {
                case CharacterClass.support:
                    playerInfoCard.playerSigil.sprite = supportClassSigil;
                    playerInfoCard.playerClassText.text = "Support";
                    break;
                case CharacterClass.sniper:
                    playerInfoCard.playerSigil.sprite = sniperClassSigil;
                    playerInfoCard.playerClassText.text = "Sniper";
                    break;
                case CharacterClass.assault:
                    playerInfoCard.playerSigil.sprite = assaultClassSigil;
                    playerInfoCard.playerClassText.text = "Assault";
                    break;
                case CharacterClass.heavy:
                    playerInfoCard.playerSigil.sprite = heavyClassSigil;
                    playerInfoCard.playerClassText.text = "Heavy";
                    break;
                case CharacterClass.hero:
                    playerInfoCard.playerSigil.sprite = heroClassSigil;
                    playerInfoCard.playerClassText.text = "Hero";
                    break;
                case CharacterClass.undefined:
                    playerInfoCard.playerSigil.sprite = null;
                    playerInfoCard.playerClassText.text = "No Class";
                    break;
                default:
                    break;
            }

            playerInfoCard.playerNameText.text = characterInput.characterSetupVariables.characterName;
            playerInfoCard.playerCallSignText.text = characterInput.characterSetupVariables.callSign;
            playerInfoCard.playerMissionKillsText.text = characterInput.characterStatsVariables.missionKills.ToString();
            playerInfoCard.playerLevelText.text = characterInput.characterSetupVariables.characterLevel.ToString();

            if (characterInput.characterStatsVariables.isAlive)
            {
                playerInfoCard.playerStatsText.text = "Alive";
                playerInfoCard.playerStatsText.color = Color.green;
            }
            else
            {
                playerInfoCard.playerStatsText.text = "K.I.A.";
                playerInfoCard.playerStatsText.color = Color.red;
            }


            playerInfoCard.transform.parent = playerListContent.transform;
        }

        int numberOfNormalGrunts = 0;
        int numberOfDeadNormalGrunts = 0;
        int highestNormalGrunt = 0;
        int totalPlayersKilledbyNormalGrunts = 0;
        
        int numberOfGranadiers = 0;
        int numberOfDeadGranadiers = 0;
        int highestGranadier = 0;
        int totalPlayerKilledbyGranadiers = 0;


        foreach (var groupableEntity in listOfAllEnemiesCopy)//listOfAllEnemies.GetList())
        {
            EnemyInput enemyInput = groupableEntity.GetComponent<EnemyInput>();

            if (enemyInput.EnemyStatsVariables.enemyCLass == "Normal Grunt")
            {              
                numberOfNormalGrunts++;
                totalPlayersKilledbyNormalGrunts += enemyInput.EnemyStatsVariables.playersKilled;

                if (!enemyInput.EnemyStatsVariables.isAlive)
                {
                    numberOfDeadNormalGrunts++;
                }

                if (enemyInput.EnemyStatsVariables.enemyLevel > highestNormalGrunt)
                {
                    highestNormalGrunt = enemyInput.EnemyStatsVariables.enemyLevel;
                }
            }
            else if (enemyInput.EnemyStatsVariables.enemyCLass == "Granadier")
            {
                numberOfGranadiers++;
                totalPlayerKilledbyGranadiers += enemyInput.EnemyStatsVariables.playersKilled;

                if (!enemyInput.EnemyStatsVariables.isAlive)
                {
                    numberOfDeadGranadiers++;
                }

                if (enemyInput.EnemyStatsVariables.enemyLevel > highestGranadier)
                {
                    highestGranadier = enemyInput.EnemyStatsVariables.enemyLevel;
                }
            }
            //etc...            
        }

        if (numberOfNormalGrunts > 0)
        {
            EnemyInfoCard enemyInfoCard = Instantiate(enemyInfoCardPrefab.GetComponent<EnemyInfoCard>(), enemyListContent.transform);
            enemyInfoCard.gameObject.SetActive(true);

            enemyInfoCard.enemyClassText.text = "Normal Grunt";
            enemyInfoCard.enemyUnitsKilledText.text = numberOfDeadNormalGrunts.ToString();
            enemyInfoCard.enemyUnitsKilledText.color = Color.green;
            enemyInfoCard.enemyMostHigherLevelText.text = highestNormalGrunt.ToString();
            enemyInfoCard.enemyMissionKillsText.text = totalPlayersKilledbyNormalGrunts.ToString();
            enemyInfoCard.enemyMissionKillsText.color = Color.red;
            enemyInfoCard.enemyUnitsAliveText.text = (numberOfNormalGrunts - numberOfDeadNormalGrunts).ToString();            
        }

        if (numberOfGranadiers > 0)
        {
            EnemyInfoCard enemyInfoCard = Instantiate(enemyInfoCardPrefab.GetComponent<EnemyInfoCard>(), enemyListContent.transform);
            enemyInfoCard.gameObject.SetActive(true);

            enemyInfoCard.enemyClassText.text = "Granadier";
            enemyInfoCard.enemyUnitsKilledText.text = numberOfDeadGranadiers.ToString();
            enemyInfoCard.enemyUnitsKilledText.color = Color.green;
            enemyInfoCard.enemyMostHigherLevelText.text = highestGranadier.ToString();
            enemyInfoCard.enemyMissionKillsText.text = totalPlayerKilledbyGranadiers.ToString();
            enemyInfoCard.enemyMissionKillsText.color = Color.red;
            enemyInfoCard.enemyUnitsAliveText.text = (numberOfGranadiers - numberOfDeadGranadiers).ToString();
        }
        //etc...


        SetHeader();
        EndLevelBoardToggleTween();
    }

    public void SetHeader()
    {
        if (TurnManager.isGameWon)
        {
            boardHeader.text = "Mission Completed!";
            boardHeader.color = Color.green;
        }
        else if (TurnManager.isGameOver)
        {
            boardHeader.text = "Mission Failed!";
            boardHeader.color = Color.red;
        }
    }

    public void EndLevelBoardToggleTween()
    {
        GetComponent<EndLevelTween>().ToggleTween();
    }


}
