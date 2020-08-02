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
        StartCoroutine(GetComponent<WeaponBasic>().CombatCalculatorManager.ApplyDamage(StartCoroutine(Shooting(characterInput)), enemy, _finaldamage, characterInput));
    }

    public void AIShoot(EnemyInput enemy, int _finaldamage, CharacterInput characterInput)
    {
        StartCoroutine(GetComponent<WeaponBasic>().CombatCalculatorManager.AIApplyDamage(StartCoroutine(Shooting(null)), enemy, _finaldamage, characterInput));
    }



    public IEnumerator Shooting(CharacterInput characterInput)
    {
        GameObject vfx;
        timeToFire = Time.time + (1 / projectilePrefab.GetComponent<ProjectileMove>().fireRate);

        float weaponfireRate = GetComponent<WeaponBasic>().weaponBasicVariables.fireRate;

        //l

        switch (GetComponent<WeaponBasic>().weaponBasicVariables.weaponClass)
        {
            case WeaponClass.Melee:
                break;
            case WeaponClass.Gun:
                if (characterInput != null)
                {
                    characterInput.GetComponent<CharacterAnimation>().ShootingAnimation(true);
                }
                vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);

                yield return new WaitForSeconds(1.167f);//(1 / weaponfireRate) ;

                if (characterInput != null)
                {
                    characterInput.GetComponent<CharacterAnimation>().ShootingAnimation(true);
                }
                vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);

                yield return new WaitForSeconds(1.167f);//(1 / weaponfireRate);

                if (characterInput != null)
                {
                    characterInput.GetComponent<CharacterAnimation>().ShootingAnimation(false);
                }

                break;
            case WeaponClass.Rifle:

                if (characterInput != null)
                {
                    characterInput.GetComponent<CharacterAnimation>().ShootingAnimation(true);
                }
                else
                {
                    vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);
                }

                yield return new WaitForSeconds(1 / weaponfireRate);

                if (characterInput != null)
                {
                    vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);
                    characterInput.GetComponent<CharacterAnimation>().ShootingAnimation(true);
                }
                vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);

                yield return new WaitForSeconds(1 / weaponfireRate);

                if (characterInput != null)
                {
                    characterInput.GetComponent<CharacterAnimation>().ShootingAnimation(true);
                }
                vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);

                yield return new WaitForSeconds(1 / weaponfireRate);

                if (characterInput != null)
                {
                    characterInput.GetComponent<CharacterAnimation>().ShootingAnimation(true);
                }
                vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);

                yield return new WaitForSeconds(1 / weaponfireRate);

                if (characterInput != null)
                {
                    characterInput.GetComponent<CharacterAnimation>().ShootingAnimation(false);
                }

                break;
            case WeaponClass.MiniGun:

                if (characterInput != null)
                {
                    characterInput.GetComponent<CharacterAnimation>().ShootingAnimation(true);
                }
                else
                {
                    vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);
                }                

                yield return new WaitForSeconds(1 / weaponfireRate);

                if (characterInput != null)
                {
                    vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);
                    characterInput.GetComponent<CharacterAnimation>().ShootingAnimation(true);
                }

                yield return new WaitForSeconds(1 / weaponfireRate);

                vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);

                yield return new WaitForSeconds(1 / weaponfireRate);

                vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);

                yield return new WaitForSeconds(1 / weaponfireRate);

                vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);

                yield return new WaitForSeconds(1 / weaponfireRate);

                vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);

                yield return new WaitForSeconds(1 / weaponfireRate);

                vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);

                yield return new WaitForSeconds(1 / weaponfireRate);

                vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);

                yield return new WaitForSeconds(1 / weaponfireRate);

                if (characterInput != null)
                {
                    characterInput.GetComponent<CharacterAnimation>().ShootingAnimation(false);
                }

                break;
            case WeaponClass.Sniper:
                if (characterInput != null)
                {
                    characterInput.GetComponent<CharacterAnimation>().ShootingAnimation(true);
                    yield return new WaitForSeconds(.30f);
                    vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);
                }
                else
                {
                    vfx = Instantiate(projectilePrefab, firePointer.transform.position, firePointer.transform.rotation);
                }

                if (characterInput != null)
                {
                    characterInput.GetComponent<CharacterAnimation>().ShootingAnimation(false);
                }


                yield return new WaitForSeconds(1 / weaponfireRate);
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(1);

    }
}
