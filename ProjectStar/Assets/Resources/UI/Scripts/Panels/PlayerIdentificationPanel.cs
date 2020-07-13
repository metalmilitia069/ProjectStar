using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIdentificationPanel : MonoBehaviour
{
    [Header("INSERT TURN MANAGER SO :")]
    public TurnManager_SO TurnManager;
    [Header("INSERT UI MANAGER SO :")]
    public UIManager_SO uiManager;

    public Text characterNameText;
    public Text characterClassText;
    public Image characterClassSigil;


    // Start is called before the first frame update
    void Start()
    {
        uiManager.playerIdentificationPanel = this;
    }
     
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupPlayerIdentificationPanel(CharacterInput characterInput)
    {
        characterNameText.text = characterInput.characterSetupVariables.characterName;

        switch (characterInput.characterSetupVariables.characterClass)
        {
            case CharacterClass.support:
                characterClassText.text = "Support";
                break;
            case CharacterClass.sniper:
                characterClassText.text = "Sniper";
                break;
            case CharacterClass.assault:
                characterClassText.text = "Assault";
                break;
            case CharacterClass.heavy:
                characterClassText.text = "Heavy";
                break;
            case CharacterClass.hero:
                characterClassText.text = "Hero";
                break;
            case CharacterClass.undefined:
                characterClassText.text = "NOT DEFINED";
                break;
            default:
                break;
        }

        characterClassSigil.sprite = characterInput.characterSetupVariables.characterClassSigilReference;
    }

    public void ToggleTween()
    {
        GetComponent<PlayerIdentificationPanelTween>().ToggleTween();
    }
}
