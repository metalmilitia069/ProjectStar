using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject firePointer;

    public int speed = 5;
    public float timeToFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.U) && Time.time > timeToFire)
        {
            timeToFire = Time.time + (1 / projectilePrefab.GetComponent<ProjectileMove>().fireRate);
            //Shoot();
        }
    }

    public void Shoot(EnemyInput enemy, int _finaldamage, CharacterInput characterInput)
    {
        //GameObject vfx;
        //timeToFire = 0;

        //if (firePointer != null)
        //{
        //    vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);//rotation);

        //}
        //else
        //{
        //    Debug.Log("No Fire Point Found!");
        //}

        StartCoroutine(GetComponent<WeaponBasic>().CombatCalculatorManager.ApplyDamage(StartCoroutine(Shooting()), enemy, _finaldamage, characterInput));
    }

    public IEnumerator Shooting()
    {
        GameObject vfx;
        timeToFire = Time.time + (1 / projectilePrefab.GetComponent<ProjectileMove>().fireRate);

        vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);//rotation);

        yield return new WaitForSeconds(1 / projectilePrefab.GetComponent<ProjectileMove>().fireRate);

        vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);//rotation);

        yield return new WaitForSeconds(1 / projectilePrefab.GetComponent<ProjectileMove>().fireRate);

        vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);//rotation);

        yield return new WaitForSeconds(1 / projectilePrefab.GetComponent<ProjectileMove>().fireRate);

        vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);//rotation);

        yield return new WaitForSeconds(1 / projectilePrefab.GetComponent<ProjectileMove>().fireRate);

    }
}
