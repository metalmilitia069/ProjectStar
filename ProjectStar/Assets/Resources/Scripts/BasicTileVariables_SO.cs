using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicTileVariables", menuName = "Tiles/BasicVariables")]
public class BasicTileVariables_SO : ScriptableObject
{
    public bool isWalkable = true;
    public bool isCurrent = false;
    public bool isCover = false;
    public bool isLatter = false;
    public bool isSelectable = false;
    public bool isTarget = false;

    public List<BasicTile> listOfNearbyValidTiles = new List<BasicTile>();

    public int jumpHeight = 2;

    public BasicTile referenceTile;

    public bool isVisited = false;
    public int distance = 0;

    public BasicTile parent = default;

    //Combat Stuff
    public bool isAttakable = false;
    public bool isAttackMode = false;
    public bool isMoveMode = true;

    //Latter
    public Vector3 latterSpotPosition;



}
