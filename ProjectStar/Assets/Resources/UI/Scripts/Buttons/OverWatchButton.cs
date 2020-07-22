using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWatchButton : MonoBehaviour
{
    [Header("INSERT TURN MANAGER SO :")]
    public TurnManager_SO TurnManager;
    [Header("INSERT UI MANAGER SO :")]
    public UIManager_SO uiManager;

    public string buttonToolTip;
    public ToolTipPanel toolTipPanel;

    // Start is called before the first frame update
    void Start()
    {
        uiManager.overWatchButton = this;
    }

    public void Overwatch()
    {
        foreach (var character in TurnManager.playerTeamList)
        {
            if (character.GetComponent<CharacterTurn>().characterTurnVariables.isTurnActive)
            {
                character.GetComponent<CharacterCombat>().PrepareOverWatch();
                break;
            }
        }
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
