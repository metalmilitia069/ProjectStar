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

    // Start is called before the first frame update
    private void Awake()
    {
        uiManager.endLevelBoard = this;        
    }
    
    public void PreparePlayerInfoCards()
    {
        foreach (var characterInput in savedPlayerCharacters_SO.GetMissionList())
        {
            PlayerInfoCard playerInfoCard = Instantiate(playerInfoCardPrefab).GetComponent<PlayerInfoCard>();

            
        }
    }
}
