using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInjection : MonoBehaviour
{
    [Header("INSERT ENEMY CHARACTERS LIST HERE:")]
    public EnemyList_SO listOfAllEnemies_SO;

    private void OnEnable()
    {

        GetComponent<EnemyCombat>().EnemyCombatVariables = ScriptableObject.CreateInstance<EnemyCombat_SO>();
        GetComponent<EnemyCombat>().EnemyCombatVariables.name = "InstanceEnemyCombat";

        GetComponent<EnemyStats>().EnemyStatsVariables = ScriptableObject.CreateInstance<EnemyStats_SO>();
        GetComponent<EnemyStats>().EnemyStatsVariables.name = "InstanceEnemyStats";

        GetComponent<EnemyTurn>().EnemyTurnVariables = ScriptableObject.CreateInstance<EnemyTurn_SO>();
        GetComponent<EnemyTurn>().EnemyTurnVariables.name = "InstanceEnemyTurn";

        GetComponent<EnemyInput>().EnemyCombatVariables = GetComponent<EnemyCombat>().EnemyCombatVariables;
        GetComponent<EnemyInput>().EnemyStatsVariables = GetComponent<EnemyStats>().EnemyStatsVariables;
        GetComponent<EnemyInput>().EnemyTurnVariables = GetComponent<EnemyTurn>().EnemyTurnVariables;


        listOfAllEnemies_SO.AddCharacter(this.GetComponent<EnemyInput>());
    }

    private void OnDisable()
    {
        listOfAllEnemies_SO.RemoveCharacter(this.GetComponent<EnemyInput>());
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
