using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{    
    [Header("CHARACTER COMBAT VARIABLES - INSTANCE :")]
    public CharacterCombat_SO characterCombatVariables;








    //teste de sacar arma futuro belt
    public GameObject weapon;
    public GameObject weapongrip;
    public GameObject playerGrip;
    public GameObject gunHolster;
    public GameObject weaponHolsterPoint;

    private Vector3 weaponLocation;
    private Quaternion weaponRotation;


    private bool isWeaponBeltReady = false;

    // Start is called before the first frame update
    void Start()
    {
        //weapon.transform.parent = playerGrip.transform;

        //weaponLocation = weapongrip.transform.localPosition * (-1);
        //weaponRotation = weapongrip.transform.localRotation;

        //weapon.transform.localPosition = weaponLocation;
        //weapon.transform.localRotation = weaponRotation;



        //if (GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt.Count > 0)
        //{
        //    GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[0].weaponBasicVariables.isCurrent = true;
        //    GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[0].transform.parent = GetComponent<CharacterInput>().characterGeometryVariables.handWeaponGripPoint.transform;

        //    weaponLocation = GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[0].weaponBasicVariables.weaponGripSocket.transform.localPosition * (-1);
        //    weaponRotation = GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[0].weaponBasicVariables.weaponGripSocket.transform.localRotation;

        //    GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[0].transform.localPosition = weaponLocation;
        //    GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[0].transform.localRotation = weaponRotation;

        //}
        //else
        //{
        //    Debug.Log("CUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU");
        //}


        //GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[0].weaponBasicVariables.weaponGripSocket



        

    }

    // Update is called once per frame
    void Update()
    {
        if (!isWeaponBeltReady)
        {
            if (GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt.Count > 0)
            {
                GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[0].weaponBasicVariables.isCurrent = true;

                GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[0].transform.parent = GetComponent<CharacterInput>().characterGeometryVariables.handWeaponGripPoint.transform;

                weaponLocation = GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[0].weaponBasicVariables.weaponGripSocket.transform.localPosition * (-1);
                weaponRotation = GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[0].weaponBasicVariables.weaponGripSocket.transform.localRotation;

                GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[0].transform.localPosition = weaponLocation;
                GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[0].transform.localRotation = weaponRotation;

                isWeaponBeltReady = true;

                GetCurrentWeapon();
            }
        }
    }

    private WeaponInput currentWeapon;
    private WeaponInput nextWeapon;
    private CharacterGeometry_SO charGeo;
    public WeaponInput GetCurrentWeapon()
    {
        for (int index = 0; index < GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt.Count; index++)
        {
            if (GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[index].weaponBasicVariables.isCurrent)
            {
                if (index == GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt.Count -1)
                {
                    nextWeapon = GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[0];
                }
                else
                {
                    int i = index + 1;
                    nextWeapon = GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[i];
                }
                currentWeapon = GetComponent<CharacterInput>().characterEquipmentVariables.weaponBelt[index];
                return currentWeapon;
            }
        }

        return currentWeapon;//GetComponent<CharacterInput>().characterEquipmentVariables.dicWeaponBelt[0];
    }

    public void ChangeWeapon()
    {

        HolsterCurrentWeapon();
        DrawNextWeapon();
    }

    public void HolsterCurrentWeapon()
    {        
        charGeo = GetComponent<CharacterInput>().characterSetupVariables.characterGeometryReference.GetComponent<CharacterGeometry>().CharacterGeometryVariables;
        
        currentWeapon.weaponBasicVariables.isCurrent = false;

        switch (currentWeapon.weaponBasicVariables.weaponClass)
        {
            case WeaponClass.Melee:                
                currentWeapon.transform.parent = charGeo.meleeWeaponHolster.transform;                
                break;
            case WeaponClass.Gun:                
                currentWeapon.transform.parent = charGeo.gunWeaponHolster.transform;
                break;
            case WeaponClass.Rifle:
                currentWeapon.transform.parent = charGeo.rifleWeaponHolster.transform;
                break;
            case WeaponClass.MiniGun:
                currentWeapon.transform.parent = charGeo.minigunWeaponHolster.transform;
                break;
            case WeaponClass.Sniper:
                currentWeapon.transform.parent = charGeo.sniperWeaponHolster.transform;
                break;
            default:
                break;
        }
        currentWeapon.transform.localPosition = default;
        currentWeapon.transform.localRotation = default;
    }

    public void DrawNextWeapon()
    {
        nextWeapon.transform.parent = charGeo.handWeaponGripPoint.transform;

        weaponLocation = nextWeapon.weaponBasicVariables.weaponGripSocket.transform.localPosition * (-1);
        weaponRotation = nextWeapon.weaponBasicVariables.weaponGripSocket.transform.localRotation;

        nextWeapon.transform.localPosition = weaponLocation;
        nextWeapon.transform.localRotation = weaponRotation;

        currentWeapon = nextWeapon;
        currentWeapon.weaponBasicVariables.isCurrent = true;
    }

    public void ScanForEnemies()
    {
        if (characterCombatVariables._listOfScannedEnemies.Count > 0)
        {
            foreach (var enemy in characterCombatVariables._listOfScannedEnemies)
            {
                enemy.EnemyStatsVariables.canBeAttacked = false;
            }
        }

        characterCombatVariables._listOfScannedEnemies.Clear();

        foreach (var item in GetComponent<CharacterInput>().GridManager.listOfSelectableTiles)
        {
            RaycastHit hit;
            if (Physics.Raycast(item.transform.position, Vector3.up, out hit, 1))
            {
                EnemyInput enemyPlaceHolder = hit.collider.GetComponent<EnemyInput>();
                if (enemyPlaceHolder)
                {
                    characterCombatVariables._listOfScannedEnemies.Add(enemyPlaceHolder);
                    enemyPlaceHolder.EnemyStatsVariables.canBeAttacked = true;

                }
            }
        }
    }

    public void Attack(EnemyInput enemy)
    {
        if (currentWeapon.weaponBasicVariables.currentAmmunition == 0 && currentWeapon.weaponBasicVariables.weaponClass != WeaponClass.Melee)
        {
            return;
        }
        transform.LookAt(enemy.transform);

        CharacterInput charInput = GetComponent<CharacterInput>();

        charInput.CombatCalculatorManager.isShowProbabilities = false;
        
        currentWeapon.GetComponent<WeaponBasic>().GatherWeaponAttackStats(charInput, enemy); //weaponInstanceBelt[_currentWeaponIndex].GetComponent<WeaponBaseClass>().GatherWeaponAttackStats((CharacterStats)this, enemy);
        currentWeapon.GetComponent<WeaponBasic>().weaponBasicVariables.currentAmmunition--;

        charInput.CombatCalculatorManager.GatherEnemyDefenseStats(enemy); //CombatCalculatorManager.instance.GatherEnemyDefenseStats(enemy);
        charInput.CombatCalculatorManager.GatherPlayerAttackStats(charInput); //CombatCalculatorManager.instance.GatherPlayerAttackStats((CharacterStats)this);
        charInput.CombatCalculatorManager.PlayerFinalAttackCalculation(enemy, charInput); //CombatCalculatorManager.instance.PlayerFinalAttackCalculation(enemy);

        //this.GetComponent<CharacterTurn>().characterTurnVariables.actionPoints--;//ITS ON COMBAT CALCULATOR NOW!!!!!!!!!!!!!!!!!!!!!!


        //GetComponent<CharacterInput>().uiManager.DisplayBullets();
        if (!characterCombatVariables.isOverWatching)
        {
            //charInput.GetComponent<CharacterTurn>().characterTurnVariables.actionPoints--;
            GetComponent<CharacterInput>().uiManager.weaponDisplayPanel.SetWeaponToDisplay();
        }


        //if (this.GetComponent<CharacterTurn>().characterTurnVariables.actionPoints <= 0 && !characterCombatVariables.isOverWatching)
        //{            
        //    GetComponent<CharacterInput>().ChangeMode();
        //    GetComponent<CharacterInput>().TurnManager.RemoveFromTurn(this.GetComponent<CharacterTurn>(), null);            
        //}
    }

    public void ShowProbability(EnemyInput enemy)
    {
        transform.LookAt(enemy.transform);

        CharacterInput charInput = GetComponent<CharacterInput>();

        currentWeapon.GetComponent<WeaponBasic>().GatherWeaponAttackStats(charInput, enemy); //weaponInstanceBelt[_currentWeaponIndex].GetComponent<WeaponBaseClass>().GatherWeaponAttackStats((CharacterStats)this, enemy);

        charInput.CombatCalculatorManager.GatherEnemyDefenseStats(enemy); //CombatCalculatorManager.instance.GatherEnemyDefenseStats(enemy);
        charInput.CombatCalculatorManager.GatherPlayerAttackStats(charInput); //CombatCalculatorManager.instance.GatherPlayerAttackStats((CharacterStats)this);
        charInput.CombatCalculatorManager.PlayerFinalAttackCalculation(enemy, null); //CombatCalculatorManager.instance.DisplayShotChance();//(weaponInstanceBelt[_currentWeaponIndex].GetComponent<WeaponBaseClass>());

        enemy.ShowProbability();
    }

    public void PrepareOverWatch()
    {
        if (GetCurrentWeapon().weaponBasicVariables.currentAmmunition <= 0)
        {
            characterCombatVariables.isOverWatching = false;
            return;
        }

        if (GetComponent<CharacterInput>().characterTurnVariables.isTurnActive)
        {            
            GetComponent<CharacterInput>().characterTurnVariables.actionPoints = 0;
            GetComponent<CharacterInput>().TurnManager.RemoveFromTurn(this.GetComponent<CharacterTurn>(), null);
        }

        characterCombatVariables.isOverWatching = true;
        OverWatch();
    }

    public void OverWatch()
    {
       

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetCurrentWeapon().weaponBasicVariables.weaponRange);

        foreach (var col in hitColliders)
        {
            if (GetCurrentWeapon().weaponBasicVariables.currentAmmunition > 0 && col.gameObject.GetComponent<EnemyInput>())
            {
                if(col.gameObject.GetComponent<EnemyInput>().EnemyMoveVariables.isMoving)
                {
                    if (!col.gameObject.GetComponent<EnemyInput>().EnemyStatsVariables.isOverWatched)
                    {
                        Attack(col.gameObject.GetComponent<EnemyInput>());
                        col.gameObject.GetComponent<EnemyInput>().EnemyStatsVariables.isOverWatched = true;
                    }
                }
            }
        }

        //GetComponent<CharacterInput>().characterTurnVariables.actionPoints = 0;
    }

    //DAMAGE FROM ENEMY

    public void ApplyDamage(int Damage, EnemyInput enemyInput)
    {
        int playerHealth = GetComponent<CharacterStats>().characterStatsVariables.health -= Damage;

        if (playerHealth <= 0)
        {
            Debug.Log("PLAYER IS DEAD!!!!");
            GetComponent<CharacterInput>().characterStatsVariables.isAlive = false;
            enemyInput.EnemyStatsVariables.playersKilled++;
            
            GetComponent<CharacterInput>().characterSavedData.AddMissionCharacter(this.GetComponent<CharacterInput>());
            GetComponent<CharacterInput>().TurnManager.RemoveFromTeam(GetComponent<CharacterTurn>(), null);
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}

