using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTile : MonoBehaviour
{
    [Header("BASIC TILE VARIABLES - INSTANCE : ")]
    public BasicTile_SO basicTileVariables;
    // Start is called before the first frame update
    void Start()
    {
        ScanTiles(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetTileData()
    {
        //bool isWalkable = false;
        //isCover = false;
        basicTileVariables.isCurrent = false;//
        //isLatter = false;
        basicTileVariables.isSelectable = false;//
        basicTileVariables.isTarget = false;//



        basicTileVariables.listOfNearbyValidTiles.Clear();//



        basicTileVariables.referenceTile = default;//

        basicTileVariables.isVisited = false;//

        basicTileVariables.distance = 0;//

        basicTileVariables.parent = null;//


        //COMBAT STUFF

        basicTileVariables.isAttakable = false;


        //AI STUFF
        GetComponent<AdvancedTile>().advancedTileVariables.f = GetComponent<AdvancedTile>().advancedTileVariables.g = GetComponent<AdvancedTile>().advancedTileVariables.h = 0;

    }

    public void ScanTiles(AdvancedTile advancedTile)
    {
        ResetTileData();

        //Debug.Log("cucucucufhdgd");

        GatherNearbyTiles(Vector3.forward, advancedTile);
        GatherNearbyTiles(Vector3.back, advancedTile);
        GatherNearbyTiles(Vector3.left, advancedTile);
        GatherNearbyTiles(Vector3.right, advancedTile);

        if (basicTileVariables.isLatter)
        {
            DetectLatterTop();
        }

        //if (isCover)
        //{
        //    SetCovertTiles();
        //}

    }

    public void GatherNearbyTiles(Vector3 direction, AdvancedTile advancedTile)
    {
        Vector3 halfExtends = new Vector3(0.25f, basicTileVariables.jumpHeight, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtends);

        foreach (var col in colliders)
        {
            basicTileVariables.referenceTile = col.GetComponent<AdvancedTile>();

            if (basicTileVariables.referenceTile != null && basicTileVariables.referenceTile.basicTileVariables.isWalkable)
            {
                RaycastHit hit;

                if (!Physics.Raycast(basicTileVariables.referenceTile.transform.position, Vector3.up, out hit, 1) || (basicTileVariables.referenceTile == advancedTile))
                {
                    basicTileVariables.listOfNearbyValidTiles.Add(basicTileVariables.referenceTile);
                }
                else
                {
                    if (!this.basicTileVariables.isMoveMode)
                    {
                        if (hit.transform.GetComponent<EnemyInput>())
                        {
                            basicTileVariables.listOfNearbyValidTiles.Add(basicTileVariables.referenceTile);
                        }
                    }
                }

            }
        }
    }

    //public void GatherNearbyTilesAttackMode(Vector3 direction)
    //{
    //    Vector3 halfExtends = new Vector3(0.25f, jumpHeight, 0.25f);
    //    Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtends);

    //    foreach (var col in colliders)
    //    {
    //        referenceTile = col.GetComponent<Tile>();

    //        if (referenceTile != null && referenceTile.isWalkable)
    //        {
    //            RaycastHit hit;

    //            if (!Physics.Raycast(referenceTile.transform.position, Vector3.up, out hit, 1))
    //            {
    //                listOfNearbyValidTiles.Add(referenceTile);
    //            }
    //            else
    //            {
    //                if (hit.transform.GetComponent<EnemyBaseClass>())
    //                {
    //                    //referenceTile.isMoveMode = false;
    //                    //if (!referenceTile.isMoveMode)
    //                    //{
    //                    //Debug.Log("xibiru");
    //                    listOfNearbyValidTiles.Add(referenceTile);
    //                    //}

    //                }
    //            }

    //        }
    //    }
    //}    

    public void DetectLatterTop()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + basicTileVariables.latterSpotPosition, Vector3.up);

        foreach (var col in colliders)
        {
            AdvancedTile tile = col.GetComponent<AdvancedTile>();
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
