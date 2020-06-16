using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("CHARACTER COMBAT VARIABLES - INSTANCE :")]
    public EnemyCombat_SO EnemyCombatVariables;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(int Damage)
    {
        int enemyHealth = GetComponent<EnemyStats>().EnemyStatsVariables.health -= Damage;

        if (enemyHealth <= 0)
        {
            Debug.Log("ENEMY IS DEAD!!!!");
            Destroy(this.gameObject);
        }
    }


}
