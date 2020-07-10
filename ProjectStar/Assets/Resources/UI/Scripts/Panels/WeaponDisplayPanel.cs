using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplayPanel : MonoBehaviour
{
    [Header("INSERT TURN MANAGER SO :")]
    public TurnManager_SO TurnManager;
    [Header("INSERT UI MANAGER SO :")]
    public UIManager_SO uiManager;

    [Header("PANEL RELATED ELEMENTS")]
    public Text weaponNameText;
    public Image weaponDisplayImage;
    public Image bulletsDisplayImage;

    private void Awake()
    {
        uiManager.weaponDisplayPanel = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
