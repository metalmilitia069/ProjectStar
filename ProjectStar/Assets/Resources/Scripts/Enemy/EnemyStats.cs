using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("CHARACTER STATS VARIABLES - INSTANCE :")]
    public EnemyStats_SO EnemyStatsVariables;


    [Header("ENEMY STATS CONFIGURATION :")]
    public Sprite enemySigil;

    // Start is called before the first frame update
    void Start()
    {
        SetEnemySigil();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnemySigil()
    {
        EnemyStatsVariables.enemySigilReference = enemySigil;
    }

}
