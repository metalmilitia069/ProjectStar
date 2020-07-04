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
                    //>>>>>>>REDO THIS WITH WEAPON BELT!!!!!!!!!!!

                    foreach (var weapon in GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt)
                    {
                        if (weapon.weaponBasicVariables.isCurrent)
                        {
                            characterMoveVariables._weaponRange = weapon.weaponBasicVariables.weaponRange;
                            break;
                        }
                    }

                    foreach (var weapon in GetComponent<CharacterInput>().characterEquipmentVariables.dicWeaponBelt)
                    {
                        if (weapon.Value.weaponBasicVariables.isCurrent)
                        {
                            characterMoveVariables._weaponRange = weapon.Value.weaponBasicVariables.weaponRange;

                        }
                        
                    }
                    
                    
                    //characterMoveVariables._weaponRange = GetComponent<CharacterCombat>().weapon.GetComponent<WeaponInput>().weaponBasicVariables.weaponRange; //weaponInstanceBelt[_currentWeaponIndex].GetComponent<WeaponBaseClass>().weaponRange + attackRangeModifier;



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

            if (Input.GetKeyDown(KeyCode.F))
            {
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
                            GetComponent<CharacterCombat>().Attack(enemy);                           
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
            foreach (var item in GridManager.tileList_SO.GetList())
            {
                item.basicTileVariables.isMoveMode = true;
            }
        }

        GridManager.ClearSelectableTiles();
    }
    

}
