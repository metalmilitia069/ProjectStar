using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipPanel : MonoBehaviour
{
    [Header("INSERT UI MANAGER SO :")]
    public UIManager_SO uiManager;

    public Text toolTipText;

    private void Start()
    {
        uiManager.toolTipPanel = this;
    }


}
