using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    [Header("INSERT TURN MANAGER SO :")]
    public TurnManager_SO TurnManager;
    [Header("INSERT UI MANAGER SO :")]
    public UIManager_SO uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager.endTurnButton = this;
    }

    public void EndPlayerTurn()
    {
        TurnManager.EndPlayerTurn();
        uiManager.DisableButtons();
        uiManager.endTurnConfirmPanel.GetComponent<TurnPanelTween>().ToggleTween();
    }

    public void AskEndTurnConfirmation()
    {
        uiManager.DisableButtons();
        uiManager.endTurnConfirmPanel.GetComponent<TurnPanelTween>().ToggleTween();
    }

    public void EndTurnYes()
    {
        EndPlayerTurn();
    }

    public void EndTurnNo()
    {
        uiManager.EnableButtons();
        uiManager.endTurnConfirmPanel.GetComponent<TurnPanelTween>().ToggleTween();
    }


}
