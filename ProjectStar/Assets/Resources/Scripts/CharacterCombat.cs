using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{    
    [Header("CHARACTER COMBAT VARIABLES - INSTANCE :")]
    public CharacterCombat_SO characterCombatVariables;








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


        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    Debug.Log("CUUUU");
        //    if (weapon.transform.parent == playerGrip.transform)
        //    {
        //        weapon.transform.parent = gunHolster.transform;

        //        weaponLocation = weaponHolsterPoint.transform.localPosition * (-1);
        //        weaponRotation = weaponHolsterPoint.transform.localRotation;

        //        weapon.transform.localPosition = weaponLocation;
        //        weapon.transform.localRotation = weaponRotation;
        //    }
        //    else //if (weapon.transform.parent == weaponHolsterPoint.transform)
        //    {
                
        //        weapon.transform.parent = playerGrip.transform;

        //        weaponLocation = weapongrip.transform.localPosition * (-1);
        //        weaponRotation = weapongrip.transform.localRotation;

        //        weapon.transform.localPosition = weaponLocation;
        //        weapon.transform.localRotation = weaponRotation;
        //    }
        //}



    }

    public void ChangeWeapon()
    {
        //weaponInstanceBelt[_currentWeaponIndex].GetComponent<WeaponBaseClass>().isCurrent = false;
        //weaponInstanceBelt[_currentWeaponIndex].transform.localPosition = weaponHolsters[_currentWeaponIndex].transform.localPosition;

        //if (_currentWeaponIndex < weaponInstanceBelt.Length - 1)
        //{
        //    _currentWeaponIndex++;
        //}
        //else
        //{
        //    _currentWeaponIndex = 0;
        //}

        //weaponInstanceBelt[_currentWeaponIndex].GetComponent<WeaponBaseClass>().isCurrent = true;
        //weaponInstanceBelt[_currentWeaponIndex].transform.localPosition = weaponGripPlace.transform.localPosition;

        //_weaponClass = weaponInstanceBelt[_currentWeaponIndex].GetComponent<WeaponBaseClass>().weaponClass;

        //isAttackRangeFound = false;

        //GridManager.instance.ClearSelectableTiles();
    }

    public void ScanForEnemies()
    {
        if (characterCombatVariables._listOfScannedEnemies.Count > 0)
        {
            foreach (var enemy in characterCombatVariables._listOfScannedEnemies)
            {
                enemy.EnemyStatsVariables.canBeAttacked = false;
            }
        }

        characterCombatVariables._listOfScannedEnemies.Clear();

        foreach (var item in GetComponent<CharacterInput>().GridManager.listOfSelectableTiles)
        {
            RaycastHit hit;
            if (Physics.Raycast(item.transform.position, Vector3.up, out hit, 1))
            {
                EnemyInput enemyPlaceHolder = hit.collider.GetComponent<EnemyInput>();
                if (enemyPlaceHolder)
                {
                    characterCombatVariables._listOfScannedEnemies.Add(enemyPlaceHolder);
                    enemyPlaceHolder.EnemyStatsVariables.canBeAttacked = true;

                }
            }
        }
    }

    public void Attack(EnemyInput enemy)
    {        
        transform.LookAt(enemy.transform);

        CharacterInput charInput = GetComponent<CharacterInput>();

        charInput.CombatCalculatorManager.isShowProbabilities = false;
        
        weapon.GetComponent<WeaponBasic>().GatherWeaponAttackStats(charInput, enemy); //weaponInstanceBelt[_currentWeaponIndex].GetComponent<WeaponBaseClass>().GatherWeaponAttackStats((CharacterStats)this, enemy);
        
        //charInput.CombatCalculatorManager.GatherEnemyDefenseStats(charInput, enemy); //CombatCalculatorManager.instance.GatherEnemyDefenseStats(enemy);
        //charInput.CombatCalculatorManager.GatherPlayerAttackStats(charInput, enemy); //CombatCalculatorManager.instance.GatherPlayerAttackStats((CharacterStats)this);
        //charInput.CombatCalculatorManager.PlayerFinalAttackCalculation(enemy); //CombatCalculatorManager.instance.PlayerFinalAttackCalculation(enemy);
        
        
        

        this.GetComponent<CharacterStats>().characterStatsVariables.actionPoints--;

        if (this.GetComponent<CharacterStats>().characterStatsVariables.actionPoints <= 0)
        {
            //TurnManager.instance.PlayerCharacterActionDepleted((CharacterStats)this);  //TODO: implement TURN MANAGER
        }

    }

    public void ShowProbability(EnemyInput enemy)
    {
        transform.LookAt(enemy.transform);

        CharacterInput charInput = GetComponent<CharacterInput>();
        //charInput.CombatCalculatorManager.isShowProbabilities = true;
        weapon.GetComponent<WeaponBasic>().GatherWeaponAttackStats(charInput, enemy); //weaponInstanceBelt[_currentWeaponIndex].GetComponent<WeaponBaseClass>().GatherWeaponAttackStats((CharacterStats)this, enemy);

        //charInput.CombatCalculatorManager.GatherEnemyDefenseStats(enemy); //CombatCalculatorManager.instance.GatherEnemyDefenseStats(enemy);
        //charInput.CombatCalculatorManager.GatherPlayerAttackStats(charInput); //CombatCalculatorManager.instance.GatherPlayerAttackStats((CharacterStats)this);
        //charInput.CombatCalculatorManager.PlayerFinalAttackCalculation(enemy); //CombatCalculatorManager.instance.DisplayShotChance();//(weaponInstanceBelt[_currentWeaponIndex].GetComponent<WeaponBaseClass>());

        enemy.ShowProbability();
    }
}
