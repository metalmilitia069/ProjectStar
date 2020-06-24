using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathAI : MonoBehaviour
{
    [Header("INSERT A GRIDMANAGER SO :")]
    public GridManager_SO GridManager;



    [Header("ENEMY PATH AI VARIABLES - INSTANCE :")]
    public EnemyPathAI_SO enemyPathAIVariables;


    public void FindNearestTarget()
    {
        List<GroupableEntities> targets = GetComponent<EnemyInput>().TurnManager.listOfAllCharacters.GetList();

        if (targets.Count == 0)
        {
            return;
        }

        GameObject nearest = null;
        float distance = float.MaxValue;

        foreach (var groupable in targets)
        {
            float d = Vector3.Distance(transform.position, groupable.transform.position); //Look at this method as solution later >>> Vector3.SqrMagnitude

            if (d < distance)
            {
                distance = d;
                nearest = groupable.gameObject;
            }
        }

        enemyPathAIVariables.characterTarget = nearest;
    }

    public void CalculatePath()
    {
        AdvancedTile advancedTile = GridManager.GetTileWhereTheSelectedCharacterTargetIs(characterTarget: enemyPathAIVariables.characterTarget); 
        FindPathAI(advancedTile);
    }


    //A-STAR ALGORITHM
    public void FindPathAI(AdvancedTile targetTile)
    {
        GridManager.UpdateScanAllTiles(targetTile);
        GridManager.GetCurrentTile(this.gameObject);



        List<AdvancedTile> openList = new List<AdvancedTile>();
        List<AdvancedTile> closeList = new List<AdvancedTile>();

        AdvancedTile currentTile = GridManager.GetTilePlaceHolder();
        openList.Add(currentTile);
        currentTile.advancedTileVariables.h = Vector3.Distance(currentTile.transform.position, targetTile.transform.position);
        currentTile.advancedTileVariables.f = currentTile.advancedTileVariables.h;

        while (openList.Count > 0)
        {
            //A* algorithm

            AdvancedTile t = FindLowestF(openList);

            closeList.Add(t);

            if (t == targetTile)
            {
                enemyPathAIVariables.actualTargetTile = FindEndTile(t);                
                GridManager.CalculateEnemyAIPathToDesignatedTile(enemyPathAIVariables.actualTargetTile);
                return;
            }

            foreach (var tile in t.basicTileVariables.listOfNearbyValidTiles)
            {
                if (closeList.Contains(tile))
                {
                    //Do nothing, already processed
                }
                else if (openList.Contains(tile))
                {
                    float tempG = t.advancedTileVariables.g + Vector3.Distance(tile.transform.position, t.transform.position);

                    if (tempG < tile.advancedTileVariables.g)
                    {
                        tile.basicTileVariables.parent = t;

                        tile.advancedTileVariables.g = tempG;
                        tile.advancedTileVariables.f = tile.advancedTileVariables.g + tile.advancedTileVariables.h;
                    }
                }
                else
                {
                    tile.basicTileVariables.parent = t;

                    tile.advancedTileVariables.g = t.advancedTileVariables.g + Vector3.Distance(tile.transform.position, t.transform.position);//g is the distance to beginning
                    tile.advancedTileVariables.h = Vector3.Distance(tile.transform.position, targetTile.transform.position);//h is the estimated distance to the end
                    tile.advancedTileVariables.f = tile.advancedTileVariables.g + tile.advancedTileVariables.h;

                    openList.Add(tile);
                }
            }
        }

        //todo: what do you if there is no path to the target tile???
        Debug.Log("Path not found");

    }

    protected AdvancedTile FindLowestF(List<AdvancedTile> list)
    {
        AdvancedTile lowest = list[0];

        foreach (var tile in list)
        {
            if (tile.advancedTileVariables.f < lowest.advancedTileVariables.f)
            {
                lowest = tile;
            }
        }

        list.Remove(lowest);

        return lowest;
    }

    protected AdvancedTile FindEndTile(AdvancedTile t)
    {
        Stack<AdvancedTile> tempPath = new Stack<AdvancedTile>();

        AdvancedTile next = t.basicTileVariables.parent;
        while (next != null)
        {
            tempPath.Push(next);
            next = next.basicTileVariables.parent;
        }

        if (tempPath.Count <= GetComponent<EnemyMove>().enemyMoveVariables._movePoints)
        {
            return t.basicTileVariables.parent;
        }

        AdvancedTile endTile = null;//default;
        for (int i = 0; i <= GetComponent<EnemyMove>().enemyMoveVariables._movePoints; i++)
        {
            endTile = tempPath.Pop();
        }

        return endTile;

    }


}
