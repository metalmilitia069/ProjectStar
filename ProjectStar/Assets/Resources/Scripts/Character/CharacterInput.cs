﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    [Header("CHARACTER SAVED DATA SO - COPY FROM CHARACTER SAVED DATA")]
    public SavedPlayerCharacters_SO characterSavedData;
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
    [Header("CHARACTER ANIMATION VARIABLES - INSTANCE :")]
    public CharacterAnimation_SO characterAnimationVariables;

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
            //characterCombatVariables.isOverWatching = false;


            if (MainCameraControllerVariables.isLocked)
            {
                MainCameraControllerVariables.followTransform = transform;
            }

            if (characterMoveVariables._isMoveMode)
            {

                ClearScannedEnemiesList();

                if (!characterMoveVariables.isMoving)
                {
                    uiManager.EnableButtons();
                    ActivateMouseToMovement();
                }
                else
                {
                    uiManager.DisableButtons();
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
                    //if (this.GetComponent<CharacterTurn>().characterTurnVariables.actionPoints <= 0)
                    //{
                    //    GetComponent<CharacterInput>().ChangeMode();
                    //    GetComponent<CharacterInput>().TurnManager.RemoveFromTurn(this.GetComponent<CharacterTurn>(), null);

                    //    return;
                    //    //TurnManager.instance.PlayerCharacterActionDepleted((CharacterStats)this);  //TODO: implement TURN MANAGER
                    //}

                if (!characterMoveVariables.isAttackRangeFound)
                {

                    characterMoveVariables._weaponRange = GetComponent<CharacterCombat>().GetCurrentWeapon().weaponBasicVariables.weaponRange;


                    //if (characterTurnVariables.actionPoints <= 0)
                    //{
                    //    ClearScannedEnemiesList();
                    //    Debug.Log("CUUUUUUUUU");
                    //    TurnOffCombatScanMode();

                    //    TurnManager.RemoveFromTurn(this.GetComponent<CharacterTurn>(), null);
                    //    return;
                    //}
                    GridManager.CalculateAttackPath(this.gameObject);
                    GetComponent<CharacterCombat>().ScanForEnemies();
                    //CombatScanMode(SearchMarkedEnemy());

                    //CombatScanMode();
                }

                ActivateMouseToAttack();

            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                ChangeMode();
            }

            //if (Input.GetKeyDown(KeyCode.Alpha9))
            //{
            //    GetComponent<CharacterCombat>().PrepareOverWatch();
            //    GetComponent<CharacterCombat>().OverWatch();
            //    //ChangeWeapon();
            //    //if (_isCombatMode)
            //    //{
            //    //    ChangeWeapon();
            //    //}
            //}

        }
        else
        {
            if (characterCombatVariables.isOverWatching)
            {           
                GetComponent<CharacterCombat>().OverWatch();            
            }
        }




        
    }

    public void ChangeWeapon()
    {
        ClearScannedEnemiesList();

        TurnOffCombatScanMode();

        if (!characterMoveVariables._isMoveMode)
        {
            GridManager.ClearSelectableTiles();
        }

        GetComponent<CharacterCombat>().ChangeWeapon();
        characterMoveVariables.isAttackRangeFound = false;
    }

    public void ClearScannedEnemiesList()
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

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
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
                            if (EventSystem.current.IsPointerOverGameObject())
                            {
                                return;
                            }
                            if (enemy.EnemyCombatVariables.isMarkedEnemy)
                            {
                                GetComponent<CharacterCombat>().Attack(enemy);
                            }
                            else
                            {
                                CombatScanMode(enemy);
                            }
                            //GetComponent<CharacterCombat>().Attack(enemy);
                            //GetComponent<CharacterCombat>().ShowProbability(enemy);
                        }
                        else
                        {
                            //TODO: highlight selectable enemy (UI)
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

        GetComponent<CharacterCombat>().ShowProbability(enemy);

        uiManager.canAttackPanelDataBeTurnedOff = false;

        uiManager.spawnedCrossSignUI.transform.position = enemy.transform.position;
        uiManager.spawnedCrossSignUI.gameObject.SetActive(true);
    }

    public void UnMarkEnemy()
    {
        uiManager.canAttackPanelDataBeTurnedOff = true;


        EnemyInput searchedEnemy = SearchMarkedEnemy();
        if (searchedEnemy != null)
        {            
            searchedEnemy.EnemyCombatVariables.isMarkedEnemy = false;
        }

        TurnOffCombatScanMode();
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

    public void GetAttackDAta()
    {

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
            UnMarkEnemy();
            GetComponent<CharacterAnimation>().ShootingAnimation(false);
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
