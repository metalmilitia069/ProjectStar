using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : MonoBehaviour
{
    [Header("INSERT A COMBAT CALCULATOR MANAGER SO :")]
    public CombatCalculatorManager_SO CombatCalculatorManager;

    [Header("CHARACTER COMBAT VARIABLES - INSTANCE :")]
    public EnemyCombat_SO EnemyCombatVariables;
    [Header("CHARACTER STATS VARIABLES - INSTANCE :")]
    public EnemyStats_SO EnemyStatsVariables;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowProbability()
    {
        Debug.Log(CombatCalculatorManager.DisplayShotChance());
    }

    
}
