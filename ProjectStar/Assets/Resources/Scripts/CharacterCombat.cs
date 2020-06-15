using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    [Header("CHARACTER COMBAT VARIABLES - INSTANCE :")]
    public CharacterCombat_SO characterCombatVariables;


    public GameObject weapon;
    public GameObject weapongrip;
    public GameObject playerGrip;
    public GameObject gunHolster;
    public GameObject weaponHolsterPoint;

    private Vector3 weaponLocation;
    private Quaternion weaponRotation;

    // Start is called before the first frame update
    void Start()
    {
        weapon.transform.parent = playerGrip.transform;

        weaponLocation = weapongrip.transform.localPosition * (-1);
        weaponRotation = weapongrip.transform.localRotation;

        weapon.transform.localPosition = weaponLocation;
        weapon.transform.localRotation = weaponRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("CUUUU");
            if (weapon.transform.parent == playerGrip.transform)
            {
                weapon.transform.parent = gunHolster.transform;

                weaponLocation = weaponHolsterPoint.transform.localPosition * (-1);
                weaponRotation = weaponHolsterPoint.transform.localRotation;

                weapon.transform.localPosition = weaponLocation;
                weapon.transform.localRotation = weaponRotation;
            }
            else //if (weapon.transform.parent == weaponHolsterPoint.transform)
            {
                
                weapon.transform.parent = playerGrip.transform;

                weaponLocation = weapongrip.transform.localPosition * (-1);
                weaponRotation = weapongrip.transform.localRotation;

                weapon.transform.localPosition = weaponLocation;
                weapon.transform.localRotation = weaponRotation;
            }
        }
    }
}
