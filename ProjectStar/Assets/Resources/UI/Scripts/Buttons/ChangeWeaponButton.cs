using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponButton : MonoBehaviour
{
    [Header("INSERT A UI MANAGER SO :")]
    public UIManager_SO uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager.changeWeaponButton = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
