using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BasicTile", menuName = "ScriptableVariables/Type: BasicTile")]
public class BasicTile_SO : ScriptableObject
{
    [TextArea(1, 10)]
    public string description = "Variables to Handle the Methods for the Basic Tile Class.";

    public bool isWalkable = true;
    public bool isCurrent = false;
    public bool isCover = false;
    public bool isLatter = false;
    public bool isSelectable = false;
    public bool isTarget = false;

    public List<AdvancedTile> listOfNearbyValidTiles = new List<AdvancedTile>();
    public AdvancedTile referenceTile;
    public AdvancedTile parent = default;

    public int jumpHeight = 2;


    public bool isVisited = false;
    public int distance = 0;

    //Latter Stuff
    public Vector3 latterSpotPosition;


    //COmbat Stuff
    public bool isAttakable = false;
    public bool isAttackMode = false;
    public bool isMoveMode = true;
}
