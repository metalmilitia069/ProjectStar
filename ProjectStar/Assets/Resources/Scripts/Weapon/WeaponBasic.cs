﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeaponBasic : MonoBehaviour
{
    [Header("INSERT A COMBAT CALCULATOR MANAGER SO :")]
    public CombatCalculatorManager_SO CombatCalculatorManager;


    [Header("BASIC WEAPON VARIABLES - INSTANCE :")]
    public WeaponBasic_SO weaponBasicVariables;

    [Header("WEAPON INITIAL SETUP : ")]
    public WeaponClass weaponClass;

    [Header("WEAPON PREFAB :")]
    public GameObject weaponPrefab;

    [Header("WEAPON SOCKETS SETUP :")]
    public GameObject weaponGripSocket;
    public GameObject weaponHolsterSocket;
    public GameObject firePoint;

    [Header("WEAPON UI SETUP")]
    public Sprite weaponDisplaySprite;
    public string weaponName;

    // Start is called before the first frame update
    void Awake()//Start()
    {
        weaponBasicVariables.weaponClass = weaponClass;

        weaponBasicVariables.weaponPrefab = weaponPrefab;

        weaponBasicVariables.weaponGripSocket = weaponGripSocket;
        weaponBasicVariables.weaponHolsterSocket = weaponHolsterSocket;
        weaponBasicVariables.firePoint = firePoint;

        weaponBasicVariables.weaponDisplaySprite = weaponDisplaySprite;
        weaponBasicVariables.weaponName = weaponName;


        weaponBasicVariables.WeaponBasicSetup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GatherWeaponAttackStats(CharacterInput character, EnemyInput enemy)
    {
        //transform.LookAt(enemy.transform);
        Ray ray = new Ray(weaponBasicVariables.firePoint.transform.position, transform.forward * 100);
        Debug.DrawRay(weaponBasicVariables.firePoint.transform.position, transform.forward * 100, Color.red, 2);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, Vector3.Distance(this.transform.position, enemy.transform.position));

        foreach (var hit in hits)
        {
            TileModifier cover = hit.collider.GetComponent<TileModifier>();

            if (cover && cover.isCover)
            {                
                if (cover.isHalfCover)
                {
                    Debug.Log("Hit HALF Cover");
                    weaponBasicVariables.successShotProbability -= cover.halfCoverPenalty;
                    weaponBasicVariables.isHalfCover = true;
                }
                else if (cover.isFullCover)//&& !isCoverComputed)
                {
                    Debug.Log("Hit FULL Cover");
                    weaponBasicVariables.successShotProbability -= cover.fullCoverPenalty;
                    weaponBasicVariables.isFullCover = true;
                }
            }

            EnemyInput enemyclass = hit.collider.GetComponent<EnemyInput>();

            if (enemyclass)
            {
                //Debug.Log("Hit Enemy!!!");
                weaponBasicVariables.distanceFromTarget = Vector3.Distance(character.transform.position, enemy.transform.position);
                //Debug.Log("Distance From The Target: " + distanceFromTarget);
                if (weaponBasicVariables.optimalRange + 1 >= weaponBasicVariables.distanceFromTarget && weaponBasicVariables.optimalRange - 1 <= weaponBasicVariables.distanceFromTarget)//
                {
                    weaponBasicVariables.damagePenalty -= 0f;
                    weaponBasicVariables.successShotProbability -= 0f;
                }
                else
                {
                    weaponBasicVariables.damagePenalty -= 0.2f;
                    weaponBasicVariables.successShotProbability -= .1f;
                }
                CalculateBaseDamage();
            }
        }
    }

    public void CalculateBaseDamage()
    {
        
        weaponBasicVariables.calculatedBaseDamage = Random.Range(weaponBasicVariables.minDamage, weaponBasicVariables.maxDamage + 1);
        //Debug.Log("calculated Base Damage = " + calculatedBaseDamage);

        weaponBasicVariables.calculatedBaseDamage = (int)(weaponBasicVariables.calculatedBaseDamage * weaponBasicVariables.damagePenalty);
        //Debug.Log("calculated Base Damage * Range Penalty = " + calculatedBaseDamage);

        if (weaponBasicVariables.isHalfCover)
        {
            weaponBasicVariables.calculatedBaseDamage = (int)(weaponBasicVariables.calculatedBaseDamage * 0.80f);
            weaponBasicVariables.isHalfCover = false;
            //Debug.Log("isHalfCover");

        }
        else if (weaponBasicVariables.isFullCover)
        {
            weaponBasicVariables.calculatedBaseDamage = (int)(weaponBasicVariables.calculatedBaseDamage * 0.50f);
            weaponBasicVariables.isFullCover = false;
            //Debug.Log("isFullCover");
        }
        //Debug.Log("calculated Base Damage * Penalty * CoverAbsortion = " + calculatedBaseDamage);

        CombatCalculatorManager.GatherWeaponAttackStats(GetComponent<WeaponInput>());
        //CombatCalculatorManager.GatherWeaponAttackStats(GetComponent<WeaponInput>(), character, enemy);  



        //Debug.Log("Shot Success Chance = " + successShotProbability * 100 + "%");

        //Debug.Log("Shot Critical Chance = " + weaponCriticalChance * 100 + "%");

        //Debug.Log("Shot Critical Damage Rate = " + weaponCriticalDamage * 100 + "%");


        //Reset Odds
        weaponBasicVariables.successShotProbability = 1;
        weaponBasicVariables.damagePenalty = 1.0f;
    }

    //AI WEAPON METHODS
    public void GatherAIWeaponAttackStats(EnemyInput enemy, CharacterInput character)
    {
        transform.LookAt(character.transform);
        Ray ray = new Ray(weaponBasicVariables.firePoint.transform.position, transform.forward * 100);
        Debug.DrawRay(weaponBasicVariables.firePoint.transform.position, transform.forward * 100, Color.red, 2);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, Vector3.Distance(this.transform.position, character.transform.position));

        foreach (var hit in hits)
        {
            TileModifier cover = hit.collider.GetComponent<TileModifier>();

            if (cover && cover.isCover)
            {
                if (cover.isHalfCover)
                {
                    Debug.Log("Hit HALF Cover");
                    weaponBasicVariables.successShotProbability -= cover.halfCoverPenalty;
                    weaponBasicVariables.isHalfCover = true;
                }
                else if (cover.isFullCover)//&& !isCoverComputed)
                {
                    Debug.Log("Hit FULL Cover");
                    weaponBasicVariables.successShotProbability -= cover.fullCoverPenalty;
                    weaponBasicVariables.isFullCover = true;
                }
            }

            CharacterInput characterclass = hit.collider.GetComponent<CharacterInput>();

            if (characterclass)
            {
                //Debug.Log("Hit Enemy!!!");
                weaponBasicVariables.distanceFromTarget = Vector3.Distance(enemy.transform.position, character.transform.position);
                //Debug.Log("Distance From The Target: " + distanceFromTarget);
                if (weaponBasicVariables.optimalRange + 1 >= weaponBasicVariables.distanceFromTarget && weaponBasicVariables.optimalRange - 1 <= weaponBasicVariables.distanceFromTarget)//
                {
                    weaponBasicVariables.damagePenalty -= 0f;
                    weaponBasicVariables.successShotProbability -= 0f;
                }
                else
                {
                    weaponBasicVariables.damagePenalty -= 0.2f;
                    weaponBasicVariables.successShotProbability -= .1f;
                }
                CalculateBaseDamage();
            }
        }
    }
}
