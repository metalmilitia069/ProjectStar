using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIManager", menuName = "ScriptableManagers/Type: UIManager")]
public class UIManager_SO : ScriptableObject
{
    public UIGizmoSign crossSignUIGizmo;
    
    public UIGizmoSign spawnedCrossSignUI;

    public bool canAttackPanelDataBeTurnedOff = false;



    public AttackModeButton attackModeButton;
    public ReloadButton reloadButton;
    public ChangeWeaponButton changeWeaponButton;

    public EndTurnButton endTurnButton;
    


    public WeaponDisplayPanel weaponDisplayPanel;
    public TurnPanel turnPanel;
    public EndTurnConfirmPanel endTurnConfirmPanel;


    [Header("UI SPRITES COLLECTION FOR BULLET COUNT :")]
    public Sprite[] magazine2;
    public Sprite[] magazine4;
    public Sprite[] magazine6;
    public Sprite[] magazine8;
    public Sprite[] magazine10;

    public void DisableButtons()
    {
        attackModeButton.gameObject.SetActive(false);
        reloadButton.gameObject.SetActive(false);
        changeWeaponButton.gameObject.SetActive(false);
        endTurnButton.gameObject.SetActive(false);
    }

    public void EnableButtons()
    {
        attackModeButton.gameObject.SetActive(true);
        reloadButton.gameObject.SetActive(true);
        changeWeaponButton.gameObject.SetActive(true);
        endTurnButton.gameObject.SetActive(true);
    }

    public Sprite DisplayBullets(int maxAmmo, int curAmmo)
    {
        Sprite[] magazine = default;

        switch (maxAmmo)
        {
            case 2:
                magazine = magazine2;
                break;
            case 4:
                magazine = magazine4;
                break;
            case 6:
                magazine = magazine6;
                break;
            case 8:
                magazine = magazine8;
                break;
            case 10:
                magazine = magazine10;
                break;
            default:
                Debug.Log("Weapon Magazine Not Located!!!");

                return null;//magazine2[2];                
        }

        return magazine[curAmmo];
    }

    private void OnDisable()
    {
        spawnedCrossSignUI = default;
        canAttackPanelDataBeTurnedOff = false;
        attackModeButton = default;
        changeWeaponButton = default;

        weaponDisplayPanel = default;
        turnPanel = default;
    }


}
