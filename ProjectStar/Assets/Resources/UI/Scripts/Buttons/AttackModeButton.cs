using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackModeButton : MonoBehaviour, IButton
{
    public void ButtonAction()
    {
        SwitchToCombatMode();
    }

    public void SwitchToCombatMode()
    {
        CharacterInput charInput = GetComponent<ButtonInjection>().GetActiveCharacter();
        charInput.ChangeMode();
    }
}
