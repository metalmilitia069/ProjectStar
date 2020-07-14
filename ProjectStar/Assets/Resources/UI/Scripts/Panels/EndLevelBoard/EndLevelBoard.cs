using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelBoard : MonoBehaviour
{
    [Header("INSERT TURN MANAGER SO :")]
    public TurnManager_SO TurnManager;
    [Header("INSERT UI MANAGER SO :")]
    public UIManager_SO uiManager;

    [Header("SIGIL BOARD VERSIONS")]
    public Sprite heroClassSigil;
    public Sprite assaultClassSigil;
    public Sprite sniperClassSigil;
    public Sprite heavyClassSigil;
    public Sprite supportClassSigil;

    [Header("INFO CARD PREFAB")]
    public GameObject infoCardPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
