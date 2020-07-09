using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    [Header("INSERT A GRIDMANAGER SO :")]
    public GridManager_SO GridManager;
    [Header("INSERT A COMBAT CALCULATOR MANAGER SO :")]
    public CombatCalculatorManager_SO CombatCalculatorManager;
    [Header("INSERT A TURN MANAGER SO :")]
    public TurnManager_SO TurnManager;
    [Header("INSERT A UI MANAGER SO :")]
    public UIManager_SO uiManager;
    [Header("INSERT THE MAIN CAMERA CONTROLLER VARIABLES :")]
    public MainCameraController_SO MainCameraControllerVariables;


    [Header("CHARACTER SETUP VARIABLES - COPY FROM SETUP :")]
    public CharacterSetup_SO characterSetupVariables;
    [Header("CHARACTER MOVE VARIABLES - INSTANCE :")]
    public CharacterMove_SO characterMoveVariables;
    [Header("CHARACTER EQUIPMENT VARIABLES - COPY FROM SETUP :")]
    public CharacterEquipment_SO characterEquipmentVariables;
    [Header("CHARACTER GEOMETRY VARIABLES = COPY FROM SETUP :")]
    public CharacterGeometry_SO characterGeometryVariables;
    [Header("CHARACTER COMBAT VARIABLES - INSTANCE :")]
    public CharacterCombat_SO characterCombatVariables;
    [Header("CHARACTER STATS VARIABLES - INSTANCE :")]
    public CharacterStats_SO characterStatsVariables;
    [Header("CHARACTER TURN VARIABLES - INSTANCE :")]
    public CharacterTurn_SO characterTurnVariables;

    // Start is called before the first frame update
    void Start()
    {
        //AddPlayerToTeamList();

        ////weaponInstanceBelt = new GameObject[weaponPrefabBelt.Length];



        ////int weaponIndex = 0;

        ////foreach (var weapon in weaponPrefabBelt)
        ////{
        ////    weaponInstanceBelt[weaponIndex] = Instantiate(weapon, this.transform);
        ////    if (weaponInstanceBelt[weaponIndex].GetComponent<WeaponBaseClass>().isCurrent)
        ////    {
        ////        weaponInstanceBelt[weaponIndex].transform.localPosition = weaponGripPlace.transform.localPosition;
        ////        _currentWeaponIndex = weaponIndex;
        ////        _weaponClass = weaponInstanceBelt[_currentWeaponIndex].GetComponent<WeaponBaseClass>().weaponClass;
        ////        _weaponRange = weaponInstanceBelt[_currentWeaponIndex].GetComponent<WeaponBaseClass>().weaponRange + attackRangeModifier;
        ////    }
        ////    else
        ////    {
        ////        weaponInstanceBelt[weaponIndex].transform.localPosition = weaponHolsters[weaponIndex].transform.localPosition;
        ////    }
        ////    weaponIndex++;
        ////}
    }

    // Update is called once per frame
    void Update()
    {
        if (this.characterTurnVariables.isTurnActive)
        {
            if (MainCameraControllerVariables.isLocked)
            {
                MainCameraControllerVariables.followTransform = transform;
            }

            if (characterMoveVariables._isMoveMode)
            {
                if (characterCombatVariables._listOfScannedEnemies.Count > 0)
                {
                    foreach (var enemy in characterCombatVariables._listOfScannedEnemies)
                    {
                        enemy.EnemyStatsVariables.canBeAttacked = false;
                    }
                }

                UnMarkEnemy();
                characterCombatVariables._listOfScannedEnemies.Clear();

                if (!characterMoveVariables.isMoving)
                {
                    ActivateMouseToMovement();
                }
                else
                {
                    GetComponent<CharacterMove>().Move();
                }

                if (!characterMoveVariables.isTilesFound)
                {
                    if (characterTurnVariables.actionPoints <= 0)
                    {
                        TurnManager.RemoveFromTurn(this.GetComponent<CharacterTurn>(), null);
                        return;
                    }
                    GridManager.CalculateAvailablePath(this.gameObject);
                }
            }

            if (characterMoveVariables._isCombatMode)
            {

                if (!characterMoveVariables.isAttackRangeFound)
                {
                    characterMoveVariables._weaponRange = GetComponent<CharacterCombat>().GetCurrentWeapon().weaponBasicVariables.weaponRange;

                    if (characterTurnVariables.actionPoints <= 0)
                    {
                        TurnManager.RemoveFromTurn(this.GetComponent<CharacterTurn>(), null);
                        return;
                    }
                    GridManager.CalculateAttackPath(this.gameObject);
                    GetComponent<CharacterCombat>().ScanForEnemies();
                }

                ActivateMouseToAttack();

            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                ChangeMode();
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                GetComponent<CharacterCombat>().ChangeWeapon();
                GridManager.ClearSelectableTiles();
                characterMoveVariables.isAttackRangeFound = false;
                //if (_isCombatMode)
                //{
                //    ChangeWeapon();
                //}
            }
            
        }

        
    }

    //public bool canChange = false;

    private void OnMouseOver()
    {
        foreach (var character in TurnManager.listOfAllCharacters.GetList())
        {
            if (character.GetComponent<CharacterInput>().characterMoveVariables.isMoving)
            {
                return;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {

            MainCameraControllerVariables.canMouseInput = false;

            MainCameraControllerVariables.LockCamera(transform);
            TurnManager.SelectCharacterOnClick(this.GetComponent<CharacterTurn>());
        }        
    }   

    protected void ActivateMouseToMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                AdvancedTile tile = hit.collider.GetComponent<AdvancedTile>();

                if (tile != null && tile.basicTileVariables.isSelectable)
                {
                    GridManager.CalculatePathToDesignatedTile(tile);



                    //characterTurnVariables.actionPoints--;  ///botei em characterInput                   
                }
            }
        }
    }

    public void ActivateMouseToAttack()
    {        
        //if(Input.GetMouseButtonDown(0))
        //{
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            EnemyInput enemyPlaceHolder = hit.collider.GetComponent<EnemyInput>();
            if (enemyPlaceHolder)
            {
                foreach (var enemy in characterCombatVariables._listOfScannedEnemies)
                {
                    if (enemy == enemyPlaceHolder)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            //GetComponent<CharacterCombat>().Attack(enemy);
                            CombatScanMode(enemy);
                        }
                        else
                        {
                            GetComponent<CharacterCombat>().ShowProbability(enemy);
                        }
                    }


                }
            }
        }
        //}
    }

    public void CombatScanMode(EnemyInput enemy)
    {
        UnMarkEnemy();

        enemy.EnemyCombatVariables.isMarkedEnemy = true;

        uiManager.spawnedCrossSignUI.transform.position = enemy.transform.position;
        uiManager.spawnedCrossSignUI.gameObject.SetActive(true);
    }

    public void UnMarkEnemy()
    {
        Debug.Log(characterCombatVariables._listOfScannedEnemies.Count);
        EnemyInput searchedEnemy = SearchMarkedEnemy();
        if (searchedEnemy != null)
        {
            Debug.Log("search enemy found!");
            searchedEnemy.EnemyCombatVariables.isMarkedEnemy = false;
        }
    }

    public EnemyInput SearchMarkedEnemy()
    {
        foreach (var scannedEnemy in characterCombatVariables._listOfScannedEnemies)
        {
            if (scannedEnemy.EnemyCombatVariables.isMarkedEnemy)
            {
                //scannedEnemy.EnemyCombatVariables.isMarkedEnemy = false;
                return scannedEnemy;
            }
        }
        return null;
    }

    public void TurnOffCombatScanMode()
    {
        uiManager.spawnedCrossSignUI.gameObject.SetActive(false);
    }

    public void ChangeMode()
    {
        if (characterMoveVariables._isMoveMode)
        {
            characterMoveVariables._isCombatMode = true;
            characterMoveVariables._isMoveMode = false;
            characterMoveVariables.isAttackRangeFound = false;
            foreach (var item in GridManager.tileList_SO.GetList())
            {
                item.basicTileVariables.isMoveMode = false;
            }

        }
        else if (characterMoveVariables._isCombatMode)
        {
            characterMoveVariables._isCombatMode = false;
            characterMoveVariables._isMoveMode = true;
            characterMoveVariables.isTilesFound = false;
            TurnOffCombatScanMode();
            foreach (var item in GridManager.tileList_SO.GetList())
            {
                item.basicTileVariables.isMoveMode = true;
            }
        }

        GridManager.ClearSelectableTiles();
    }

    public void ChangeModeFromButton()
    {
        if (characterMoveVariables._isMoveMode)
        {
            characterMoveVariables._isCombatMode = true;
            characterMoveVariables._isMoveMode = false;
            characterMoveVariables.isAttackRangeFound = false;
            foreach (var item in GridManager.tileList_SO.GetList())
            {
                item.basicTileVariables.isMoveMode = false;
            }

        }

        GridManager.ClearSelectableTiles();
    }

    //public WeaponInput GetCurrentWeapon()
    //{
    //    foreach (var weapon in characterEquipmentVariables.dicWeaponBelt)
    //    {
    //        if (weapon.Value.weaponBasicVariables.isCurrent)
    //        {
    //            return weapon.Value;
    //        }
    //    }

    //    return characterEquipmentVariables.dicWeaponBelt[0];
    //}

    //public void ChangeWeapon()
    //{
    //    if (characterEquipmentVariables.dicWeaponBelt.ContainsKey(WeaponClass.Gun))
    //    {
    //        WeaponInput value;
    //        characterEquipmentVariables.dicWeaponBelt.TryGetValue(WeaponClass.Gun, out value);
            
    //    }
        
    //}

    

}
