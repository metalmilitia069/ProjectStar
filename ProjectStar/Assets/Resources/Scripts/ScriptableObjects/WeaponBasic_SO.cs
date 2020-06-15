﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponBasic", menuName = "ScriptableVariables/Type: BasicWeapon")]
public class WeaponBasic_SO : ScriptableObject
{
    public WeaponClass weaponClass;

    public bool isCurrent = false;

    //Weapon Stats Calculations
    [Header("WEAPON STATS")]
    public int weaponRange;//ok
    public int optimalRange;//ok
    public int minDamage;//ok
    public int maxDamage;//ok
    public int calculatedBaseDamage;//->    
    public float distanceFromTarget;//ok


    public float weaponCriticalChance;//->
    public float successShotProbability = 1;//->
    public float damagePenalty = 1.0f;//ok    
    public float weaponCriticalDamage;//->

    //[Header("COVER DAMAGE REDUCTION")]
    private bool isFullCover = false;
    private bool isHalfCover = false;

    //Weapon Stats Bullet Behavior
    [Header("WEAPON BEHAVIOR")]
    public bool hasSpread = false;
    public float fireRate;


    public void WeaponBasicSetup()
    {
        switch (weaponClass)
        {
            case WeaponClass.Melee:
                weaponRange = 1;
                optimalRange = 1;
                minDamage = 5;
                maxDamage = 7;
                weaponCriticalChance = .1f;
                weaponCriticalDamage = 1.5f;
                break;
            case WeaponClass.Gun:
                weaponRange = 5;
                optimalRange = 3;
                minDamage = 3;
                maxDamage = 5;
                weaponCriticalChance = .1f;
                weaponCriticalDamage = 1.5f;
                break;
            case WeaponClass.Rifle:
                weaponRange = 7;
                optimalRange = 5;
                minDamage = 4;
                maxDamage = 6;
                weaponCriticalChance = .1f;
                weaponCriticalDamage = 1.5f;
                break;
            case WeaponClass.MiniGun:
                weaponRange = 4;
                optimalRange = 3;
                minDamage = 2;
                maxDamage = 7;
                weaponCriticalChance = .1f;
                weaponCriticalDamage = 1.5f;
                break;
            default:
                Debug.Log("No Weapon Selected");
                break;
        }
    }
}

public enum WeaponClass
{
    Melee,
    Gun,
    Rifle,
    MiniGun,
}
