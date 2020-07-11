using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterCombat", menuName = "ScriptableVariables/Type: CharacterCombat")]
public class CharacterCombat_SO : ScriptableObject
{
    [SerializeField]
    public WeaponClass _weaponClass;// = WeaponClass.Gun;    
    public int _currentWeaponIndex;



    public List<EnemyInput> _listOfScannedEnemies = new List<EnemyInput>();

    public GameObject weaponGripPlace;

    //public delegate void OnAttack(WeaponInput weaponBaseClass);
    //public static event OnAttack EventAttackTarget;

    //TEST 
    public GameObject[] weaponPrefabBelt;
    public GameObject[] weaponInstanceBelt;
    public GameObject[] weaponHolsters;
    public int weaponBeltSize = 4;


    public WeaponInput currentWeapon;




    private void OnDisable()
    {
        _listOfScannedEnemies.Clear();
    }
}
