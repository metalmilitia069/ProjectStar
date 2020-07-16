using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponBasic", menuName = "ScriptableVariables/Type: BasicWeapon")]
public class WeaponBasic_SO : ScriptableObject
{
    [Header("WEAPON PREFAB :")]
    public GameObject weaponPrefab;

    [Header("WEAPON SOCKETS :")]
    public GameObject weaponGripSocket;
    public GameObject weaponHolsterSocket;
    public GameObject firePoint;

    [Header("WEAPON CLASS :")]
    public WeaponClass weaponClass;

    [Header("WEAPON UI INFORMATION")]
    public Sprite weaponDisplaySprite;
    public string weaponName;

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
    public float successShotProbability = 1.0f;//->
    public float damagePenalty = 1.0f;//ok    
    public float weaponCriticalDamage;//->


    //[Header("COVER DAMAGE REDUCTION")]
    public bool isFullCover = false;
    public bool isHalfCover = false;

    //Weapon Stats Bullet Behavior
    [Header("WEAPON BEHAVIOR")]
    public bool hasSpread = false;
    public float fireRate;
    public int maxAmmunition;
    public int currentAmmunition;


    public void WeaponBasicSetup()
    {
        switch (weaponClass)
        {
            case WeaponClass.Melee:
                weaponRange = 1;
                optimalRange = 1;
                minDamage = 5;
                maxDamage = 7;
                weaponCriticalChance = .4f;
                weaponCriticalDamage = 1.5f;
                currentAmmunition = maxAmmunition = 0;

                successShotProbability = 1.0f;
                break;
            case WeaponClass.Gun:
                weaponRange = 5;
                optimalRange = 3;
                minDamage = 3;
                maxDamage = 5;
                weaponCriticalChance = .1f;
                weaponCriticalDamage = 1.5f;
                currentAmmunition = maxAmmunition = 6;

                fireRate = 2;

                successShotProbability = 1.0f;
                break;
            case WeaponClass.Rifle:
                weaponRange = 7;
                optimalRange = 5;
                minDamage = 4;
                maxDamage = 6;
                weaponCriticalChance = .1f;
                weaponCriticalDamage = 1.5f;
                currentAmmunition = maxAmmunition = 4;

                fireRate = 6;

                successShotProbability = 1.0f;
                break;
            case WeaponClass.MiniGun:
                weaponRange = 4;
                optimalRange = 3;
                minDamage = 2;
                maxDamage = 7;
                weaponCriticalChance = .1f;
                weaponCriticalDamage = 1.5f;
                currentAmmunition = maxAmmunition = 6;

                fireRate = 8;

                successShotProbability = 1.0f;
                break;
            case WeaponClass.Sniper:
                weaponRange = 8;
                optimalRange = 7;
                minDamage = 5;
                maxDamage = 7;
                weaponCriticalChance = .3f;
                weaponCriticalDamage = 1.5f;
                currentAmmunition = maxAmmunition = 4;

                fireRate = 1;

                successShotProbability = 1.0f;
                break;
            default:
                Debug.Log("No Weapon Selected");
                break;
        }
    }

    public int[] ReloadWeapon()
    {
        if (currentAmmunition == maxAmmunition)
        {
            Debug.Log("Ammunition is Full!!!");
            return null;
        }

        currentAmmunition = maxAmmunition;
        int[] bullets = { maxAmmunition, maxAmmunition};




        return bullets;
    }
}

public enum WeaponClass
{
    Melee,
    Gun,
    Rifle,
    MiniGun,
    Sniper,
}


