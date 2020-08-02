using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [Header("INSERT CHARACTER ANIMATION VARIABLES :")]
    public CharacterAnimation_SO characterAnimationVariables;

    public Animator animator = default;
    // Start is called before the first frame update
    void Start()
    {
        characterAnimationVariables.animator = GetComponent<CharacterInput>().characterSetupVariables.characterGeometryReference.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMovementAnimation(float velocity)
    {
        characterAnimationVariables.velocity = velocity;
        GetComponent<CharacterInput>().characterAnimationVariables.animator.SetFloat("Velocity", velocity);
    }

    public void SetWeaponHoldingAnimation(int weaponAnimationStance)
    {
        characterAnimationVariables.animator.SetInteger("WeaponStance", weaponAnimationStance);
        //characterAnimationVariables.animator.
    }

    public void ShootingAnimation(bool isShooting)
    {
        characterAnimationVariables.animator.SetBool("IsShooting", isShooting);
    }
}
