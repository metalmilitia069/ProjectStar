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

    [Header("ENEMY GATHERED ATTACK STATS")]
    //Attack Stats
    [SerializeField]
    public int _enemyDamageModifier;
    [SerializeField]
    public float _enemyCriticalChanceModifier;
    [SerializeField]
    public float _enemyCriticalDamageModifier;

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

    [Header("ENEMY GATHERED ELEMENTAL ATTACK STATS")]
    [SerializeField]
    public int _enemyElementalDmgFire;
    [SerializeField]
    public int _enemyElementalDmgElectricity;
    [SerializeField]
    public int _enemyElementalDmgCold;
    [SerializeField]
    public int _enemyElementalDmgPoison;

    [Header("DEFENSE STATS")]

    [Header("PLAYER GATHERED DEFENSE STATS")]
    //Defense Stats
    [SerializeField]
    public int _playerArmorNormal;
    [SerializeField]
    public int _playerArmorBlindage;
    [SerializeField]
    public float _playerDodgeChance;

    [Header("ENEMY GATHERED DEFENSE STATS")]
    //Defense Stats
    [SerializeField]
    public int _enemyArmorNormal;
    [SerializeField]
    public int _enemyArmorBlindage;
    [SerializeField]
    public float _enemyDodgeChance;

    //TODO: Elemental Defense

    [Header("PLAYER GATHERED ELEMENTAL DEFENSE STATS")]
    [SerializeField]
    public int _playerElementalDefFire;
    [SerializeField]
    public int _playerElementalDefElectricity;
    [SerializeField]
    public int _playerElementalDefCold;
    [SerializeField]
    public int _playerElementalDefPoison;

    [Header("ENEMY GATHERED ELEMENTAL DEFENSE STATS")]
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

    [Header("CALCULATED VARIABLES TO DISPLAY")]
    public float finalAttackProbability;
    public int weaponDamageMedian;
    public int finalDamage;


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

    public void PlayerFinalAttackCalculation(EnemyInput enemy, CharacterInput characterInput)
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

                //_cachedWeapon.GetComponent<WeaponShooting>().Shoot(enemy, _finalDamage, characterInput);

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

            _cachedWeapon.GetComponent<WeaponShooting>().Shoot(enemy, _finalDamage, characterInput);
            //StartCoroutine(ApplyDamage());//(ApplyDamage(_cachedWeapon.GetComponent<WeaponShooting>().Shooting(), enemy, _finalDamage, characterInput));
            return;
            //enemy.GetComponent<EnemyCombat>().ApplyDamage(_finalDamage, characterInput);
        }

        ResetCalculaterVariables();
    }

    public IEnumerator ApplyDamage(Coroutine co, EnemyInput enemy, int _finaldamage, CharacterInput characterInput)
    {
        yield return co;
        enemy.GetComponent<EnemyCombat>().ApplyDamage(_finaldamage, characterInput);
        
        if (!characterInput.characterCombatVariables.isOverWatching)
        {
            Debug.Log("overwatching? " + characterInput.characterCombatVariables.isOverWatching);
            characterInput.GetComponent<CharacterTurn>().characterTurnVariables.actionPoints--;
        }
        yield return new WaitForSeconds(1);//co;

        if (characterInput.GetComponent<CharacterTurn>().characterTurnVariables.actionPoints <= 0 && !characterInput.characterCombatVariables.isOverWatching)
        {
            //Debug.Log("overwatching? " + characterInput.characterCombatVariables.isOverWatching);
            characterInput.ChangeMode();
            characterInput.GetComponent<CharacterTurn>().ResetActionPoints();
            characterInput.TurnManager.RemoveFromTurn(characterInput.GetComponent<CharacterTurn>(), null);
        }
        ResetCalculaterVariables();
    }



    public string DisplayShotChance()
    {
        finalAttackProbability = _weaponSuccessShotProbability - _enemyDodgeChance;
        weaponDamageMedian = (int)((_cachedWeapon.weaponBasicVariables.maxDamage + _cachedWeapon.weaponBasicVariables.minDamage) / 2);
        finalDamage = weaponDamageMedian + _playerDamageModifier - _enemyArmorNormal - _enemyArmorBlindage;
        _finalCriticalProbability = _weaponCriticalChance + _playerCriticalChanceModifier;
        string probabilityText = ("Shot Success Chance = " + finalAttackProbability * 100 + "%  || Damage Preview = " + finalDamage);
        return probabilityText;
    }

    public void ResetCalculaterVariables()
    {
        _finalDamage = default;
        _finalCriticalProbability = default;
    }

    //NPC AI COMBAT CALCULATIONS

    public void GatherPlayerDefenseStats(CharacterInput CharacterRef)//(CharacterInput character, EnemyInput enemyRef)
    {
        _playerArmorNormal = CharacterRef.characterStatsVariables._armorNormal;//
        _playerArmorBlindage = CharacterRef.characterStatsVariables._armorBlindage;//
        _playerDodgeChance = CharacterRef.characterStatsVariables._dodgeChance;//        
    }

    public void GatherEnemyAttackStats(EnemyInput enemyRef)
    {
        _enemyDamageModifier = enemyRef.EnemyStatsVariables._damageModifier;//   
        _enemyCriticalChanceModifier = enemyRef.EnemyStatsVariables._criticalChanceModifier;//        
        _enemyCriticalDamageModifier = enemyRef.EnemyStatsVariables._criticalDamageModifier;//
    }

    public void EnemyFinalAttackCalculation(CharacterInput character, EnemyInput enemyInput)
    {
        float finalAttackProbability = _weaponSuccessShotProbability - _playerDodgeChance;
        float diceRoll = Random.Range(0.0f, 1.0f);
        bool success = (diceRoll <= finalAttackProbability);
        
        
            if (success)
            {
                int finalDamage = _weaponCalculatedBaseDamage + _enemyDamageModifier - _playerArmorNormal - _playerArmorBlindage;
                _finalDamage = finalDamage;
                float finalCriticalProbability = _weaponCriticalChance + _enemyCriticalChanceModifier;
                _finalCriticalProbability = finalCriticalProbability;
                float diceRoll02 = Random.Range(0.0f, 1.0f);
                bool success02 = (diceRoll02 <= finalCriticalProbability);

                if (success02)
                {
                    finalDamage = (finalDamage * ((int)(_weaponCriticalDamage + _enemyCriticalDamageModifier)));
                    _finalDamage = finalDamage;
                    Debug.Log("Enemy Critical Shot Success!!!");
                }
            }
            else
            {
                Debug.Log("Enemy Shot MISSED!!!");
                _finalDamage = 0;
            }


            Debug.Log("Calculated Critical Chance = " + _finalCriticalProbability);
            Debug.Log("FINAL DAMAGE ON ENEMY = " + _finalDamage);

            

            character.GetComponent<CharacterCombat>().ApplyDamage(_finalDamage, enemyInput);
        

        ResetCalculaterVariables();
    }
}
