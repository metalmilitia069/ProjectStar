using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnConfirmPanel : MonoBehaviour
{
    [Header("INSERT TURN MANAGER SO :")]
    public TurnManager_SO TurnManager;
    [Header("INSERT UI MANAGER SO :")]
    public UIManager_SO uiManager;


    public EndTurnConfirmationButton yesButton;
    public EndTurnConfirmationButton noButton;

    // Start is called before the first frame update
    void Start()
    {
        uiManager.endTurnConfirmPanel = this;
        //GetComponent<TurnPanelTween>().ToggleTween();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
