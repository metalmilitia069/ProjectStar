using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTurn", menuName = "ScriptableVariables/Type: EnemyTurn")]
public class EnemyTurn_SO : ScriptableObject
{
    [Header("TURN VARIABLES")]
    [SerializeField]    
    public bool isTurnActive = false;
    public int actionPoints = 2;
}
