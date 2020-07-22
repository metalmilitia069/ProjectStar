using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToolTipButton : MonoBehaviour
{
    [Header("INSERT UI MANAGER SO :")]
    public UIManager_SO uiManager;

    public GameObject toolTipPanel;

    // Start is called before the first frame update
    void Awake()
    {
        uiManager.mouseToolTipButton = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
