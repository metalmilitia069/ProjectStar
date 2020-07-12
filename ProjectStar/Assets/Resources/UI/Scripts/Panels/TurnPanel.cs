using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPanel : MonoBehaviour
{
    [Header("INSERT TURN MANAGER SO :")]
    public TurnManager_SO TurnManager;
    [Header("INSERT UI MANAGER SO :")]
    public UIManager_SO uiManager;

    public Sprite turnPanelSprite;
    public Image sigilImage;
    public Text turnsNameText;

    private void Awake()
    {
        uiManager.turnPanel = this;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallTurnPanel(GroupableEntities groupableEntity)
    {
        if (groupableEntity.GetComponent<CharacterInput>())
        {
            Debug.Log("Peidei");
            CharacterInput characterInput = groupableEntity.GetComponent<CharacterInput>();
            SetupTurnPanel(characterInput, null);

            StartCoroutine(TurnPanelCorroutine(characterInput, null));
        }
        else if (groupableEntity.GetComponent<EnemyInput>())
        {
            EnemyInput enemyInput = groupableEntity.GetComponent<EnemyInput>();
            SetupTurnPanel(null, enemyInput);

            StartCoroutine(TurnPanelCorroutine(null, enemyInput));
        }
    }

    public void SetupTurnPanel(CharacterInput characterInput, EnemyInput enemyInput)
    {
        if (characterInput != null)
        {
            sigilImage.sprite = characterInput.characterSetupVariables.characterSigilReference;
            turnsNameText.text = "Player's Turn";
        }
        else if (enemyInput != null)
        {
            sigilImage.sprite = enemyInput.EnemyStatsVariables.enemySigilReference;
            turnsNameText.text = "Enemy's Turn";
        }
    }

    IEnumerator TurnPanelCorroutine(CharacterInput characterInput, EnemyInput enemyInput)
    {
        GetComponent<TurnPanelTween>().ToggleTween();

        yield return new WaitForSeconds(4);

        GetComponent<TurnPanelTween>().ToggleTween();
        //TurnManager.canContinue = true;
        if (enemyInput != null)
        {
            TurnManager.ContinueRound();
        }
    }




}
