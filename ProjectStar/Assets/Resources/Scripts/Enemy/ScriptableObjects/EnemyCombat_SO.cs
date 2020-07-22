using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCombat", menuName = "ScriptableVariables/Type: EnemyCombat")]
public class EnemyCombat_SO : ScriptableObject
{
    [SerializeField]
    public WeaponClass _weaponClass;// = WeaponClass.Gun;    
    public int _currentWeaponIndex;



    public List<CharacterInput> _listOfScannedCharacters = new List<CharacterInput>();
    public List<CharacterInput> listOfWatchedCharacters = new List<CharacterInput>();
    public bool canOverwatch = true;
    public bool checkOverwatch = true;

    public bool isMarkedEnemy = false;

    public GameObject weaponGripPlace;

    //public delegate void OnAttack(WeaponInput weaponBaseClass);
    //public static event OnAttack EventAttackTarget;

    //TEST 
    public GameObject[] weaponPrefabBelt;
    public GameObject[] weaponInstanceBelt;
    public GameObject[] weaponHolsters;
    public int weaponBeltSize = 4;


    public bool isOverWatching = false;


    private void OnDisable()
    {
        _listOfScannedCharacters.Clear();        

    }
}
