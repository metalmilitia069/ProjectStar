using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStarter : MonoBehaviour
{
    [Header("INSERT A UI MANAGER SO :")]
    public UIManager_SO uiManager;


    // Start is called before the first frame update
    void Start()
    {
        uiManager.spawnedCrossSignUI = Instantiate(uiManager.crossSignUIGizmo, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
