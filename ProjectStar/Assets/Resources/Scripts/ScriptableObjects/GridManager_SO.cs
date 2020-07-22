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
    
    

    private AdvancedTile tilePlaceholder;

    public CharacterInput inputCharacter;
    public EnemyInput inputEnemy;

    [SerializeField]
    public Stack<AdvancedTile> stackTilePath = new Stack<AdvancedTile>();

    //combat 
    public bool _isCombatMode = false;

    private void OnDisable()
    {
        listOfSelectableTiles.Clear();
        inputCharacter = default;
        inputEnemy = default;
        stackTilePath = default;
        tilePlaceholder = default;
    }

    public AdvancedTile GetTilePlaceHolder()
    {
        return tilePlaceholder;
    }

    // USED BY PLAYER AND AI !!!!!!!!!!!!!!!!!!
    public void GetCurrentTile(GameObject characterStandingOnTile) 
    {
        RaycastHit hit;
        Debug.DrawRay(characterStandingOnTile.transform.position, Vector3.down);

        if (Physics.Raycast(characterStandingOnTile.transform.position, Vector3.down, out hit, 1))
        {
            tilePlaceholder = hit.collider.transform.GetComponent<AdvancedTile>();

            tilePlaceholder.basicTileVariables.isCurrent = true;


            if (characterStandingOnTile.GetComponent<CharacterInput>())
            {
                tilePlaceholder.basicTileVariables.isMoveMode = inputCharacter.characterMoveVariables._isMoveMode; //To CHange Move or Attack Tile Neighbours


                if (tilePlaceholder.basicTileVariables.isCover) //&& tilePlaceholder.isCurrent)
                {
                    inputCharacter.GetComponent<CharacterMove>().CoverMode(true);
                }
                else
                {
                    inputCharacter.GetComponent<CharacterMove>().CoverMode(false);
                }
            }
            else if (characterStandingOnTile.GetComponent<EnemyInput>()) //CHANGE TO WHILE TESTING AI
            {
                tilePlaceholder.basicTileVariables.isMoveMode = inputEnemy.EnemyMoveVariables._isMoveMode;

                if (tilePlaceholder.basicTileVariables.isCover)
                {
                    inputEnemy.GetComponent<EnemyMove>().CoverMode(true);
                }
                else
                {
                    inputEnemy.GetComponent<EnemyMove>().CoverMode(false);
                }
            }                                                             //CHANGE WHILE TESTING AI
        }
    }

    // USED BY PLAYER AND AI !!!!!!!!!!!!!!!!!!
    public void UpdateScanAllTiles(AdvancedTile advancedTile)
    {
        foreach (var tile in tileList_SO.GetList())
        {
            tile.GetComponent<BasicTile>().ScanTiles(advancedTile);
        }
    }

    public void CalculateAvailablePath(GameObject character)
    {
        //Debug.Log("listOfSelecatbleTiles.Count = " + listOfSelectableTiles.Count);
        inputCharacter = character.GetComponent<CharacterInput>();



        UpdateScanAllTiles(null); //EventScanTilesUpdate();
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

            if (t.basicTileVariables.distance < inputCharacter.characterMoveVariables._movePoints)
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
        inputCharacter.characterMoveVariables.currentTile = tilePlaceholder;
        inputCharacter.characterMoveVariables.isTilesFound = true;
    }

    public void CalculatePathToDesignatedTile(AdvancedTile tile)
    {
        tile.basicTileVariables.isTarget = true;
        inputCharacter.characterMoveVariables.isMoving = true;

        stackTilePath.Clear();

        AdvancedTile next = tile;

        while (next != null)
        {
            stackTilePath.Push(next);
            next = next.basicTileVariables.parent;
        }
    }

    // USED BY PLAYER AND AI !!!!!!!!!!!!!!!!!!
    public void ClearSelectableTiles()
    {
        if (tilePlaceholder != null)
        {
            tilePlaceholder.basicTileVariables.isCurrent = false;
            tilePlaceholder = default;
        }

        if (listOfSelectableTiles != null)
        {
            foreach (var tile in listOfSelectableTiles)
            {
                if (tile != null)
                {
                    //Debug.Log("tile name = " + tile.name);
                    tile.GetComponent<BasicTile>().ResetTileData();
                }
            }

            listOfSelectableTiles.Clear();
            //Debug.Log("listOfSelecatbleTiles.Count = " + listOfSelectableTiles.Count);
        }

    }

    public void CalculateAttackPath(GameObject character)
    {
        inputCharacter = character.GetComponent<CharacterInput>();

        UpdateScanAllTiles(null); //EventScanTilesUpdate();
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

            if (t.basicTileVariables.distance < inputCharacter.characterMoveVariables._weaponRange)
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
        inputCharacter.characterMoveVariables.currentTile = tilePlaceholder;
        inputCharacter.characterMoveVariables.isAttackRangeFound = true;
    }

    //AI/NPC RELATED METHODS

    public AdvancedTile GetTileWhereTheSelectedCharacterTargetIs(GameObject characterTarget)
    {
        RaycastHit hit;

        if (Physics.Raycast(characterTarget.transform.position, Vector3.down, out hit, 1))
        {
            tilePlaceholder = hit.collider.transform.GetComponent<AdvancedTile>();
        }

        return tilePlaceholder;
    }

    //##################################################################### AI-ONLY METHODS ##########################################################################################

    public void CalculateAvailablePathForTheAI(GameObject enemy)// THE ONLY FUNCTION OF THIS METHOD IS TO USE BFS ALGORITHM TO SHOW AI MOVEMENT OPTIONS. NOT RELEVANT TO ITS MOVEMENT
    {
        inputEnemy = enemy.GetComponent<EnemyInput>(); // REDUNDANT>>>>>>>>>>>>>>>>>>>>>>>>



        UpdateScanAllTiles(null); //EventScanTilesUpdate();
        GetCurrentTile(enemy);



        //BFS Algorithm
        var queueProcess = new Queue<AdvancedTile>();

        queueProcess.Enqueue(tilePlaceholder);
        tilePlaceholder.basicTileVariables.isVisited = true;

        while (queueProcess.Count > 0)
        {
            AdvancedTile t = queueProcess.Dequeue();

            listOfSelectableTiles.Add(t);
            t.basicTileVariables.isSelectable = true;

            if (t.basicTileVariables.distance < inputEnemy.EnemyMoveVariables._movePoints)
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
        inputEnemy.EnemyMoveVariables.currentTile = tilePlaceholder;
        inputEnemy.EnemyMoveVariables.isTilesFound = true;
    }

    public void CalculateEnemyAIPathToDesignatedTile(AdvancedTile tile)
    {
        tile.basicTileVariables.isTarget = true;
        inputEnemy.EnemyMoveVariables.isMoving = true;

        stackTilePath.Clear();

        AdvancedTile next = tile;

        while (next != null)
        {
            stackTilePath.Push(next);            
            next = next.basicTileVariables.parent;
        }

        // UNCOMMENT THIS IF YOU DONT NEED TO SHOW AI PATH OPTIONS
        //inputEnemy.EnemyMoveVariables.currentTile = tilePlaceholder;
        //inputEnemy.EnemyMoveVariables.isTilesFound = true;
    }

    public void CalculateAttackPathForTheAI(GameObject enemy)
    {
        inputEnemy = enemy.GetComponent<EnemyInput>(); //REDUNDANT >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        UpdateScanAllTiles(null); //EventScanTilesUpdate();
        GetCurrentTile(enemy);

        //BFS Algorithm
        var queueProcess = new Queue<AdvancedTile>();

        queueProcess.Enqueue(tilePlaceholder);
        tilePlaceholder.basicTileVariables.isVisited = true;

        while (queueProcess.Count > 0)
        {
            AdvancedTile t = queueProcess.Dequeue();

            listOfSelectableTiles.Add(t);
            t.basicTileVariables.isAttakable = true;

            if (t.basicTileVariables.distance < inputEnemy.EnemyMoveVariables._weaponRange) //CHANGE THE CURRENT WEAPON RANGE ONCE THE WEAPON BELT SYSTEM IS IN PLACE!!!!!!!!!!!!!!!!!
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
        inputCharacter.characterMoveVariables.currentTile = tilePlaceholder;
        inputCharacter.characterMoveVariables.isAttackRangeFound = true;
    }

    public void CalculateOverWacthPathForTheAI(GameObject enemy)
    {
        inputEnemy = enemy.GetComponent<EnemyInput>(); //REDUNDANT >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        UpdateScanAllTiles(null); //EventScanTilesUpdate();
        GetCurrentTile(enemy);

        //BFS Algorithm
        var queueProcess = new Queue<AdvancedTile>();

        queueProcess.Enqueue(tilePlaceholder);
        tilePlaceholder.basicTileVariables.isVisited = true;

        while (queueProcess.Count > 0)
        {
            AdvancedTile t = queueProcess.Dequeue();

            listOfSelectableTiles.Add(t);
            
            t.basicTileVariables.isInOverwatch = true;//


            if (t.basicTileVariables.distance < inputEnemy.EnemyMoveVariables._weaponRange) //CHANGE THE CURRENT WEAPON RANGE ONCE THE WEAPON BELT SYSTEM IS IN PLACE!!!!!!!!!!!!!!!!!
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
        
        //inputCharacter.characterMoveVariables.currentTile = tilePlaceholder;
        //inputCharacter.characterMoveVariables.isAttackRangeFound = true;
    }
}
