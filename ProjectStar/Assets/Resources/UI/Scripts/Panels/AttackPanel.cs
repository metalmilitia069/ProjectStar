using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPanel : MonoBehaviour
{
    [Header("INSERT AN UI MANAGER SO :")]
    public UIManager_SO uiManager;
    [Header("INSERT A COMBAT CALCULATOR MANAGER SO :")]
    public CombatCalculatorManager_SO CombatCalculatorManager;

    private AttackPanelTween attackPanelTweenRef;

    public Text hitChanceTextValue;
    public Text critChanceTextValue;
    public Text basicDamageTextValue;

    // Start is called before the first frame update
    void Start()
    {
        uiManager.attackPanel = this;
        attackPanelTweenRef = GetComponent<AttackPanelTween>();
    }

    public AttackPanelTween GetAttackPanelTween()
    {
        return attackPanelTweenRef;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackPanelTweenRef.isAttackModeOn)
        {
            AutomaticallyMarkEnemyOnCombatMode();

            if (uiManager.canAttackPanelDataBeTurnedOff)
            {
                hitChanceTextValue.enabled = false;
                critChanceTextValue.enabled = false;
                basicDamageTextValue.enabled = false;
            }
            else
            {
                hitChanceTextValue.enabled = true;
                hitChanceTextValue.text = (CombatCalculatorManager.finalAttackProbability * 100).ToString() + "%";

                critChanceTextValue.enabled = true;
                critChanceTextValue.text = (CombatCalculatorManager._finalCriticalProbability * 100).ToString() + "%";

                basicDamageTextValue.enabled = true;
                basicDamageTextValue.text = (CombatCalculatorManager._cachedWeapon.weaponBasicVariables.minDamage.ToString()) + "-" + (CombatCalculatorManager._cachedWeapon.weaponBasicVariables.maxDamage.ToString());
            }
        }
    }

    public void AutomaticallyMarkEnemyOnCombatMode()
    {
        CharacterInput selectedCharacterInput = attackPanelTweenRef.attackModeButton.charInput;

        if (selectedCharacterInput.SearchMarkedEnemy() != null  || selectedCharacterInput.characterMoveVariables._isMoveMode)
        {
            return;
        }
        

        float minDistance = float.MaxValue;
        EnemyInput closestEnemyInput = default;

        foreach (var enemy in selectedCharacterInput.characterCombatVariables._listOfScannedEnemies)
        {
            if (enemy != null)
            {
                float distance = Vector3.Distance(enemy.transform.position, attackPanelTweenRef.attackModeButton.charInput.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemyInput = enemy;
                    //Debug.Log("AIIIIIIIIIII DENTUUUUUUUUUU");
                }
            }
        }

        if (closestEnemyInput != null)
        {
            selectedCharacterInput.CombatScanMode(closestEnemyInput);
        }
    }
}
