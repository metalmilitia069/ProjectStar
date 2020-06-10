using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicTileVariables", menuName = "Tiles/Basic")]
[System.Serializable]
public class BasicTile_SO : ScriptableObject
{
    public bool isWalkable = true;
    public bool isCurrent = false;
    public bool isCover = false;
    public bool isLatter = false;
    public bool isSelectable = false;
    public bool isTarget = false;

    public List<BasicTile> listOfNearbyValidTiles;

    public int jumpHeight = 2;

    private BasicTile referenceTile;

    public bool isVisited = false;
    public int distance = 0;

    public BasicTile parent = default;
}
