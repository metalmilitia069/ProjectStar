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

    public void DisableButtons()
    {
        attackModeButton.gameObject.SetActive(false);
    }

    public void EnableButtons()
    {
        attackModeButton.gameObject.SetActive(true);
    }
}
