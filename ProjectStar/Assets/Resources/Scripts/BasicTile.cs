using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicTile : MonoBehaviour
{
    [Header("BASIC TILE VARIABLES")]
    public BasicTileVariables_SO basicTileVariables;

    public void ResetTileData()
    {
        basicTileVariables.isCurrent = false;//        
        basicTileVariables.isSelectable = false;//
        basicTileVariables.isTarget = false;//
        if (basicTileVariables.listOfNearbyValidTiles.Count > 0)
        {
            basicTileVariables.listOfNearbyValidTiles.Clear();//
        }
        basicTileVariables.referenceTile = default;//
        basicTileVariables.isVisited = false;//
        basicTileVariables.distance = 0;//
        basicTileVariables.parent = null;//


        //COMBAT STUFF

        basicTileVariables.isAttakable = false;
    }

    public void ScanTiles()
    {
        ResetTileData();

        //Debug.Log("cucucucufhdgd");

        GatherNearbyTiles(Vector3.forward);
        GatherNearbyTiles(Vector3.back);
        GatherNearbyTiles(Vector3.left);
        GatherNearbyTiles(Vector3.right);

        if (basicTileVariables.isLatter)
        {
            DetectLatterTop();
        }

        //if (isCover)
        //{
        //    SetCovertTiles();
        //}

    }

    public void GatherNearbyTiles(Vector3 direction)
    {
        Vector3 halfExtends = new Vector3(0.25f, basicTileVariables.jumpHeight, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtends);

        foreach (var col in colliders)
        {
            basicTileVariables.referenceTile = col.GetComponent<BasicTile>();

            if (basicTileVariables.referenceTile != null && basicTileVariables.referenceTile.basicTileVariables.isWalkable)
            {
                RaycastHit hit;

                if (!Physics.Raycast(basicTileVariables.referenceTile.transform.position, Vector3.up, out hit, 1))
                {
                    basicTileVariables.listOfNearbyValidTiles.Add(basicTileVariables.referenceTile);
                }
                else
                {                    
                    if (!this.basicTileVariables.isMoveMode)
                    {
                        //if (hit.transform.GetComponent<EnemyBaseClass>())
                        //{
                        //    basicTileVariables.listOfNearbyValidTiles.Add(basicTileVariables.referenceTile);
                        //}
                    }
                }
            }
        }
    }

    public void DetectLatterTop()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + basicTileVariables.latterSpotPosition, Vector3.up);

        foreach (var col in colliders)
        {
            BasicTile tile = col.GetComponent<BasicTile>();
            if (tile)
            {
                basicTileVariables.listOfNearbyValidTiles.Add(tile);
            }
        }
    }

    public void SetCovertTiles()
    {
        foreach (var tile in basicTileVariables.listOfNearbyValidTiles)
        {
            tile.basicTileVariables.isCover = true;
        }
    }
}


