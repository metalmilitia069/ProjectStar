using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatCalculatorManager", menuName = "ScriptableManagers/Type: CombatCalculatorManager")]
public class CombatCalculatorManager_SO : ScriptableObject
{
    [Header("WEAPON GATHERED STATS")]
    [SerializeField]
    public int _weaponCalculatedBaseDamage;
    [SerializeField]
    public float _weaponSuccessShotProbability;
    [SerializeField]
    public float _weaponCriticalChance;
    [SerializeField]
    public float _weaponCriticalDamage;

    [Header("ATTACK STATS")]

    [Header("PLAYER GATHERED ATTACK STATS")]
    //Attack Stats
    [SerializeField]
    public int _playerDamageModifier;
    [SerializeField]
    public float _playerCriticalChanceModifier;
    [SerializeField]
    public float _playerCriticalDamageModifier;

    //TODO: Elemental Attack
    [Header("PLAYER GATHERED ELEMENTAL ATTACK STATS")]
    [SerializeField]
    public int _playerElementalDmgFire;
    [SerializeField]
    public int _playerElementalDmgElectricity;
    [SerializeField]
    public int _playerElementalDmgCold;
    [SerializeField]
    public int _playerElementalDmgPoison;

    [Header("DEFENSE STATS")]

    [Header("ENEMY GATHERED DEFENSE STATS")]
    //Defense Stats
    [SerializeField]
    public int _enemyArmorNormal;
    [SerializeField]
    public int _enemyArmorBlindage;
    [SerializeField]
    public float _enemyDodgeChance;

    //TODO: Elemental Defense
    [Header("ELEMENTAL DEFENSE STATS")]
    [SerializeField]
    public int _enemyElementalDefFire;
    [SerializeField]
    public int _enemyElementalDefElectricity;
    [SerializeField]
    public int _enemyElementalDefCold;
    [SerializeField]
    public int _enemyElementalDefPoison;

    [Header("MANAGER CALCULATED VARIABLES")]
    [SerializeField]
    public int _finalDamage;
    [SerializeField]
    public float _finalCriticalProbability;
    [SerializeField]
    public WeaponInput _cachedWeapon;
    [SerializeField]
    public bool isShowProbabilities = false;

    public void GatherWeaponAttackStats(WeaponInput weaponRef)//, CharacterInput character, EnemyInput enemy)
    {
        _cachedWeapon = weaponRef;

        _weaponCalculatedBaseDamage = weaponRef.weaponBasicVariables.calculatedBaseDamage;//
        _weaponSuccessShotProbability = weaponRef.weaponBasicVariables.successShotProbability;//
        _weaponCriticalChance = weaponRef.weaponBasicVariables.weaponCriticalChance;//
        _weaponCriticalDamage = weaponRef.weaponBasicVariables.weaponCriticalDamage;//        
    }

    public void GatherEnemyDefenseStats(EnemyInput enemyRef)//(CharacterInput character, EnemyInput enemyRef)
    {
        _enemyArmorNormal = enemyRef.EnemyStatsVariables.armorNormal;//
        _enemyArmorBlindage = enemyRef.EnemyStatsVariables.armorBlindage;//
        _enemyDodgeChance = enemyRef.EnemyStatsVariables.dodgeChance;//        
    }

    public void GatherPlayerAttackStats(CharacterInput characterRef)
    {
        _playerDamageModifier = characterRef.characterStatsVariables.damageModifier;//   
        _playerCriticalChanceModifier = characterRef.characterStatsVariables.criticalChanceModifier;//        
        _playerCriticalDamageModifier = characterRef.characterStatsVariables.criticalDamageModifier;//
    }

    public void PlayerFinalAttackCalculation(EnemyInput enemy)
    {        
        float finalAttackProbability = _weaponSuccessShotProbability - _enemyDodgeChance;
        float diceRoll = Random.Range(0.0f, 1.0f);
        bool success = (diceRoll <= finalAttackProbability);
        

        if (!isShowProbabilities)
        {
            if (success)
            {
                int finalDamage = _weaponCalculatedBaseDamage + _playerDamageModifier - _enemyArmorNormal - _enemyArmorBlindage;
                _finalDamage = finalDamage;
                float finalCriticalProbability = _weaponCriticalChance + _playerCriticalChanceModifier;
                _finalCriticalProbability = finalCriticalProbability;
                float diceRoll02 = Random.Range(0.0f, 1.0f);
                bool success02 = (diceRoll02 <= finalCriticalProbability);

                if (success02)
                {
                    finalDamage = (finalDamage * ((int)(_weaponCriticalDamage + _playerCriticalDamageModifier)));
                    _finalDamage = finalDamage;
                    Debug.Log("Critical Shot Success!!!");
                }
            }
            else
            {
                Debug.Log("Shot MISSED!!!");
                _finalDamage = 0;
            }


            Debug.Log("Calculated Critical Chance = " + _finalCriticalProbability);
            Debug.Log("FINAL DAMAGE ON ENEMY = " + _finalDamage);

            isShowProbabilities = true;

            enemy.GetComponent<EnemyCombat>().ApplyDamage(_finalDamage);
        }        

        ResetCalculaterVariables();
    }   

    public string DisplayShotChance()
    {
        float finalAttackProbability = _weaponSuccessShotProbability - _enemyDodgeChance;
        int weaponDamageMedian = (int)((_cachedWeapon.weaponBasicVariables.maxDamage + _cachedWeapon.weaponBasicVariables.minDamage) / 2);
        int finalDamage = weaponDamageMedian + _playerDamageModifier - _enemyArmorNormal - _enemyArmorBlindage;
        string probabilityText = ("Shot Success Chance = " + finalAttackProbability * 100 + "%  || Damage Preview = " + finalDamage);
        return probabilityText;
    }

    public void ResetCalculaterVariables()
    {
        _finalDamage = default;
        _finalCriticalProbability = default;
    }
}
