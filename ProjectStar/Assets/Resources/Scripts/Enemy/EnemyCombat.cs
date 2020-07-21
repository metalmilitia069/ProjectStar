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

    public void ScanRoutine()
    {
        ChooseTargetAI(ScanForPlayerCharacters());


    }

    public bool ScanForPlayerCharacters()
    {
        if (EnemyCombatVariables._listOfScannedCharacters.Count > 0)
        {
            foreach (var character in EnemyCombatVariables._listOfScannedCharacters)
            {
                character.characterStatsVariables.canBeAttacked = false;
            }
        }

        EnemyCombatVariables._listOfScannedCharacters.Clear();

        foreach (var item in GetComponent<EnemyInput>().GridManager.listOfSelectableTiles)
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

        return (EnemyCombatVariables._listOfScannedCharacters.Count > 0);
    }

    public void ChooseTargetAI(bool foundEnemies) //RIGHT NOW, IT LOOKS FOR THE CLOSEST PLAYER CHARACTER!!!!  MODIFY LATER TO INCLUDE OTHER CASES!!!!
    {
        if (!foundEnemies)
        {
            return;
        }

        float closest = float.MaxValue;
        int index = default;

        foreach (var character in EnemyCombatVariables._listOfScannedCharacters)
        {
            float distance = Vector3.Distance(this.transform.position, character.transform.position);
            if (distance < closest)
            {
                closest = distance;
                index = EnemyCombatVariables._listOfScannedCharacters.IndexOf(character);
            }
        }


        Attack(EnemyCombatVariables._listOfScannedCharacters[index]);
    }

    public void Attack(CharacterInput character)
    {
        transform.LookAt(character.transform);

        EnemyInput enemyInput = GetComponent<EnemyInput>();        

        weapon.GetComponent<WeaponBasic>().GatherAIWeaponAttackStats(enemyInput, character); //weaponInstanceBelt[_currentWeaponIndex].GetComponent<WeaponBaseClass>().GatherWeaponAttackStats((CharacterStats)this, enemy);

        enemyInput.CombatCalculatorManager.GatherPlayerDefenseStats(character); //CombatCalculatorManager.instance.GatherEnemyDefenseStats(enemy);
        enemyInput.CombatCalculatorManager.GatherEnemyAttackStats(enemyInput); //CombatCalculatorManager.instance.GatherPlayerAttackStats((CharacterStats)this);
        enemyInput.CombatCalculatorManager.EnemyFinalAttackCalculation(character, enemyInput); //CombatCalculatorManager.instance.PlayerFinalAttackCalculation(enemy);

        this.GetComponent<EnemyTurn>().EnemyTurnVariables.actionPoints--;

        //if (this.GetComponent<EnemyTurn>().EnemyTurnVariables.actionPoints <= 0)
        //{
        //    GetComponent<EnemyInput>().TurnManager.RemoveFromTurn(null, this.GetComponent<EnemyTurn>());
        //    return;
        //    //TurnManager.instance.PlayerCharacterActionDepleted((CharacterStats)this);  //TODO: implement TURN MANAGER
        //}

    }

    public void PrepareAIOverWatch()
    {
        //if (weapon.GetComponent<WeaponBasic>().weaponBasicVariables.currentAmmunition <= 0)
        //{
            //EnemyCombatVariables.isOverWatching = false;
            //return;
        //}

        if (GetComponent<EnemyInput>().EnemyTurnVariables.isTurnActive)
        {
            GetComponent<CharacterInput>().characterTurnVariables.actionPoints = 0;
            GetComponent<CharacterInput>().TurnManager.RemoveFromTurn(this.GetComponent<CharacterTurn>(), null);
        }

        EnemyCombatVariables.isOverWatching = true;
        OverWatch();
    }

    public void OverWatch()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, weapon.GetComponent<WeaponBasic>().weaponBasicVariables.weaponRange);

        foreach (var col in hitColliders)
        {
            if (col.gameObject.GetComponent<CharacterInput>())
            {
                if (col.gameObject.GetComponent<CharacterInput>().characterMoveVariables.isMoving)
                {
                    if (!col.gameObject.GetComponent<CharacterInput>().characterStatsVariables.isOverWatched)
                    {
                        Attack(col.gameObject.GetComponent<CharacterInput>());
                        col.gameObject.GetComponent<CharacterInput>().characterStatsVariables.isOverWatched = true;
                    }
                }
            }
        }
        //GetComponent<CharacterInput>().characterTurnVariables.actionPoints = 0;
    }



    //DAMAGE FROM PLAYER

    public void ApplyDamage(int Damage, CharacterInput characterInput)
    {
        int enemyHealth = GetComponent<EnemyStats>().EnemyStatsVariables.health -= Damage;

        if (enemyHealth <= 0)
        {
            Debug.Log("ENEMY IS DEAD!!!!");
            EnemyCombatVariables.isMarkedEnemy = false;

            EnemyInput enemyInput = GetComponent<EnemyInput>();
            enemyInput.EnemyStatsVariables.isAlive = false;

            characterInput.characterStatsVariables.missionKills++;
            characterInput.characterStatsVariables.allTimeKills += characterInput.characterStatsVariables.missionKills;

            enemyInput.enemySavedData.AddMissionEnemy(enemyInput);
            enemyInput.TurnManager.RemoveFromTeam(null, GetComponent<EnemyTurn>());
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }


}
