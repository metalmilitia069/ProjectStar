using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInjection : MonoBehaviour
{

    private void OnEnable()
    {
        GetComponent<WeaponBasic>().weaponBasicVariables = ScriptableObject.CreateInstance<WeaponBasic_SO>();
        GetComponent<WeaponBasic>().weaponBasicVariables.name = "BasicWeaponInstance";
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
