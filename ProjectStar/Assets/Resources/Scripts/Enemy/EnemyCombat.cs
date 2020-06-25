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

    public void ScanForCharacters()
    {
        if (EnemyCombatVariables._listOfScannedCharacters.Count > 0)
        {
            foreach (var character in EnemyCombatVariables._listOfScannedCharacters)
            {
                character.characterStatsVariables.canBeAttacked = false;
            }
        }

        EnemyCombatVariables._listOfScannedCharacters.Clear();

        foreach (var item in GetComponent<CharacterInput>().GridManager.listOfSelectableTiles)
        {
            RaycastHit hit;
            if (Physics.Raycast(item.transform.position, Vector3.up, out hit, 1))
            {
                CharacterInput characterPlaceHolder = hit.collider.GetComponent<CharacterInput>();
                if (characterPlaceHolder)
                {
                    EnemyCombatVariables._listOfScannedCharacters.Add(characterPlaceHolder);
                    characterPlaceHolder.characterStatsVariables.canBeAttacked = true;

                }
            }
        }
    }

    public void Attack(CharacterInput character)
    {
        transform.LookAt(character.transform);

        EnemyInput enemyInput = GetComponent<EnemyInput>();        

        weapon.GetComponent<WeaponBasic>().GatherAIWeaponAttackStats(enemyInput, character); //weaponInstanceBelt[_currentWeaponIndex].GetComponent<WeaponBaseClass>().GatherWeaponAttackStats((CharacterStats)this, enemy);

        enemyInput.CombatCalculatorManager.GatherPlayerDefenseStats(character); //CombatCalculatorManager.instance.GatherEnemyDefenseStats(enemy);
        enemyInput.CombatCalculatorManager.GatherEnemyAttackStats(enemyInput); //CombatCalculatorManager.instance.GatherPlayerAttackStats((CharacterStats)this);
        enemyInput.CombatCalculatorManager.EnemyFinalAttackCalculation(character); //CombatCalculatorManager.instance.PlayerFinalAttackCalculation(enemy);

        this.GetComponent<EnemyTurn>().EnemyTurnVariables.actionPoints--;

        if (this.GetComponent<EnemyTurn>().EnemyTurnVariables.actionPoints <= 0)
        {
            GetComponent<EnemyInput>().TurnManager.RemoveFromTurn(null, this.GetComponent<EnemyTurn>());
            return;
            //TurnManager.instance.PlayerCharacterActionDepleted((CharacterStats)this);  //TODO: implement TURN MANAGER
        }

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
