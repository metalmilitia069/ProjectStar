using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterCombat", menuName = "ScriptableVariables/Type: CharacterCombat")]
public class CharacterCombat_SO : ScriptableObject
{
    [SerializeField]
    public WeaponClass _weaponClass = default;// = WeaponClass.Gun;    
    public int _currentWeaponIndex = default;



    public List<EnemyInput> _listOfScannedEnemies = new List<EnemyInput>();

    public GameObject weaponGripPlace;

    //public delegate void OnAttack(WeaponInput weaponBaseClass);
    //public static event OnAttack EventAttackTarget;

    //TEST 
    public GameObject[] weaponPrefabBelt;
    public GameObject[] weaponInstanceBelt;
    public GameObject[] weaponHolsters;
    public int weaponBeltSize = 4;


    public WeaponInput currentWeapon = default;
    public bool isOverWatching = false;



    private void OnDisable()
    {
        _listOfScannedEnemies.Clear();
    }
}
