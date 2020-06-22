using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterTurn", menuName = "ScriptableVariables/Type: CharacterTurn")]
public class CharacterTurn_SO : ScriptableObject
{
    [Header("TURN VARIABLES")]
    [SerializeField]
    public int teamId;
    public bool isTurnActive = false;
    public int actionPoints = 2;
    public bool canMouseOver = true;

}
