using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAnimation", menuName = "ScriptableVariables/Type: CharacterAnimation")]
public class CharacterAnimation_SO : ScriptableObject
{
    public float velocity = 0.0f;
    public bool IsGun = false;
    public bool IsRifle = false;
    public bool IsShotgun = false;
    public bool IsShooting = false;
    public Animator animator = default;

    private void OnDisable()
    {
        velocity = 0.0f;
        IsGun = false;
        IsRifle = false;
        IsShotgun = false;
        IsShooting = false;
        animator = default;
    }



}
