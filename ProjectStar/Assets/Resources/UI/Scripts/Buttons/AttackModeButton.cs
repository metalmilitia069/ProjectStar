using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackModeButton : MonoBehaviour
{
    public CharacterInput charInput;

    public string buttonToolTip;
    public ToolTipPanel toolTipPanel;

    public void ButtonAction()
    {
        SwitchToCombatMode();
    }

    public void SwitchToCombatMode()
    {
        charInput = GetComponent<ButtonInjection>().GetActiveCharacter();
        charInput.ChangeMode();
    }

    public void ActivateToolTip()
    {
        StartCoroutine(WaitToActivateToolTip());        
    }

    public void DeactivateToolTip()
    {
        StopAllCoroutines();
        toolTipPanel.gameObject.SetActive(false);
        toolTipPanel.toolTipText.text = default;
    }

    public IEnumerator WaitToActivateToolTip()
    {
        yield return new WaitForSeconds(2);
        toolTipPanel.gameObject.SetActive(true);
        toolTipPanel.toolTipText.text = buttonToolTip;
        toolTipPanel.transform.position = Input.mousePosition + new Vector3(-160, 40, 0);
    }
}
