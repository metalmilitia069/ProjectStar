using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "ScriptableVariables/Type: CharacterStats")]
public class CharacterStats_SO : ScriptableObject
{
    //Character Stats
    //Attack Stats
    [Header("ATTACK STATS")]
    [SerializeField]
    public int damageModifier = 0;
    [SerializeField]
    public float criticalChanceModifier = 0.0f;
    [SerializeField]
    public int attackRangeModifier = 0;
    [SerializeField]
    public float criticalDamageModifier = 0;

    //TODO: Elemental Attack
    [Header("ELEMENTAL ATTACK STATS")]
    [SerializeField]
    public int _elementalDmgFire = 0;
    [SerializeField]
    public int _elementalDmgElectricity = 0;
    [SerializeField]
    public int _elementalDmgCold = 0;
    [SerializeField]
    public int _elementalDmgPoison = 0;

    [Header("DEFENSE STATS")]
    //Defense Stats
    [SerializeField]
    public int _armorNormal = 0;
    [SerializeField]
    public int _armorBlindage = 0;
    [SerializeField]
    public float _dodgeChance = 0.0f;
    [SerializeField]
    public int health = 10;

    //TODO: Elemental Defense
    [Header("ELEMENTAL DEFENSE STATS")]
    [SerializeField]
    public int _elementalDefFire = 0;
    [SerializeField]
    public int _elementalDefElectricity = 0;
    [SerializeField]
    public int _elementalDefCold = 0;
    [SerializeField]
    public int _elementalDefPoison = 0;


    //Other Variables
    //[Header("TURN VARIABLES")]
    //public bool isTurnActive = false;

    public int maxActionPoints = 2;
}
