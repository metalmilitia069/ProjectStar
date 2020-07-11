using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplayPanel : MonoBehaviour
{
    [Header("INSERT TURN MANAGER SO :")]
    public TurnManager_SO TurnManager;
    [Header("INSERT UI MANAGER SO :")]
    public UIManager_SO uiManager;

    [Header("PANEL RELATED ELEMENTS")]
    public Text weaponNameText;
    public Image weaponDisplayImage;
    public Image bulletsDisplayImage;


    bool isWeaponDisplayInitialized = false;

    private void Awake()
    {
        uiManager.weaponDisplayPanel = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWeaponDisplayInitialized)
        {
            return;
        }
        if (GetActiveCharacter().GetComponent<CharacterCombat>().GetCurrentWeapon() != null)
        {
            SetWeaponToDisplay();
            isWeaponDisplayInitialized = true;
        }
    }

    public void SetWeaponToDisplay()
    {
        if (GetActiveCharacter().GetComponent<CharacterCombat>().GetCurrentWeapon() != null)
        {
            weaponDisplayImage.sprite = GetActiveCharacter().GetComponent<CharacterCombat>().GetCurrentWeapon().weaponBasicVariables.weaponDisplaySprite;
        }
    }

    public void ChangeWeapon()
    {
        GetActiveCharacter().ChangeWeapon();
    }

    public CharacterInput GetActiveCharacter()
    {
        foreach (var groupableEntity in TurnManager.listOfAllCharacters.GetList())
        {
            if (groupableEntity.GetComponent<CharacterInput>().characterTurnVariables.isTurnActive)
            {
                return groupableEntity.GetComponent<CharacterInput>();
            }
        }

        return null;
    }
}
