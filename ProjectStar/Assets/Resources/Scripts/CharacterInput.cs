using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    [Header("INSERT A GRIDMANAGER SO :")]
    public GridManager_SO GridManager;


    [Header("CHARACTER MOVE VARIABLES - INSTANCE :")]
    public CharacterMove_SO characterMoveVariables;
    [Header("CHARACTER STATS VARIABLES - INSTANCE :")]
    public CharacterStats_SO characterStatsVariables;

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
        //if (characterStatsVariables.isTurnActive)
        //{
            //if (CameraTargetManager.instance.isLocked)//
            //{
            //    //CameraTargetManager.instance.transform.parent = this.transform;//
            //    //CameraTargetManager.instance.transform.position = this.transform.position;//

            //    CameraTargetManager.instance.followTransform = transform;
            //}

            if (characterMoveVariables._isMoveMode)
            {
                //if (_listOfScannedEnemies.Count > 0)
                //{
                //    foreach (var enemy in _listOfScannedEnemies)
                //    {
                //        enemy.canBeAttacked = false;
                //    }
                //}

                //_listOfScannedEnemies.Clear();

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
                    //if (characterStatsVariables.actionPoints <= 0)
                    //{
                    //    //TurnManager.instance.PlayerCharacterActionDepleted((CharacterStats)this);
                    //    return;
                    //}
                    GridManager.CalculateAvailablePath(this.gameObject);
                }
            }

            if (characterMoveVariables._isCombatMode)
            {

                //if (!characterMoveVariables.isAttackRangeFound)
                //{
                //    _weaponRange = weaponInstanceBelt[_currentWeaponIndex].GetComponent<WeaponBaseClass>().weaponRange + attackRangeModifier;
                //    GridManager.instance.CalculateAttackPath(this.gameObject);
                //    ScanForEnemies();
                //}

                //ActivateMouseToAttack();

            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                //ChangeMode();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                //if (_isCombatMode)
                //{
                //    ChangeWeapon();
                //}
            }
        //}
    }    

    //private void OnMouseDown()
    //{
    //    CameraTargetManager.instance.followTransform = transform;
    //}

    protected void ActivateMouseToMovement()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                AdvancedTile tile = hit.collider.GetComponent<AdvancedTile>();

                if (tile != null && tile.basicTileVariables.isSelectable)
                {
                    GridManager.CalculatePathToDesignatedTile(tile);



                    characterStatsVariables.actionPoints--;                    
                }
            }
        }
    }
}
