using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridManager", menuName = "ScriptableManagers/Type: GridManager")]
public class GridManager_SO : ScriptableObject
{
    [Header("INSERT ALL TILES LIST HERE:")]
    public TileList_SO tileList_SO;


    [Header("Tile Data")]
    public List<AdvancedTile> listOfSelectableTiles = new List<AdvancedTile>();
    //public List<AdvancedTile> listOfAllTilesInLevel;

    private AdvancedTile tilePlaceholder;




    public CharacterInput baseCharacter;

    [SerializeField]
    public Stack<AdvancedTile> stackTilePath = new Stack<AdvancedTile>();


    //public delegate void OnScanTiles();
    //public static event OnScanTiles EventScanTilesUpdate;



    //combat 
    public bool _isCombatMode = false;

    private void OnDisable()
    {
        listOfSelectableTiles.Clear();
        baseCharacter = default;
        stackTilePath = default;
        tilePlaceholder = default;
    }

    


    public void GetCurrentTile(GameObject characterStandingOnTile)
    {
        RaycastHit hit;
        Debug.DrawRay(characterStandingOnTile.transform.position, Vector3.down);

        if (Physics.Raycast(characterStandingOnTile.transform.position, Vector3.down, out hit, 1))
        {
            tilePlaceholder = hit.collider.transform.GetComponent<AdvancedTile>();

            tilePlaceholder.basicTileVariables.isCurrent = true;


            tilePlaceholder.basicTileVariables.isMoveMode = baseCharacter.characterMoveVariables._isMoveMode; //To CHange Move or Attack Tile Neighbours


            if (tilePlaceholder.basicTileVariables.isCover) //&& tilePlaceholder.isCurrent)
            {
                baseCharacter.GetComponent<CharacterMove>().CoverMode(true);
            }
            else
            {
                baseCharacter.GetComponent<CharacterMove>().CoverMode(false);
            }
        }
    }

    //NEWWWWWWWWWWWWWWWWWWWWWWWWW

    public void UpdateScanAllTiles()
    {
        foreach (var tile in tileList_SO.GetList())
        {
            tile.GetComponent<BasicTile>().ScanTiles();
        }
    }

    //NEWWWWWWWWWWWWWWWWWWWWWWWWW

    public void CalculateAvailablePath(GameObject character)
    {
        baseCharacter = character.GetComponent<CharacterInput>();



        UpdateScanAllTiles(); //EventScanTilesUpdate();
        GetCurrentTile(character);



        //BFS Algorithm
        var queueProcess = new Queue<AdvancedTile>();

        queueProcess.Enqueue(tilePlaceholder);
        tilePlaceholder.basicTileVariables.isVisited = true;

        while (queueProcess.Count > 0)
        {
            AdvancedTile t = queueProcess.Dequeue();

            listOfSelectableTiles.Add(t);
            t.basicTileVariables.isSelectable = true;

            if (t.basicTileVariables.distance < baseCharacter.characterMoveVariables._movePoints)
            {
                foreach (var tile in t.basicTileVariables.listOfNearbyValidTiles)
                {
                    if (!tile.basicTileVariables.isVisited)
                    {
                        tile.basicTileVariables.parent = t;
                        tile.basicTileVariables.isVisited = true;
                        tile.basicTileVariables.distance = 1 + t.basicTileVariables.distance;
                        queueProcess.Enqueue(tile);
                    }
                }
            }
        }
        baseCharacter.characterMoveVariables.currentTile = tilePlaceholder;
        baseCharacter.characterMoveVariables.isTilesFound = true;
    }

    public void CalculatePathToDesignatedTile(AdvancedTile tile)
    {
        tile.basicTileVariables.isTarget = true;
        baseCharacter.characterMoveVariables.isMoving = true;

        stackTilePath.Clear();

        AdvancedTile next = tile;

        while (next != null)
        {
            stackTilePath.Push(next);
            next = next.basicTileVariables.parent;
        }
    }

    public void ClearSelectableTiles()
    {
        if (tilePlaceholder != null)
        {
            tilePlaceholder.basicTileVariables.isCurrent = false;
            tilePlaceholder = default;
        }

        foreach (var tile in listOfSelectableTiles)
        {
            tile.GetComponent<BasicTile>().ResetTileData();
        }

        listOfSelectableTiles.Clear();
    }

    public void CalculateAttackPath(GameObject character)
    {
        baseCharacter = character.GetComponent<CharacterInput>();

        UpdateScanAllTiles(); //EventScanTilesUpdate();
        GetCurrentTile(character);

        //BFS Algorithm
        var queueProcess = new Queue<AdvancedTile>();

        queueProcess.Enqueue(tilePlaceholder);
        tilePlaceholder.basicTileVariables.isVisited = true;

        while (queueProcess.Count > 0)
        {
            AdvancedTile t = queueProcess.Dequeue();

            listOfSelectableTiles.Add(t);
            t.basicTileVariables.isAttakable = true;

            if (t.basicTileVariables.distance < baseCharacter.characterMoveVariables._weaponRange)
            {
                foreach (var tile in t.basicTileVariables.listOfNearbyValidTiles)
                {
                    if (!tile.basicTileVariables.isVisited)
                    {
                        tile.basicTileVariables.parent = t;
                        tile.basicTileVariables.isVisited = true;
                        tile.basicTileVariables.distance = 1 + t.basicTileVariables.distance;
                        queueProcess.Enqueue(tile);
                    }
                }
            }
        }
        baseCharacter.characterMoveVariables.currentTile = tilePlaceholder;
        baseCharacter.characterMoveVariables.isAttackRangeFound = true;
    }
}
