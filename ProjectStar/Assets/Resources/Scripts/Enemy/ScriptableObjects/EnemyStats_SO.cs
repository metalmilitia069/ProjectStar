using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableVariables/Type: EnemyStats")]
public class EnemyStats_SO : ScriptableObject
{
    public bool canBeAttacked = false;

    //Enemy Stats
    //Attack Stats
    [Header("ATTACK STATS")]
    [SerializeField]
    public int _damageModifier = 0;
    [SerializeField]
    public float _criticalChanceModifier = 0.0f;
    [SerializeField]
    public int _attackRangeModifier = 0;
    [SerializeField]
    public float _criticalDamageModifier = 0;

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
    public int armorNormal = 0;
    [SerializeField]
    public int armorBlindage = 0;
    [SerializeField]
    public float dodgeChance = 0.0f;
    [SerializeField]
    public int health = 10;

    //TODO: Elemental Defense
    [Header("ELEMENTAL DEFENSE STATS")]
    [SerializeField]
    public int elementalDefFire = 0;
    [SerializeField]
    public int elementalDefElectricity = 0;
    [SerializeField]
    public int elementalDefCold = 0;
    [SerializeField]
    public int elementalDefPoison = 0;

    public int maxActionPoints = 2;

    [Header("ENEMY UI ELEMENTS")]
    public Sprite enemySigilReference;

    [Header("ENEMY STATISTICS")]
    public int playersKilled = 0;
    public int enemyLevel = 1;
    public bool isAlive = true;
    public string enemyCLass = "Normal Grunt";

}
