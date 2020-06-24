using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("CHARACTER COMBAT VARIABLES - INSTANCE :")]
    public EnemyCombat_SO EnemyCombatVariables;



    //teste de sacar arma futuro belt
    public GameObject weapon;
    public GameObject weapongrip;
    public GameObject playerGrip;
    public GameObject gunHolster;
    public GameObject weaponHolsterPoint;

    private Vector3 weaponLocation;
    private Quaternion weaponRotation;

    // Start is called before the first frame update
    void Start()
    {
        weapon.transform.parent = playerGrip.transform;

        weaponLocation = weapongrip.transform.localPosition * (-1);
        weaponRotation = weapongrip.transform.localRotation;

        weapon.transform.localPosition = weaponLocation;
        weapon.transform.localRotation = weaponRotation;
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
            GetComponent<EnemyInput>().TurnManager.RemoveFromTeam(null, GetComponent<EnemyTurn>());
            Destroy(this.gameObject);
        }
    }


}
