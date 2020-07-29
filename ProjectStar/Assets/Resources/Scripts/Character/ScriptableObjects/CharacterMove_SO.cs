using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterMove", menuName = "ScriptableVariables/Type: CharacterMove")]
public class CharacterMove_SO : ScriptableObject
{
    [Header("MOVEMENT VARIABLES :")]    
    [SerializeField]
    public List<AdvancedTile> selectableTiles = new List<AdvancedTile>();
    [SerializeField]
    public AdvancedTile currentTile;

    [SerializeField]
    public int _movePoints = 10;
    public bool isMoving = false;
    public float halfHeight = 1;
    public bool isTilesFound = false;
    [SerializeField]
    public Stack<AdvancedTile> _stackTilePath = new Stack<AdvancedTile>();
    [SerializeField]
    public Vector3 _velocity = new Vector3();
    public Vector3 _movementDirection = new Vector3();
    public float moveSpeed = 2;
    public bool _isCoverMode = false;

    [Header("JUMP VARIABLES :")]
    //Jump Variables
    public bool fallingDown = false;
    public bool jumpingUp = false;
    public bool movingEdge = false;
    public Vector3 jumpTarget;
    public float jumpVelocity = 4.5f;

    [Header("COMBAT VARIABLES :")]
    //Combat Mode
    [SerializeField]
    public int _weaponRange;
    public bool isAttackRangeFound = false;
    [SerializeField]
    public bool _isCombatMode = false;
    [SerializeField]
    public bool _isMoveMode = true;


}
