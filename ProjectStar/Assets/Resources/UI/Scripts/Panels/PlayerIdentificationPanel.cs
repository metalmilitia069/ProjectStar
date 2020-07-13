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

    //LEAVE JUST ONE CLASS SIGIL SPRITE LATER, WHEN CREATING POOLS AND SPAWNMANAGER!!!!!!!!!!
    public Sprite assaultSigil;
    public Sprite sniperSigil;
    public Sprite heavySigil;
    public Sprite supportSigil;
    public Sprite heroSigil;

    private void Awake()
    {
        uiManager.playerIdentificationPanel = this;        
    }

    // Start is called before the first frame update
    void Start()
    {
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
                characterInput.characterSetupVariables.characterClassSigilReference = supportSigil;
                break;
            case CharacterClass.sniper:
                characterClassText.text = "Sniper";
                characterInput.characterSetupVariables.characterClassSigilReference = sniperSigil;
                break;
            case CharacterClass.assault:
                characterClassText.text = "Assault";
                characterInput.characterSetupVariables.characterClassSigilReference = assaultSigil;
                break;
            case CharacterClass.heavy:
                characterClassText.text = "Heavy";
                characterInput.characterSetupVariables.characterClassSigilReference = heavySigil;
                break;
            case CharacterClass.hero:
                characterClassText.text = "Hero";
                characterInput.characterSetupVariables.characterClassSigilReference = heroSigil;
                break;
            case CharacterClass.undefined:
                characterClassText.text = "NOT DEFINED";
                break;
            default:
                break;
        }

        characterClassSigil.sprite = characterInput.characterSetupVariables.characterClassSigilReference;
        characterClassSigil.SetNativeSize();
    }

    public void ToggleTween()
    {
        GetComponent<PlayerIdentificationPanelTween>().ToggleTween();
    }
}
