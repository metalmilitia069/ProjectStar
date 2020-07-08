using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackModeButton : MonoBehaviour
{
    public CharacterInput charInput;

    public void ButtonAction()
    {
        SwitchToCombatMode();
    }

    public void SwitchToCombatMode()
    {
        charInput = GetComponent<ButtonInjection>().GetActiveCharacter();
        charInput.ChangeMode();
    }
}
