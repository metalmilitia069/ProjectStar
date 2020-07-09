using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPanel : MonoBehaviour
{
    [Header("INSERT AN UI MANAGER SO :")]
    public UIManager_SO uiManager;
    [Header("INSERT A COMBAT CALCULATOR MANAGER SO :")]
    public CombatCalculatorManager_SO CombatCalculatorManager;




    private AttackPanelTween attackPanelTweenRef;

    // Start is called before the first frame update
    void Start()
    {
        attackPanelTweenRef = GetComponent<AttackPanelTween>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackPanelTweenRef.isAttackModeOn)
        {
            CharacterInput selectedCharacterInput = attackPanelTweenRef.attackModeButton.charInput;

            if (selectedCharacterInput.SearchMarkedEnemy() != null)
            {
                return;
            }

            float minDistance = float.MaxValue;
            EnemyInput closestEnemyInput = default;

            foreach (var enemy in selectedCharacterInput.characterCombatVariables._listOfScannedEnemies)
            {
                float distance = Vector3.Distance(enemy.transform.position, attackPanelTweenRef.attackModeButton.charInput.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemyInput = enemy;
                }
            }
                       
            selectedCharacterInput.CombatScanMode(closestEnemyInput);
        }
    }
}
